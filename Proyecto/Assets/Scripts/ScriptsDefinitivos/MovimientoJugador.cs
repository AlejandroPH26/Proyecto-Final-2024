using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MovimientoJugador : MonoBehaviour
{
    private GameManagerHats gm;
    private MovimientoCabezaJugador cabeza;
    [Tooltip("Velocidad del jugador en unidades de unity / segundo")]
    public float speed = 2f;
    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveDown = KeyCode.S;
    public KeyCode moveRight = KeyCode.D;
    public float y = -1;
    public float x = 0;
    public Animator pAnimator;
    private bool isMoving = false;
    private Rigidbody2D rb;
    public MusicManager mm;
    public AudioClip pickUp;
    // Start is called before the first frame update
    void Start()
    {
        pAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cabeza = GetComponentInChildren<MovimientoCabezaJugador>();
        gm = GameManagerHats.instance;
        mm = MusicManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        InputJugador();
        MuerteJugador();
    }
    private void InputJugador()
    {
        Vector2 aux = Vector2.zero; // Declaramos el Vector2 para el movimiento y lo identificamos como aux la variable y ponemos que es 0 para que en caso de no pulsar nada se quede quieto
        if (Input.GetKey(moveUp) && Input.GetKey(moveLeft))// Diagonal arriba izquierda
        {
            aux.y = 1;
            aux.x = -1;
            pAnimator.Play("WalkUp_Personaje");
            if(!cabeza.isShooting)
            {
                cabeza.orientarCabeza(Direction.UP);
            }

        }
        else if (Input.GetKey(moveUp) && Input.GetKey(moveRight))// Diagonal arriba izquierda
        {
            aux.y = 1;
            aux.x = 1;
            pAnimator.Play("WalkUp_Personaje");
            if (!cabeza.isShooting)
            {
                cabeza.orientarCabeza(Direction.UP);
            }
        }
        else if (Input.GetKey(moveDown) && Input.GetKey(moveLeft))// Diagonal arriba izquierda
        {
            aux.y = -1;
            aux.x = -1;
            pAnimator.Play("WalkDown_Personaje");
            if (!cabeza.isShooting)
            {
                cabeza.orientarCabeza(Direction.DOWN);
            }
        }
        else if (Input.GetKey(moveDown) && Input.GetKey(moveRight))// Diagonal arriba izquierda
        {
            aux.y = -1;
            aux.x = 1;
            pAnimator.Play("WalkDown_Personaje");
            if (!cabeza.isShooting)
            {
                cabeza.orientarCabeza(Direction.DOWN);
            }
        }
        else if (Input.GetKey(moveUp)) // Comprobamos que se está pulsando la tecla W
        {
            // Nos desplazamos (sumamos movimiento) hacia arriba (eje y = 1), multiplicamos por deltatime
            // para que el movimiento no dependa del framerate ya que lo gestiona el motor de fisicas.
            aux.y = 1;
            pAnimator.Play("WalkUp_Personaje");
            if (!cabeza.isShooting)
            {
                cabeza.orientarCabeza(Direction.UP);
            }
        }
        else if (Input.GetKey(moveDown))
        {
            // Nos desplazamos (sumamos movimiento) hacia abajo (eje y = -1), multiplicamos por deltatime
            aux.y = -1;
            pAnimator.Play("WalkDown_Personaje");
            if (!cabeza.isShooting)
            {
                cabeza.orientarCabeza(Direction.DOWN);
            }
        }
        else if (Input.GetKey(moveLeft))
        {
            // Nos desplazamos (sumamos movimiento) hacia la izquierda (eje x = -1), multiplicamos por deltatime
            aux.x = -1;
            pAnimator.Play("WalkLeft_Personaje");
            if (!cabeza.isShooting)
            {
                cabeza.orientarCabeza(Direction.LEFT);
            }
        }
        else if (Input.GetKey(moveRight))
        {
            // Nos desplazamos (sumamos movimiento) hacia la derecha (eje x = 1), multiplicamos por deltatime
            aux.x = 1;
            pAnimator.Play("WalkRight_Personaje");
            if (!cabeza.isShooting)
            {
                cabeza.orientarCabeza(Direction.RIGHT);
            }

        } 
        if (aux.x == 0 && aux.y == 0)
        {
            // Nos desplazamos (sumamos movimiento) hacia la derecha (eje x = 1), multiplicamos por deltatime
            //pAnimator.Play("IdleDown_Personaje");
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
        pAnimator.SetBool("Moving", isMoving);
        rb.velocity = aux.normalized * speed; // Aqui lo normalizamos y lo multiplicamos por speed para que no vaya mas rapido en diagonal


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Botiquin")
        {
            Destroy(collision.gameObject);
            gm.SumarVidas();
            mm.PlaySFX(pickUp);
        }
        if (collision.tag == "BombaItem")
        {
            Destroy(collision.gameObject);
            gm.SumarBombas();
            mm.PlaySFX(pickUp);
        }
        if (collision.tag == "Enemigo")
        {
            
        }
    }
    private void MuerteJugador()
    {
        if (gm.vidas <= 0)
        {
            pAnimator.Play("Anim_Muerte");
        }
    }
}

