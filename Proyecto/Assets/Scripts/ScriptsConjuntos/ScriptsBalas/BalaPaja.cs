using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaPaja : MonoBehaviour
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
        dirBala = dir;
        rb.velocity = direcciones[(int)dirBala] * speed;
    }
}
