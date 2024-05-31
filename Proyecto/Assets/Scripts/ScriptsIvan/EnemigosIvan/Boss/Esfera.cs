using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esfera : MonoBehaviour


{
    private Transform jugador;
    public float baseSpeed = 3f;
    public float flankSpeedMultiplier = 0.5f; // Reducción de velocidad durante el flanqueo
    public float flankDistance = 3f; // Distancia a la que el enemigo se coloca alrededor del jugador
    public float detectionRange = 10f;
    public int damage = 1; // Daño causado al jugador
    public GameManagerHats gm;

    private float speed;
    private Vector2 flankOffset;

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        gm = FindObjectOfType<GameManagerHats>();
        speed = baseSpeed + Random.Range(-0.5f, 0.5f); // Añadir variación aleatoria en la velocidad
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, jugador.position);

        if (distanceToPlayer < detectionRange)
        {
            PerseguirJugador();
        }
    }

    void PerseguirJugador()
    {
        Vector2 targetPosition = (Vector2)jugador.position;
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        // Calcular la posición relativa de la esfera con respecto al jugador
        Vector2 relativePosition = targetPosition - (Vector2)transform.position;

        // Ajustar el offset de flanqueo según la posición relativa
        flankOffset = relativePosition.normalized * flankDistance;

        float distanceToPlayer = Vector2.Distance(transform.position, targetPosition);
        float adjustedSpeed = speed;

        // Reducir la velocidad durante el flanqueo
        if (distanceToPlayer < flankDistance)
        {
            adjustedSpeed *= flankSpeedMultiplier;
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPosition + flankOffset, adjustedSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Reducir las vidas del jugador
            gm.RestarVidas();
        }
    }
}

