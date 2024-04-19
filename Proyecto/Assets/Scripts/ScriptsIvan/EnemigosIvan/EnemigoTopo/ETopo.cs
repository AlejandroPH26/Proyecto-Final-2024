using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ETopo : MonoBehaviour
{
    public float VelocidadMovimiento = 2f; // Velocidad de movimiento
    public float CambiarDireccionTimer = 3f; // Tiempo en segundos para cambiar de direcci�n
   

    private Rigidbody2D rb;
    private Vector2 DireccionActual; // Direcci�n actual del movimiento
    private float timer; // Temporizador para cambiar de direcci�n

    public int vidaMax = 100; 
    public int vidaActual;
    public int da�o = 10;

    public Jugador Player;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = FindObjectOfType<Jugador>(); // Busca el script del jugador 
        vidaActual = vidaMax; // Al iniciar, la vida actual es igual a la vida maxima�
        DireccionActual = Vector2.right; // Comienza movi�ndose hacia la derecha
        timer = CambiarDireccionTimer; // Inicializa el temporizador
    }

    void Update()
    {
        // Actualiza el temporizador
        timer -= Time.deltaTime;

        // Si el temporizador llega a cero, cambia la direcci�n
        if (timer <= 0f)
        {
            CambiarDireccion();
            timer = CambiarDireccionTimer; // Reinicia el temporizador
        }

        // Aplica la velocidad de movimiento
        rb.velocity = DireccionActual * VelocidadMovimiento;
    }

    void CambiarDireccion()
    {
        // Cambia la direcci�n
        DireccionActual = new Vector2(DireccionActual.y, -DireccionActual.x);
    }

    // Detecta colisiones con obst�culos, paredes, otros enemigos y el jugador
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstaculo") || collision.gameObject.CompareTag("Pared") || collision.gameObject.CompareTag("Topo") || collision.gameObject.CompareTag("Minero"))
        {
            // Cambia la direcci�n para evitar el obst�culo
            CambiarDireccion();
           // Debug.Log("CambioDireccion");
        }

        if (collision.gameObject.CompareTag("Player"))
        {

            Player.DamageTaken(da�o);
          //  Debug.Log("Da�oRecibidoJugador");
        }

        if (collision.gameObject.CompareTag("BalaJugador"))
        {
            //Debug.Log("da�orecibidoTopo");
            DamageTaken(20);

            //Destroy(collision.gameObject); // Destruye la bala 


            if (vidaActual <= 0)
            {
                Muerte();
            }
        }
    }

    public void Muerte()

    {
        //Eanimator.Play("TOPO_DEATH");
        Destroy(this.gameObject);
    }

    public void DamageTaken(int cantidad)
    {
        vidaActual = vidaActual - cantidad;
    }

}
