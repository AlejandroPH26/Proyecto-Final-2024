using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBorracho : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    public GameObject player;
   
    public float RangoMin = 8;
   
    public float vidaActual = 0;
    public float vidaMax = 50;

    public bool canShoot = true;

   

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        vidaActual = vidaMax;
        
    }
  
    void Update()
    {
        Rango();
    }

    void shoot() // Se llama desde el animator 
    {          
            Instantiate(bullet, bulletPos.position, Quaternion.identity);      
    }
    
    void Rango()
    {
        float distancia = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log(distancia);

        if (distancia < RangoMin && canShoot == true)
        {
           
            //Reproduce la animacion de disparo , en el ultimo frame de la animacion llamar a la variable canShoot y ponerla a true
            canShoot = false;
   
        }
    }

    public void EnableShoot() // Se llama desde el animator 
    {
        canShoot = true;
    }

    public void Muerte()

    {
        //Eanimator.Play("BORRACHO_DEATH");
        Destroy(this.gameObject);
    }

    public void DamageTaken(int cantidad)
    {
        vidaActual = vidaActual - cantidad;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BalaJugador"))
        {
            Debug.Log("dañorecibidoBorracho");
            DamageTaken(20);

            //Destroy(collision.gameObject); // Destruye la bala 


            if (vidaActual <= 0)
            {
                Muerte();
            }

        }
    }

}
