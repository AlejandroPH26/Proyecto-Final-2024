using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ETopo2 : MonoBehaviour
{
    public float velocidadMovimiento = 2f; // Velocidad de movimiento
    public float cambiarDireccionTimer = 3f; // Tiempo en segundos para cambiar de dirección
    public int daño = 20;
   
   

    private Rigidbody2D rb;
    private Vector2 direccionActual; // Dirección actual del movimiento
    private float timer; // Temporizador para cambiar de dirección

    public JugadorV1 Player;
    private EnemigosComun enemy; //Referencia al script EnemigosComun
    private Direction curDir;

    public Animator animator;

    public GameManagerHats gm;

    public bool puedeCambiarDireccion = true;
    [SerializeField]
    private float TiempoEntreCambio = 3f;


    void Start()
    {
        Player = FindObjectOfType<JugadorV1>(); // Busca el script del jugador 

        if (Player != null)
          
        {
            rb = GetComponent<Rigidbody2D>();   
            gm = GameManagerHats.instance;
            enemy = GetComponent<EnemigosComun>();
            CambiarDireccion();
            timer = cambiarDireccionTimer; // Inicializa el temporizador
        }

        else 
           
        {
            rb.velocity = Vector2.zero;         
        }

    }

    void Update()
    {
        if (enemy.activo)
        {
            // Actualiza el temporizador
            timer -= Time.deltaTime;

            // Si el temporizador llega a cero, cambia la dirección
            if (timer <= 0f)
            {
                CambiarDireccion();
                timer = cambiarDireccionTimer; // Reinicia el temporizador
            }

            // Aplica la velocidad de movimiento
            rb.velocity = direccionActual * velocidadMovimiento;
        }
        else // En caso contrario, su velocidad es 0
        {
            rb.velocity = Vector3.zero;
        }



    }

    void CambiarDireccion()
    {
        // Genera una dirección aleatoria entre los ejes X e Y, sin diagonales
        int randomAxis = Random.Range(0, 2);
        if (randomAxis == 0)
        {
            // Si se elige el eje X
            int randomDirection = Random.Range(0, 2); // 0 o 1, para derecha o izquierda
            if (randomDirection == 0)
            {
                direccionActual = Vector2.right; // Derecha
                animator.Play("TOPO_RIGHT");
               
            }
            else
            {
                direccionActual = Vector2.left; // Izquierda
                animator.Play("TOPO_LEFT");

            }
        }
        else
        {
            // Si se elige el eje Y
            int randomDirection = Random.Range(0, 2); // 0 o 1, para arriba o abajo
            if (randomDirection == 0)
            {
              
                direccionActual = Vector2.up; // Arriba
                animator.Play("TOPO_IDLE");
            }
            else
            {
                direccionActual = Vector2.down; // Abajo
                animator.Play("TOPO_DOWN");

            }
        }
    }


    // Crear Metodo Cambio de direccion aleatoria que pueda escoger todas menos la Actual
    





    // Detecta colisiones con obstáculos, topes y paredes // Cambio on collision Enter por Stay
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Roca") || collision.gameObject.CompareTag("Enemigo") || collision.gameObject.CompareTag("Pared"))
        {
            
                // Cambia la dirección de forma aleatoria entre los ejes X e Y, sin diagonales
                CambiarDireccion();
                // Debug.Log("colisionDetectada");
            
            
        }

       
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CambiarDireccion();      
            gm.RestarVidas(); // Llamo al metodo restar vidas del gameManager
        }
    }

}

