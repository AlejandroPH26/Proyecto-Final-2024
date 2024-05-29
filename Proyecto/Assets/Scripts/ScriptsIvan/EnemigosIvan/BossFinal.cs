using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFinal : MonoBehaviour
{
    public float health = 100f;
    public float movementSpeed = 5f;
    public Transform spawnPoint; // Punto de aparición para la fase 2
    public GameObject spherePrefab; // El prefab de la esfera
    public Transform sphereSpawn1; // Posición de spawn para la primera esfera
    public Transform sphereSpawn2; // Posición de spawn para la segunda esfera

    private GameObject player;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isMoving = true;
    private bool spheresSpawned = false;

    public float frecuenciaDisparo = 2f;
    public BalaJefe balaPrefab;
    public float contador = 0f;
    public Transform bulletPos;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        /*if (health >= 0)
        {
            Defeated();
            return;
        }*/

        if (health == 100 && health > 75 && isMoving)
        {
            Phase1();
            return;
        }

        if (health == 75 && health > 50 && isMoving)
        {
            Phase2();
            return;
        }

        if (health == 50 && health >= 25 && !isMoving)
        {
            Debug.Log("fase3");
            Phase3();
            return;
        }



        /*if (health <= 75f && isMoving)
        {
            Phase2();
            return;
        }

        if (health <= 50f && isMoving)
        {
            Debug.Log("fase3");
            Phase3();
            return;
        }

        if (isMoving)
        {
            Phase1();
        }*/
    }

    void Phase1()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.velocity = direction * movementSpeed;
        animator.SetBool("IsMoving", true);
    }

    void Phase2()
    {
        animator.SetTrigger("Disappear");
        isMoving = false;
        rb.velocity = Vector2.zero;
    }

    void Phase3()
    {
       
      
    }

   

    public void MoveToPosition() // se llama en el ultimo frame de la animacion de desaparecer
    {
        transform.position = spawnPoint.position;
        animator.SetTrigger("Appear");
        isMoving = true;
    }

    public void OnDisappearAnimationComplete() // se llama desde la animacion de aparecer
    {
        // Una vez que la animación de "desaparecer" ha terminado, instanciamos las esferas
        if (!spheresSpawned)
        {
            Instantiate(spherePrefab, sphereSpawn1.position, Quaternion.identity);
            Instantiate(spherePrefab, sphereSpawn2.position, Quaternion.identity);
            spheresSpawned = true;
        }
    }

    public void OnAppearAnimationComplete() // se llama en el ultimo frame de la animacion de aparecer
    {
        animator.SetTrigger("Sleep");
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
            Defeated();
    }

    void Defeated()
    {
        Debug.Log("El jefe ha sido derrotado!");
        Destroy(this.gameObject);
        rb.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("BalaJugador"))
        {
            Destroy(collision.gameObject);
            TakeDamage(25);
        }
    }
}


