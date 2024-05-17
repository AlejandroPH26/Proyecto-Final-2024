using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMineroPrueba : MonoBehaviour
{
    // Estad�sticas
    public float speed;
    public int da�oPorIntervalo = 10;
    private float tiempoUltimoDa�o;

    // Componentes
    private Transform jugador; // Referencia al transform del jugador
    private Rigidbody2D rb;

    // Jugador
    private JugadorV1 Player;
    private GameManagerHats gm;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = FindObjectOfType<JugadorV1>(); // Busca el script del jugador 
        jugador = GameObject.FindGameObjectWithTag("Player").transform; // Busca el jugador por su etiqueta "Player"
        gm = FindObjectOfType<GameManagerHats>();
    }

    void Update()
    {
        if (jugador != null)
        {
            Chase();
        }
        else if (jugador == null)
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void Chase()
    {
        // Calcula la direcci�n hacia el jugador
        Vector2 direccion = (jugador.position - transform.position).normalized;

        // Lanza un Raycast hacia el jugador
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direccion, Mathf.Infinity);

        // Si el Raycast detecta un obst�culo entre el enemigo y el jugador
        if (hit.collider != null && hit.collider.CompareTag("Obstaculo"))
        {
            EvadirObstaculo();
        }
        else
        {
            // Calcula la velocidad de movimiento
            Vector2 velocidadMovimiento = direccion * speed;
            // Aplica la velocidad al Rigidbody del enemigo
            rb.velocity = velocidadMovimiento;
        }
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
                gm.RestarVidas();
                Debug.Log("Da�oAlJugador");
                tiempoUltimoDa�o = Time.time;
            }
        }
    }

    private void EvadirObstaculo()
    {
        // Calcula una direcci�n perpendicular a la direcci�n actual del movimiento
        Vector2 direccionPerpendicular = Vector2.Perpendicular(rb.velocity).normalized;

        // Calcula una direcci�n alternativa para evitar el obst�culo
        Vector2 nuevaDireccion = rb.velocity + direccionPerpendicular * Mathf.Sign(Random.Range(-1f, 1f));

        // Aplica la nueva direcci�n de movimiento
        rb.velocity = nuevaDireccion.normalized * speed;
    }
}