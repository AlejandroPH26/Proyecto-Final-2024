using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ETopo : MonoBehaviour

{
    public float velocidadMovimiento = 2f; // Velocidad de movimiento
    public float tiempoCambioDireccion = 3f; // Tiempo en segundos para cambiar de direcci�n
    public LayerMask obstaculoMask; // M�scara de capa para detectar obst�culos
    public float distanciaRaycast = 1f; // Distancia del raycast para detectar obst�culos

    private Rigidbody2D rb;
    private Vector2 direccionActual; // Direcci�n actual del movimiento
    private float timer; // Temporizador para cambiar de direcci�n

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = tiempoCambioDireccion; // Inicializa el temporizador
        CambiarDireccion();
    }

    void Update()
    {
        // Realiza un raycast en la direcci�n actual para detectar obst�culos
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direccionActual, distanciaRaycast, obstaculoMask);
        if (hit.collider != null)
        {
            // Si se detecta un obst�culo, cambia de direcci�n
            CambiarDireccion();
        }

        // Aplica la velocidad de movimiento
        rb.velocity = direccionActual * velocidadMovimiento;
    }

    void CambiarDireccion()
    {
        // Genera una direcci�n aleatoria
        int direccionX = Random.Range(0, 2) * 2 - 1; // Valor aleatorio de -1 o 1
        int direccionY = Random.Range(0, 2) * 2 - 1; // Valor aleatorio de -1 o 1
        direccionActual = new Vector2(direccionX, direccionY).normalized;
    }

    // Detecta colisiones con obst�culos, paredes, etc.
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si colisiona con un obst�culo, cambia de direcci�n
        if (collision.gameObject.CompareTag("Roca") || collision.gameObject.CompareTag("Pared") || collision.gameObject.CompareTag("Topo"))
        {
            CambiarDireccion();
        }
    }
}
