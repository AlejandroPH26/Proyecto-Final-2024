using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{

    [Tooltip("Velocidad del jugador en unidades de unity / segundo")]
    public float speed = 2f;
    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveDown = KeyCode.S;
    public KeyCode moveRight = KeyCode.D;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        InputJugador();
    }
    private void InputJugador()
    {
        Vector2 aux = Vector2.zero; // Declaramos el Vector2 para el movimiento y lo identificamos como aux la variable
        if (Input.GetKey(moveUp)) // Comprobamos que se está pulsando la tecla W
        {
            // Nos desplazamos (sumamos movimiento) hacia arriba (eje y = 1), multiplicamos por deltatime
            // para que el movimiento no dependa del framerate.
            aux.y = 1;
           
        }
        if (Input.GetKey(moveDown))
        {
            aux.y = -1;
        }
        if (Input.GetKey(moveLeft))
        {
            aux.x = -1;

        }
        if (Input.GetKey(moveRight))
        {
            aux.x = 1;
        }
        
        rb.velocity = aux.normalized * speed;
    }

}
