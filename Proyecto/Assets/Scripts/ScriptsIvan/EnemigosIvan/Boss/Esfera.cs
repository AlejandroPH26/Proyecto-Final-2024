using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esfera : MonoBehaviour
{ 
    private Transform jugador;
    public int speed = 3;
    public GameManagerHats gm;

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        gm = FindObjectOfType<GameManagerHats>();
    }

    void Update()
    {
        PerseguirJugador();
    }

    void PerseguirJugador()
    {
        transform.position = Vector2.MoveTowards(transform.position, jugador.position, speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("BalaJugador"))
        {
            Destroy(collision.gameObject);
        }

        else if (collision.collider.CompareTag("Player"))
        {
            gm.RestarVidas();
        }
    }

   
}