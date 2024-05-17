using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMinero : MonoBehaviour

{
    // Estadisticas
    public float speed;
    public int da�oPorIntervalo = 10;
    private float tiempoUltimoDa�o;

    // Componentes
    private Transform jugador; // Referencia al transform del jugador
    private Rigidbody2D rb;

    // Jugador
    private JugadorV1 Player;
    private GameManagerHats gm;

    // Animator
    private Animator animator;

    public MusicManager mm;
    public AudioClip da�oMinero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = FindObjectOfType<JugadorV1>(); // Busca el script del jugador 
        jugador = GameObject.FindGameObjectWithTag("Player").transform; // Busca el jugador por su etiqueta "Player"
        gm = FindObjectOfType<GameManagerHats>();
        animator = GetComponent<Animator>();
        mm = MusicManager.instance;
    }

    void Update()
    {
        if (jugador != null)
        {
            Chase();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        ComprobacionDireccion();
    }

    public void Chase()
    {
        // Calcula la direcci�n hacia la que el enemigo debe moverse (hacia el jugador)
        Vector2 direccion = (jugador.position - transform.position).normalized;

        // Calcula la velocidad de movimiento
        Vector2 velocidadMovimiento = direccion * speed;

        // Aplica la velocidad al Rigidbody del enemigo
        rb.velocity = velocidadMovimiento;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstaculo"))
        {
            EvadirObstaculo();
        }
        else if (collision.gameObject.CompareTag("Player"))
        {         
            gm.RestarVidas();
            mm.PlaySFX(da�oMinero);
        }
    }

    private void EvadirObstaculo()
    {
        // Calcula una direcci�n perpendicular a la direcci�n hacia el jugador
        Vector2 direccionPerpendicular = Vector2.Perpendicular(rb.velocity).normalized;

        // Calcula una direcci�n alternativa para evitar el obst�culo
        Vector2 nuevaDireccion = rb.velocity + direccionPerpendicular * Mathf.Sign(Random.Range(-1f, 1f));

        // Aplica la nueva direcci�n de movimiento
        rb.velocity = nuevaDireccion.normalized * speed;
    }

    private void ComprobacionDireccion()
    {
        Vector2 velocidad = rb.velocity;

        if (velocidad.x > 0)
        {
            // El enemigo se est� moviendo hacia la derecha
            Debug.Log("Se est� moviendo hacia la derecha");
        }
        else if (velocidad.x < 0)
        {
            // El enemigo se est� moviendo hacia la izquierda
            Debug.Log("Se est� moviendo hacia la izquierda");
        }

        if (velocidad.y > 0)
        {
            // El enemigo se est� moviendo hacia arriba
            Debug.Log("Se est� moviendo hacia arriba");
        }
        else if (velocidad.y < 0)
        {
            // El enemigo se est� moviendo hacia abajo
            Debug.Log("Se est� moviendo hacia abajo");
        }
    }
}