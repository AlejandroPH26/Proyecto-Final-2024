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

    public MovimientoPrueba Player;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = FindObjectOfType<MovimientoPrueba>(); // Busca el script del jugador 
                                                       // Al iniciar, la vida actual es igual a la vida maxima

        if (Player != null)
        {

            int randomAxis = Random.Range(0, 2);
            if (randomAxis == 0)
            {
                direccionActual = Vector2.right; // Comienza moviéndose hacia la derecha
            }
            else
            {
                direccionActual = Vector2.up; // Comienza moviéndose hacia arriba
            }

            timer = cambiarDireccionTimer; // Inicializa el temporizador

        }

        else if (Player = null)
        {
            rb.velocity = Vector2.zero;
        }

    }

    void Update()
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
            }
            else
            {
                direccionActual = Vector2.left; // Izquierda
            }
        }
        else
        {
            // Si se elige el eje Y
            int randomDirection = Random.Range(0, 2); // 0 o 1, para arriba o abajo
            if (randomDirection == 0)
            {
                direccionActual = Vector2.up; // Arriba
            }
            else
            {
                direccionActual = Vector2.down; // Abajo
            }
        }
    }

    // Detecta colisiones con obstáculos, topes y paredes // Cambio on collision Enter por Stay
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Roca") || collision.gameObject.CompareTag("Topo") || collision.gameObject.CompareTag("Pared"))
        {
            // Cambia la dirección de forma aleatoria entre los ejes X e Y, sin diagonales
            CambiarDireccion();
            Debug.Log("colisionDetectada");
        }

       
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CambiarDireccion();
            Player.DamageTaken(daño);
            Debug.Log("DañoRecibidoJugador");

        }
    }
  
}

