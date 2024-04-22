using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EProyectil : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
    }

  
    void Update()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstaculo") || collision.gameObject.CompareTag("Pared") || collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }





}
