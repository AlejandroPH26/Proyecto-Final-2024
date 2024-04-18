using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    //Estadisticas
    private int vidaActual;
    private int vidaMax = 100;
    public float speed;

    public int dañoPorIntervalo = 10;
    private float tiempoUltimoDaño;

    //Componentes
    private Transform jugador; // Referencia al transform del jugador
    private Rigidbody2D rb;

    //Jugador
    private Jugador Player;
    


    //private Animator Eanimator;

    void Start()
   
    {

        rb = GetComponent<Rigidbody2D>();

        Player = FindObjectOfType<Jugador>(); // Busca el script del jugador 

        jugador = GameObject.FindGameObjectWithTag("Player").transform; // Busca el jugador por su etiqueta "Player"

        vidaActual = vidaMax; // Al iniciar, la vida actual es igual a la vida maxima

        //animator = GetComponent<Animator>();
    }
        
    

    void Update()
    {
        if (jugador != null)
        {
            Chase();


        }

        else if(jugador = null) 
        {
            rb.velocity = Vector2.zero;
        }

    }
     

    public void Muerte()
   
    {
        //Eanimator.Play("MINERO_DEATH");
        Destroy(this.gameObject);
        
      
    }

    public void Chase()
    {

        // Calcula la dirección hacia la que el enemigo debe moverse (hacia el jugador)
        Vector2 direccion = (jugador.position - transform.position).normalized;

        // Calcula la velocidad de movimiento
        Vector2 velocidadMovimiento = direccion * speed;

        // Aplica la velocidad al Rigidbody del enemigo
        rb.velocity = velocidadMovimiento;

       
    }

    public void DamageTaken(int cantidad)
    {
        vidaActual = vidaActual - cantidad;
    }


    private void OnCollisionStay2D(Collision2D collision)

    {

        if (collision.gameObject.CompareTag("Obstaculo"))
        {
            EvadirObstaculo();
        }

       else  if (collision.gameObject.CompareTag("Player"))
        {
            if(Time.time - tiempoUltimoDaño > 1.5f)
            {
                Player.DamageTaken(dañoPorIntervalo);
                Debug.Log("DañoRecibido");
                tiempoUltimoDaño = Time.time;
            }

        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BalaJugador"))
        {
            Debug.Log("dañorecibido");
            DamageTaken(20);
           
            //Destroy(collision.gameObject); // Destruye la bala 
          

            if (vidaActual <= 0)
            {
                Muerte();
            }
          
        }
    }

    private void EvadirObstaculo()
    {
        // Calcula una dirección perpendicular a la dirección hacia el jugador
        Vector2 direccionPerpendicular = Vector2.Perpendicular(rb.velocity).normalized;

        // Calcula una dirección alternativa para evitar el obstáculo
        Vector2 nuevaDireccion = rb.velocity + direccionPerpendicular * Mathf.Sign(Random.Range(-1f, 1f));

        // Aplica la nueva dirección de movimiento
        rb.velocity = nuevaDireccion.normalized * speed;
    }


}
