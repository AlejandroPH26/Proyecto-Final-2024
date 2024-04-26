using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ETopo : MonoBehaviour

{
    public float velocidadMovimiento = 2f; // Velocidad de movimiento
    public float tiempoCambioDireccion = 3f; // Tiempo en segundos para cambiar de dirección
    public LayerMask obstaculoMask; // Máscara de capa para detectar obstáculos
    public float distanciaRaycast = 1f; // Distancia del raycast para detectar obstáculos

    private Rigidbody2D rb;
    private Vector2 direccionActual; // Dirección actual del movimiento
    private float timer; // Temporizador para cambiar de dirección

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = tiempoCambioDireccion; // Inicializa el temporizador
        CambiarDireccion();
    }

    void Update()
    {
        // Realiza un raycast en la dirección actual para detectar obstáculos
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direccionActual, distanciaRaycast, obstaculoMask);
        if (hit.collider != null)
        {
            // Si se detecta un obstáculo, cambia de dirección
            CambiarDireccion();
        }

        // Aplica la velocidad de movimiento
        rb.velocity = direccionActual * velocidadMovimiento;
    }

    void CambiarDireccion()
    {
        // Genera una dirección aleatoria
        int direccionX = Random.Range(0, 2) * 2 - 1; // Valor aleatorio de -1 o 1
        int direccionY = Random.Range(0, 2) * 2 - 1; // Valor aleatorio de -1 o 1
        direccionActual = new Vector2(direccionX, direccionY).normalized;
    }

    // Detecta colisiones con obstáculos, paredes, etc.
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si colisiona con un obstáculo, cambia de dirección
        if (collision.gameObject.CompareTag("Roca") || collision.gameObject.CompareTag("Pared") || collision.gameObject.CompareTag("Topo"))
        {
            CambiarDireccion();
        }
    }
}
