using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class EMurcielagoPrueba : MonoBehaviour
{
    public int RangoMin = 5;
    public float speed;

    public GameObject bulletPrefab;
    public Transform bulletPos;

    private Rigidbody2D rb;
    //private Transform jugador; // Referencia al transform del jugador
    public JugadorV1 jugador;
  

    public float delayDisparo = 2f;
    private float tiempoUltimoDisparo;
    public  float tiempoUltimoDa�o;

    [SerializeField]
    private bool TieneVision = false;

    private Animator animator;

   
    void Start()
    {
        // Obtener la referencia al componente Enemigo
       
        animator = GetComponent<Animator>();
        

        rb = GetComponent<Rigidbody2D>();
        //jugador = GameObject.FindGameObjectWithTag("Player").transform; // Busca el jugador por su etiqueta "Player"
        jugador = FindObjectOfType<JugadorV1>();
        tiempoUltimoDisparo = Time.time; // Inicializa el tiempo del �ltimo disparo
    }

    // Update is called once per frame
    void Update()
    {
        RayCast();
        ModosDeAtaque();
    }

    void TrackingJugador()
    {
        // Calcula la direcci�n hacia la que el enemigo debe moverse (hacia el jugador)
        Vector2 direccion = (jugador.transform.position - transform.position).normalized;

        // Calcula la velocidad de movimiento
        Vector2 velocidadMovimiento = direccion * speed;

        // Aplica la velocidad al Rigidbody del enemigo
        rb.velocity = velocidadMovimiento;
    }

    void RayCast()
    {
        // crear m�scara de capa para afectar solo a las colisiones que queremos
        int mascara = LayerMask.GetMask("Roca", "Pared","Player"); //~ Invertir la mascara (Pasa de incluir las mascaras a excluirlas)

        RaycastHit2D ray = Physics2D.Raycast(transform.position, jugador.transform.position - transform.position,
            50, mascara); // trazo el raycast
     

        if(ray.collider != null)
        {
            TieneVision = ray.collider.CompareTag("Player");
            // Debug.Log(ray.collider.name);
            if(TieneVision) 
            {
                Debug.DrawRay(transform.position, jugador.transform.position - transform.position, Color.green);
            }
            
            else
            {
                Debug.DrawRay(transform.position, jugador.transform.position - transform.position, Color.red);
            }
        }

    }

    void ModosDeAtaque()
    {
        float distancia = Vector2.Distance(transform.position, jugador.transform.position);

        if (distancia > RangoMin)
        {
            if (TieneVision)
            {
                if (Time.time - tiempoUltimoDisparo > delayDisparo)
                {
                    animator.SetBool("EstaAtacando", true); // Activo la animacion de atacar atraves de un bool creado en el animator
                    Disparar();
                    tiempoUltimoDisparo = Time.time; // Actualiza el tiempo del �ltimo disparo
                }
                rb.velocity = Vector2.zero; // Detiene el movimiento del murci�lago cuando dispara
            }
            else
            {
                animator.SetBool("EstaAtacando", false); // Desctivo la animacion de atacar atraves de un bool creado en el animator
                TrackingJugador();
            }
        }

        else if (distancia <= RangoMin)

        {
            animator.SetBool("EstaAtacando", false); // Desctivo la animacion de atacar atraves de un bool creado en el animator
            TrackingJugador();
            CancelInvoke();
        }
    }


    void Disparar()
    {
        Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity);
    }

    private void OnCollisionStay2D(Collision2D collision)

    {

        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time - tiempoUltimoDa�o > 1.5f)
            {
                jugador.DamageTaken(20);
                Debug.Log("Da�oAlJugador");
                tiempoUltimoDa�o = Time.time;
            }
        }
    }

}