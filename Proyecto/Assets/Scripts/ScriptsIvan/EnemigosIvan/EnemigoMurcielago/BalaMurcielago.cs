using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaMurcielago : MonoBehaviour

{
    public float velocidad = 10f; // Velocidad de la bala
    private Rigidbody2D rb; 
    public JugadorV1 Player;
    public int Daño = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
     
        Player = FindObjectOfType<JugadorV1>();
        Vector2 direccion = (Player.transform.position - transform.position).normalized;
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

        /*
        else if (collision.gameObject.CompareTag("Player"))
        {
            Player.DamageTaken(Daño);
            Destroy(this.gameObject);
            //daño al jugador 
         
        }
        */

        else if(collision.gameObject.CompareTag("BalaJugador"))
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }


    //Añadir al scrip de la bala jugador que si choca con un obejto con el tag bala.enemigo o .murcielago se destruye ambos objetos

    // CODIGO DE PRUEBA ALEJANDRO

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Acceder al GameManager y restarle vida al jugador
            GameManagerHats.instance.RestarVidas();
            Player.DamageTaken(Daño);

            // Destruir la bala enemiga cuando colisiona con el jugador
            Destroy(gameObject);
        }
    }
}

