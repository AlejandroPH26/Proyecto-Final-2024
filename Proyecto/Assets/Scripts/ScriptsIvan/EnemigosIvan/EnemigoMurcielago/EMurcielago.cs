using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMurcielago : MonoBehaviour
{
    public int RangoMin = 5;
    public float speed = 3;

    public GameObject bulletPrefab;
    public Transform bulletPos;

    private Rigidbody2D rb;
    private Transform jugador; // Referencia al transform del jugador

    public float delayDisparo = 2f;
    private float tiempoUltimoDisparo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jugador = GameObject.FindGameObjectWithTag("Player").transform; // Busca el jugador por su etiqueta "Player"
        tiempoUltimoDisparo = Time.time; // Inicializa el tiempo del último disparo
    }

    // Update is called once per frame
    void Update()
    {
        CalcularDistancia();
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

    void CalcularDistancia()
    {
        float distancia = Vector2.Distance(transform.position, jugador.position);

        if (distancia > RangoMin)
        {
            if (Time.time - tiempoUltimoDisparo > delayDisparo)
            {
                Disparar();
                tiempoUltimoDisparo = Time.time; // Actualiza el tiempo del último disparo
            }
            rb.velocity = Vector2.zero; // Detiene el movimiento del murciélago cuando dispara
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