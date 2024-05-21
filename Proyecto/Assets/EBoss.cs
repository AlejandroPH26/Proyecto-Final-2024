using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EBoss : MonoBehaviour
{
    public int Speed;
    public bool canMove = true;
    public float InicioFase = 5f;

    private Rigidbody2D rb;
    private JugadorV1 Player;
    private GameManagerHats gm;
    private EnemigosComun enemy;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public List<Transform> PrimeraOleadaPos;
    public List<Transform> SegundaOleadaPos;
    public List<Transform> TerceraOleadaPos;
    public List<Transform> CuartaOleadaPos;

    public List<GameObject> prefabs;

    private int OleadaActual = 0;
    private int EnemigosADerrotar = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = FindObjectOfType<JugadorV1>();
        gm = FindObjectOfType<GameManagerHats>();
        enemy = GetComponent<EnemigosComun>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Inicialmente invoca la primera ronda de enemigos
       
    }

    void Update()
    {
        InvocarEnemigos();

       
    }

    public void FaseDeAtaque()
    {
        if (enemy.vidaActual == enemy.vidaMax)
        {
            // Inicia la primera fase de ataque invocando enemigos
            InvocarEnemigos();
        }
    }

    public void InvocarEnemigos()
    {
        rb.velocity = Vector2.zero;
        //spriteRenderer.enabled = false;

        if (OleadaActual == 0)
        {
            // Primera ronda de enemigos
            EnemigosADerrotar = PrimeraOleadaPos.Count;
            foreach (Transform pos in PrimeraOleadaPos)
            {
                Instantiate(prefabs[0], pos.position, pos.rotation);
            }
        }
        else if (OleadaActual == 1)
        {
            // Rondas subsiguientes de enemigos
           EnemigosADerrotar = SegundaOleadaPos.Count;
            foreach (Transform pos in SegundaOleadaPos)
            {          
                Instantiate(prefabs[1], pos.position, pos.rotation);
            }
        }

        else if (OleadaActual == 2)
        {
            // Rondas subsiguientes de enemigos
            EnemigosADerrotar = TerceraOleadaPos.Count;
            foreach (Transform pos in SegundaOleadaPos)
            {
                Instantiate(prefabs[2], pos.position, pos.rotation);
            }
        }

        else if (OleadaActual == 3)
        {
            // Rondas subsiguientes de enemigos
            EnemigosADerrotar = CuartaOleadaPos.Count;
            foreach (Transform pos in SegundaOleadaPos)
            {
                Instantiate(prefabs[3], pos.position, pos.rotation);
            }
        }

        OleadaActual++;

        void PrimeraOleada()
        {

        }
    }

    public void EnemyDefeated()
    {
        EnemigosADerrotar--;
        if (EnemigosADerrotar <= 0)
        {
            if (OleadaActual < 3) // Realiza 3 rondas
            {
                InvocarEnemigos();
            }
            else
            {
                // Todas las rondas han sido completadas, puedes hacer algo aquí.
            }
        }
    }

    public void PerseguirJugador()
    {
        Vector2 Dir = (Player.transform.position - transform.position).normalized;
        Vector2 velocidadMovimiento = Dir * Speed;
        rb.velocity = velocidadMovimiento;
        ComprobacionDireccion();
    }

    public void Saltar()
    {
        // Implementar lógica de salto si es necesario
    }

    public void Invulnerabilidad()
    {
        // Implementar lógica de invulnerabilidad si es necesario
    }

    private void ComprobacionDireccion()
    {
        Vector2 Dir = (Player.transform.position - transform.position).normalized;

        if (Mathf.Abs(Dir.x) > Mathf.Abs(Dir.y))
        {
            if (Dir.x > 0)
            {
                animator.Play("MINERO_RIGHT");
            }
            else
            {
                animator.Play("MINERO_LEFT");
            }
        }
        else
        {
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




