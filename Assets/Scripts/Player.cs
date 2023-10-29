using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Player hookRef;
    Rigidbody2D rb;
    public static Player instance;
    Animator anim;
    [SerializeField] AnimatorOverrideController overrideOneHit, overrideTwoHit;
    [HideInInspector] public float lifes;
    //[HideInInspector] public Vector3 startPosition;

    [SerializeField] AudioClip[] sfx;
    AudioSource aS;

    [SerializeField] Sprite[] spritesPlayer;
    Sprite currenSprite;
    SpriteRenderer sR;

    [Header("Movimiento")]

    float h;
    [SerializeField] float maxMovSpeed = 10;
    Vector3 dirMov;

    [Header("Jump")]

    [SerializeField] Transform playerFeet;
    [SerializeField] LayerMask isGround;
    [SerializeField] float jumpForce;
    [SerializeField] float feetRadius = 0.5f;
    bool isGrounded;

    [SerializeField] float coyoteTime = 0.2f;
    float coyoteTimeCounter;

    [SerializeField] float bufferTime = 0.2f;
    float jumpBufferCounter;

    [SerializeField] float restrictionTime = 0.4f;
    float restrictionCounter;
    bool jumping;

    [SerializeField] Transform left, right;

    [Header("WallPushed")]

    bool isPushed;
    //[SerializeField] Transform wallDetector;
    [SerializeField] float detectorLenght;
    [SerializeField] int backSpeed;
    float cSpeed;

    enum States { Normal, Pushed };
    States currentState;

    [Header("Attack")]
    [SerializeField] float attackRange;
    [SerializeField] LayerMask enemyLayer, bossLayer;

    [Header("Hook")]
    public Camera cam;     
    public LineRenderer lr;
    public LayerMask grappleMask;
    public float moveSpeed = 2;
    public float grappleLength = 5;
    [Min(1)]
    public int maxPoints = 3;

    public bool canHook = true;

    private List<Vector2> points = new List<Vector2>();
    private bool isGrappling = false; // Variable para controlar si el gancho est� activo

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //startPosition = transform.position;
        aS = GetComponent<AudioSource>();
        sR = GetComponent<SpriteRenderer>();
        currenSprite = GetComponent<Sprite>();
        lr.positionCount = 0;

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Movement();
        JumpHandler();
        Hook();
        
        //WallChecker();
        //PositionCorrection();
        //DestroyPiece();

        //Debug.Log(WallChecker());
    }
    #region Hook
    public void Hook()
    {
        if (Input.GetMouseButtonDown(0) && !isGrappling && canHook) // Verifica si no se est� agarrando
        {
            
            isGrappling = true; // Establece que el gancho est� activo

            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - (Vector2)transform.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, grappleLength, grappleMask);
            if (hit.collider != null)
            {
                Vector2 hitPoint = hit.point;
                points.Add(hitPoint);

                if (points.Count > maxPoints)
                {
                    points.RemoveAt(0);
                }
            }
        }

        if (isGrappling)
        {

            if (points.Count > 0)
            {
                Vector2 moveTo = centriod(points.ToArray());
                rb.MovePosition(Vector2.MoveTowards((Vector2)transform.position, moveTo, Time.deltaTime * moveSpeed));

                lr.positionCount = 0;
                lr.positionCount = points.Count * 2;
                for (int n = 0, j = 0; n < points.Count * 2; n += 2, j++)
                {
                    lr.SetPosition(n, transform.position);
                    lr.SetPosition(n + 1, points[j]);
                }
            }
            else
            {
                // Si no hay puntos v�lidos, desactiva el gancho
                Detatch();
            }
        }

        if (isGrappling) // Si el gancho est� activo, contin�a el comportamiento
        {
            canHook = false;
            rb.gravityScale = 0;
            Vector2 moveTo = centriod(points.ToArray());
            rb.MovePosition(Vector2.MoveTowards((Vector2)transform.position, moveTo, Time.deltaTime * moveSpeed));

            lr.positionCount = 0;
            lr.positionCount = points.Count * 2;
            for (int n = 0, j = 0; n < points.Count * 2; n += 2, j++)
            {
                lr.SetPosition(n, transform.position);
                lr.SetPosition(n + 1, points[j]);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Detatch();
        }
    }

    public void Detatch()
    {
        isGrappling = false; // Desactiva el gancho
        rb.gravityScale = 9.73f;
        lr.positionCount = 0;
        points.Clear();
    }

    Vector2 centriod(Vector2[] points)
    {
        Vector2 center = Vector2.zero;
        foreach (Vector2 point in points)
        {
            center += point;
        }
        center /= points.Length;
        return center;
    }

    #endregion
    
    void Movement()
    {
        h = Input.GetAxisRaw("Horizontal");
        
        dirMov = new Vector3(h * (Mathf.Clamp(maxMovSpeed, 2, 10)), rb.velocity.y);
        rb.velocity = dirMov;

        if (dirMov.magnitude >= 0.1f)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }

        if (IsGrounded())
        {
            if (h == 0 && rb.velocity.x != 0)
            {
                rb.velocity = Vector3.zero;
            }
        }
    }

    #region Jump
    bool IsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(playerFeet.position, feetRadius, isGround);
        return isGrounded;
    }

    void JumpHandler()
    {
        #region Coyote
        if (IsGrounded())
        {
            canHook = true;
            coyoteTimeCounter = coyoteTime;
            restrictionCounter = restrictionTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        #endregion

        #region Buffer
        
        if (Input.GetKeyDown(KeyCode.Space) && !jumping)
        {
            jumpBufferCounter = bufferTime;
        }
        
        #endregion

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0)
        {
            Jump();
            jumpBufferCounter = 0f;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            coyoteTimeCounter = 0f;
        }

        #region Correccion Esquinas
        /*RaycastHit2D raycastLeft = Physics2D.Raycast(left.position, Vector2.up, 0.8f, isGround);
        RaycastHit2D raycastRight = Physics2D.Raycast(right.position, Vector2.up, 0.8f, isGround);

        if (raycastLeft && !raycastRight)
        {
            transform.position += new Vector3(0.2f, 0, 0);
        }
        else if (raycastRight && !raycastLeft)
        {
            transform.position -= new Vector3(0.2f, 0, 0);
        }
        */
        #endregion
    }

    void Jump()
    {
        //aS.PlayOneShot(sfx[0]);
        anim.SetTrigger("Jump");
        rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);

        StartCoroutine(Wait());
        jumpBufferCounter = 0;
    }

    IEnumerator Wait()
    {
        jumping = true;
        yield return new WaitForSeconds(0.2f);
        jumping = false;
    }
    #endregion

    public void GetHit(int damageTaken)
    {
        lifes -= damageTaken;
        if (lifes == 2)
        {
            anim.runtimeAnimatorController = overrideOneHit;
        }
        else if (lifes == 1)
        {
            anim.runtimeAnimatorController = overrideTwoHit;
        }
        else if (lifes <= 0)
        {
            Death();
        }
    }

    public void GetHealed()
    {
        lifes++;
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetTrigger("Attack");
        }
    }

    public void DealDamage()
    {
        Collider2D coll = Physics2D.OverlapCircle(transform.position + transform.right, attackRange, enemyLayer);
        if (coll != null)
        {
            if (coll.CompareTag("Enemy"))
            {
                coll.GetComponent<EnemyHealth>().GetHit();
            }
            else if (coll.CompareTag("Boss"))
            {
                coll.GetComponent<BossHealth>().GetHit();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            Death();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            Death();
        }
    }

    void Death()
    {
        aS.PlayOneShot(sfx[1]);
        GameManager.instance.StopAllCoroutines();

        if (GameManager.instance.tutorialAcabado)
        {
            GameManager.instance.DeathPanelActivate();
        }
        else
        {
            GameManager.instance.FailedTutorial();
        }
    }
}
