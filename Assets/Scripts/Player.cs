using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public static Player instance;
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









    /// <summary>
    /// Gancho
    /// </summary>
    
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
    }

    private void Update()
    {
        Movement();
        JumpHandler();
        
        
        //WallChecker();
        //PositionCorrection();
        //DestroyPiece();

        //Debug.Log(WallChecker());
    }
    
    








    void Movement()
    {
        h = Input.GetAxisRaw("Horizontal");
        
        dirMov = new Vector3(h * (Mathf.Clamp(maxMovSpeed, 2, 10)), rb.velocity.y);
        rb.velocity = dirMov;

        if (IsGrounded())
        {
           
            if (h > 0)
            {
                //sR.sprite = spritesPlayer[1];
            }
            else if (h < 0)
            {
                //sR.sprite = spritesPlayer[2];
            }

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

    #region Wall

    bool WallChecker()
    {
        //isPushed = Physics2D.OverlapCircle(wallDetector.position, detectorLenght, isGround);
        return isPushed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(wallDetector.position, detectorLenght);
    }

    /*
    void PositionCorrection()
    {
        if (!WallChecker())
        {
            if (transform.position.x != startPosition.x)
            {
                transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, transform.position.z),
                    new Vector3(startPosition.x, transform.position.y, transform.position.z), backSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, startPosition) > 7)
            {
                Death();
            }
        }
    }
    */
    #endregion

    




    /*
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

        if (collision.gameObject.CompareTag("Coin"))
        {
            //efectos de cosas al pillarla
            GameManager.instance.GetCoin();
            Destroy(collision.gameObject);
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
    
     
     */
}
