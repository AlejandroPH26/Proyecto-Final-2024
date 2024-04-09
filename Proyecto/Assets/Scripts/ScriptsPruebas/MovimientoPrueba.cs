using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPrueba : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento del jugador
    private Rigidbody2D rb; // Referencia al Rigidbody2D del jugador

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtener el Rigidbody2D del jugador
    }

    // Update se llama una vez por frame
    void Update()
    {
        // Obtener la entrada del teclado
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calcular el movimiento basado en la entrada del teclado
        Vector2 movement = new Vector2(horizontalInput, verticalInput) * speed;

        // Aplicar el movimiento al Rigidbody2D del jugador
        rb.velocity = movement;
    }
}
