using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rope : MonoBehaviour
{
    public Transform objetoA;
    public Transform objetoB;
    public float longitudInicial = 2.0f; // Longitud inicial de la cuerda
    public float fuerzaResorte = 10.0f; // Fuerza del resorte
    public float frecuenciaResorte = 5.0f; // Frecuencia del resorte
    public float amortiguacionResorte = 0.5f; // Amortiguación del resorte

    private SpringJoint2D resorte;

    void Start()
    {
        // Crea una articulación de resorte entre objetoA y objetoB
        resorte = gameObject.AddComponent<SpringJoint2D>();
        resorte.connectedBody = objetoB.GetComponent<Rigidbody2D>();
        resorte.distance = longitudInicial;
        resorte.frequency = frecuenciaResorte;
        resorte.dampingRatio = amortiguacionResorte;
        resorte.autoConfigureDistance = false;
    }

    void Update()
    {
        // Ajusta la distancia del resorte en función de la distancia actual entre objetoA y objetoB
        float distanciaActual = Vector2.Distance(objetoA.position, objetoB.position);
        resorte.distance = distanciaActual;
    }
}


