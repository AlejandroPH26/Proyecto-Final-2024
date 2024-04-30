using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class EMurcielagoPrueba : MonoBehaviour
{
    public int RangoMin = 5;
    public float speed = 3;

    public GameObject bulletPrefab;
    public Transform bulletPos;

    private Rigidbody2D rb;
    private Transform jugador; // Referencia al transform del jugador

    public float delayDisparo = 2f;
    private float tiempoUltimoDisparo;

    [SerializeField]
    private bool TieneVision = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jugador = GameObject.FindGameObjectWithTag("Player").transform; // Busca el jugador por su etiqueta "Player"
        tiempoUltimoDisparo = Time.time; // Inicializa el tiempo del último disparo
    }

    // Update is called once per frame
    void Update()
    {
        RayCast();
        ModosDeAtaque();
    }

    void TrackingJugador()
    {
        // Calcula la dirección hacia la que el enemigo debe moverse (hacia el jugador)
        Vector2 direccion = (jugador.position - transform.position).normalized;

        // Calcula la velocidad de movimiento
        Vector2 velocidadMovimiento = direccion * speed;

        // Aplica la velocidad al Rigidbody del enemigo
        rb.velocity = velocidadMovimiento;
    }

    void RayCast()
    {
        // crear máscara de capa para afectar solo a las colisiones que queremos
        int mascara = LayerMask.GetMask("Roca", "Pared","Player"); //~ Invertir la mascara (Pasa de incluir las mascaras a excluirlas)

        RaycastHit2D ray = Physics2D.Raycast(transform.position, jugador.transform.position - transform.position,
            50, mascara); // trazo el raycast
     

        if(ray.collider != null)
        {
            TieneVision = ray.collider.CompareTag("Player");
            Debug.Log(ray.collider.name);
            if(TieneVision) 
            {
                Debug.DrawRay(transform.position, jugador.position - transform.position, Color.green);
            }
            
            else
            {
                Debug.DrawRay(transform.position, jugador.position - transform.position, Color.red);
            }
        }



     
    }

    void ModosDeAtaque()
    {
        float distancia = Vector2.Distance(transform.position, jugador.position);

        if (distancia > RangoMin)
        {
            if (TieneVision)
            {
                if (Time.time - tiempoUltimoDisparo > delayDisparo)
                {
                    Disparar();
                    tiempoUltimoDisparo = Time.time; // Actualiza el tiempo del último disparo
                }
                rb.velocity = Vector2.zero; // Detiene el movimiento del murciélago cuando dispara
            }
            else
            {
                TrackingJugador();
            }
        }

        else if (distancia <= RangoMin)

        {
            TrackingJugador();
            CancelInvoke();
        }
    }


    void Disparar()
    {
        Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity);
    }
}
