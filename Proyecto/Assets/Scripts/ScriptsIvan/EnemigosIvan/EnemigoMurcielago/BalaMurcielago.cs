using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaMurcielago : MonoBehaviour

{
    public float velocidad = 10f; // Velocidad de la bala
    private Rigidbody2D rb;
    private GameObject jugador;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jugador = GameObject.FindGameObjectWithTag("Player");
        Vector2 direccion = (jugador.transform.position - transform.position).normalized;
        rb.velocity = direccion * velocidad;
    }

    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pared") || collision.gameObject.CompareTag("Roca"))
        {
            Destroy(this.gameObject);
        }

        else if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            //daño al jugador 
        }
    }
}

