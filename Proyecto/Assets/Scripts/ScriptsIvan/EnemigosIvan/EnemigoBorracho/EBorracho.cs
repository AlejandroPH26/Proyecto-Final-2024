using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBorracho : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    public JugadorV1 player;
   
    public float RangoMin = 8;

    public Animator animator;
   
   
   

    public bool canShoot = true;

   

    void Start()
    {
        player = FindObjectOfType<JugadorV1>();
           
    }
  
    void Update()
    {
        Rango();
    }

    public void shoot() // Se llama desde el animator 
    {          
            Instantiate(bullet, bulletPos.position, Quaternion.identity);      
    }
    
    void Rango()
    {
        float distancia = Vector2.Distance(transform.position, player.transform.position);
        //Debug.Log(distancia);

        if (distancia < RangoMin)
        {

            if (canShoot == true)
            {
                Debug.Log("Disparo");
                animator.Play("BORRACHO_ATTACK");
                //Reproduce la animacion de disparo , en el ultimo frame de la animacion llamar a la variable canShoot y ponerla a true
                
            }
            else // pausa entre disparos
            {
                animator.Play("BORRACHO_IDLE");
            }
        }

        else if (canShoot==false)
        {
            animator.Play("BORRACHO_IDLE");
        }
    }

    public void EnableShoot() // Se llama desde el animator 
    {
        canShoot = true;
    }
    public void DelayShoot()
    {
        canShoot = false;
        Invoke("EnableShoot", 3);
    }

  
}
