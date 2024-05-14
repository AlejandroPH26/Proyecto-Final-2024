using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaPayaso : MonoBehaviour
{
    public Direction dirBala;
    public float speed = 3f;
    private Rigidbody2D rb;
    private Vector2[] direcciones = { new Vector2(0, 1), new Vector2(0, -1), new Vector2(-1, 0), new Vector2(1, 0) };

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void asignarDireccion(Direction dir)
    {
        // Asignar dirBala     
        dirBala = dir;
        // Modificar rb.velocity
        // direcciones para usar la lista, el corchete [] es para seleccionar la posici�n en la lista,
        // que dirBala es int y no otro tipo de dato (int),
        // mutiplicarlo por speed para indicarle la velocidad
        rb.velocity = direcciones[(int)dirBala] * speed;
    }
}
