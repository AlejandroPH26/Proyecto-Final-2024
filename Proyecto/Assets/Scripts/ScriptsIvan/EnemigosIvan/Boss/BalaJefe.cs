using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaJefe : MonoBehaviour
{

    public float velocidad = 7f;
    public int daño = 10;

  
    private GameManagerHats gm;

    void Start()
        {
            gm = FindObjectOfType<GameManagerHats>();
            // Generar una dirección de movimiento aleatoria
            Vector2 direccion = Random.insideUnitCircle.normalized;

            // Obtener el componente Rigidbody2D de la bala
            Rigidbody2D rb = GetComponent<Rigidbody2D>();

            // Asignar velocidad a la bala
            rb.velocity = direccion * velocidad;
        }
   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            Destroy(this.gameObject);
           
            
                gm.RestarVidas();
            

        }

        else if (collision.collider.CompareTag("Pared") || collision.collider.CompareTag("Boss"))
        {
            Destroy(this.gameObject);
        }
    }
}
