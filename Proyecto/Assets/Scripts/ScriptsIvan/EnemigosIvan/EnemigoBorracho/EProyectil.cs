using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EProyectil : MonoBehaviour
{
    private JugadorV1 Player;

    public Animator Animator;
    
    private Rigidbody2D rb;
    public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       
        Player = FindObjectOfType<JugadorV1>(); // Busca el script del jugador 

        Animator = GetComponent<Animator>(); // Busco el animator de la bala

        Vector3 direction = Player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;

        float rot = Mathf.Atan2(-direction.y,-direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

  
    void Update()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstaculo") || collision.gameObject.CompareTag("Pared"))
        {
            Animator.Play("BOTTLE_BREAK");
            

        }

        else if (collision.gameObject.CompareTag("Player"))
        {
            Animator.Play("BOTTLE_BREAK");
            //Player.DamageTaken(20); // Jugador     
        }
    }


    public void DestruirObjeto()
    {
        Destroy(this.gameObject);
    }
}
