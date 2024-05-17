using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMinero : MonoBehaviour
{
    //Estadisticas
    public float speed;

    public int da�oPorIntervalo = 10;
    private float tiempoUltimoDa�o;

    //Componentes
    private Transform jugador; // Referencia al transform del jugador
    private Rigidbody2D rb;

    //Jugador
    private MovimientoPrueba Player;

    public MusicManager mm;

    public AudioClip da�oMinero;



    //private Animator Eanimator;

    void Start()

    {

        rb = GetComponent<Rigidbody2D>();

        Player = FindObjectOfType<MovimientoPrueba>(); // Busca el script del jugador 

        jugador = GameObject.FindGameObjectWithTag("Player").transform; // Busca el jugador por su etiqueta "Player"

        mm = MusicManager.instance;

        // Al iniciar, la vida actual es igual a la vida maxima

        //animator = GetComponent<Animator>();
    }



    void Update()
    {
        if (jugador != null)
        {
            Chase();


        }

        else if (jugador = null)
        {
            rb.velocity = Vector2.zero;
        }

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
            if (Time.time - tiempoUltimoDa�o > 1.5f)
            {
                Player.DamageTaken(da�oPorIntervalo);
                Debug.Log("Da�oAlJugador");
                tiempoUltimoDa�o = Time.time;
            }
                mm.PlaySFX(da�oMinero);
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

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


}
