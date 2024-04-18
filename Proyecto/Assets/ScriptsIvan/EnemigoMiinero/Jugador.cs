using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;

    public int vidaMax = 100;
    public int vidaActual;
    
    // Start is called before the first frame update
    public void Start()
    {
         rb = GetComponent<Rigidbody2D>();
        vidaActual = vidaMax;
    }

    // Update is called once per frame
    void Update()
    {
        movimiento();
    }

    void movimiento()
    {
        if(Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-1, 0) * speed;
               
        }

        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(1, 0) * speed;
           
        }

        else if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector2(0, 1) * speed;
            
        }

        else if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector2(0, -1) * speed;
           
        }

        else rb.velocity = Vector2.zero;
    }

    public void DamageTaken(int cantidad)
   
    {
        vidaActual -= cantidad;

        if (vidaActual <= 0)
        {
            Muerte();
        }
    }

    public void Muerte()
   
    {
        Destroy(this.gameObject);
    }
}
