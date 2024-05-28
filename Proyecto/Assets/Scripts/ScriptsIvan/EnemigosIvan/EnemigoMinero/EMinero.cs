using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EMinero : MonoBehaviour

{
    // Estadisticas
    public float speed;
   
    // Componentes
    private Transform jugador; // Referencia al transform del jugador
    private Rigidbody2D rb;

    // Jugador
    private JugadorV1 Player;
    private GameManagerHats gm;

    // Animator
    private Animator animator;

    public MusicManager mm;
    public AudioClip dañoMinero;

    public Transform MineroTransform;
    private EnemigosComun enemy; //Referencia al script EnemigosComun

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = FindObjectOfType<JugadorV1>(); // Busca el script del jugador 
        jugador = GameObject.FindGameObjectWithTag("Player").transform; // Busca el jugador por su etiqueta "Player"
        gm = FindObjectOfType<GameManagerHats>();
        animator = GetComponent<Animator>();
        mm = MusicManager.instance;
        enemy = GetComponent<EnemigosComun>();

    }

    void Update()
    {
        if (enemy.activo && jugador != null)
        {
            Chase();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        ComprobacionDireccion();
    }

    public void Chase()
    {
        // Calcula la dirección hacia la que el enemigo debe moverse (hacia el jugador)
        Vector2 Dir = (jugador.position - transform.position).normalized;

        // Calcula la velocidad de movimiento
        Vector2 velocidadMovimiento = Dir * speed;

        // Aplica la velocidad al Rigidbody del enemigo
        rb.velocity = velocidadMovimiento;

        ComprobacionDireccion();


    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstaculo"))
        {
            EvadirObstaculo();
        }
        else if (collision.gameObject.CompareTag("Player"))
        {         
            gm.RestarVidas();
            mm.PlaySFX(dañoMinero);
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

    private void ComprobacionDireccion()
    {
        Vector2 Dir = (jugador.position - transform.position).normalized;


        if (Mathf.Abs(Dir.x) > Mathf.Abs(Dir.y))
        {
            if (Dir.x > 0)
            {
                //Debug.Log("voy a la derecha");
                animator.Play("MINERO_RIGHT");
            }

            else
            {
                //Debug.Log("voy a la izquierda");
                animator.Play("MINERO_LEFT");
            }
        }

        else if (Mathf.Abs(Dir.y) > Mathf.Abs(Dir.x))
        {

            // Debug.Log("Menor que 0");
            if (Dir.y < 0)


            {
                animator.Play("MINERO_DOWN");
            }

            else
            {

                animator.Play("MINERO_UP");
            }

        }
    }
}