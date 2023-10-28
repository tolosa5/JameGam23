using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{

    public Camera cam;
    public LineRenderer lr;
    public LayerMask grappleMask;
    public float moveSpeed = 2;
    public float grappleLength = 5;
    [Min(1)]
    public int maxPoints = 3;

    private bool canHook = true;

    private Rigidbody2D rb;
    private List<Vector2> points = new List<Vector2>();
    private bool isGrappling = false; // Variable para controlar si el gancho está activo

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr.positionCount = 0;
    }

    void Update()
    {
       
    }

    public void Hook()
    {
        if (Input.GetMouseButtonDown(0) && !isGrappling && canHook) // Verifica si no se está agarrando
        {
            isGrappling = true; // Establece que el gancho está activo

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
                // Si no hay puntos válidos, desactiva el gancho
                Detatch();
            }
        }


        if (isGrappling) // Si el gancho está activo, continúa el comportamiento
        {
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

    private void OnDrawGizmos()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector2)transform.position).normalized;

        Gizmos.DrawLine(transform.position, (Vector2)transform.position + direction);

        foreach (Vector2 point in points)
        {
            Gizmos.DrawLine(transform.position, point);
        }
    }
}
