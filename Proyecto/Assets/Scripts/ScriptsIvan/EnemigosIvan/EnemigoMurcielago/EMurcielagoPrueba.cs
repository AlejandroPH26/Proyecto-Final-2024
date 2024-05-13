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
    public  float tiempoUltimoDaño;

    [SerializeField]
    private bool TieneVision = false;

    private Animator animator;

    private EnemigosComun enemy; //Referencia al script EnemigosComun

    private GameManagerHats gm;

    void Start()
    {
        // Obtener la referencia al componente Enemigo
       
        animator = GetComponent<Animator>();
        

        rb = GetComponent<Rigidbody2D>();
        //jugador = GameObject.FindGameObjectWithTag("Player").transform; // Busca el jugador por su etiqueta "Player"
        jugador = FindObjectOfType<JugadorV1>();
        tiempoUltimoDisparo = Time.time; // Inicializa el tiempo del último disparo

        enemy = GetComponent<EnemigosComun>();

        gm = GameManagerHats.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.activo) // Si el enemigo está activo
        {
            RayCast();
            ModosDeAtaque();
        }
        else // En caso contrario, su velocidad es 0
        {
            rb.velocity = Vector3.zero;
        }
    }

    void TrackingJugador()
    {
        // Calcula la dirección hacia la que el enemigo debe moverse (hacia el jugador)
        Vector2 direccion = (jugador.transform.position - transform.position).normalized;

        // Calcula la velocidad de movimiento
        Vector2 velocidadMovimiento = direccion * speed;

        // Aplica la velocidad al Rigidbody del enemigo
        rb.velocity = velocidadMovimiento;
    }

    void RayCast()
    {
        // crear máscara de capa para afectar solo a las colisiones que queremos
        int mascara = LayerMask.GetMask("Roca", "Pared","Player","HeadPlayer"); //~ Invertir la mascara (Pasa de incluir las mascaras a excluirlas)

        RaycastHit2D ray = Physics2D.Raycast(transform.position, jugador.transform.position - transform.position,
            100, mascara); // trazo el raycast
     

        if(ray.collider != null)
        {
            // Debug.Log(ray.collider.tag);
            TieneVision = ray.collider.CompareTag("Player") || ray.collider.CompareTag("HeadPlayer");
            // Debug.Log(ray.collider.name);
            if(TieneVision) 
            {
                Debug.DrawRay(transform.position, jugador.transform.position - transform.position, Color.green);
            }
            
            else
            {
                Debug.DrawRay(transform.position, jugador.transform.position - transform.position, Color.red);
            }
            Debug.DrawLine(transform.position, ray.point, Color.blue);

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
                    tiempoUltimoDisparo = Time.time; // Actualiza el tiempo del último disparo
                }
                rb.velocity = Vector2.zero; // Detiene el movimiento del murciélago cuando dispara
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
                gm.RestarVidas();
                Debug.Log("DañoAlJugador");
                       
        }
    }

}
