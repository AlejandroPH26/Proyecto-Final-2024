using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBorracho : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    public GameObject player;
   
    public float RangoMin = 8;
   
    public float vidaActual;
    public float vidaMax = 50;

    public bool canShoot = true;

   

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
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
           shoot();
            //Reproduce la animacion de disparo , en el ultimo frame de la animacion llamar a la variable canShoot y ponerla a true
            //canShoot = false;
   
        }
    }

    public void EnableShoot() // Se llama desde el animator 
    {
        canShoot = true;
    }
  
}
