using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoV1 : MonoBehaviour
{
    // Velocidad base para los enemigos
    public float velocidadMurcielago = 3f;
    public float velocidadBase = 5f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void CongelarEnemigo()
    {
        // Detener el movimiento instantáneamente
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
}
