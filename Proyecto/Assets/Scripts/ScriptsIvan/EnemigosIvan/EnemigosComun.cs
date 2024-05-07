using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigosComun : MonoBehaviour
{
    public int vidaActual;
    public int vidaMax;
    public int vidaPerdida;

    public bool activo = true;
    void Start()
    {
        vidaActual = vidaMax;
    }

    
    void Update()
    {
        
    }

    public void Muerte()

    {
        //Eanimator.Play("MINERO_DEATH");
        Destroy(this.gameObject);
    }

    public void DañoRecibido(int cantidad)
   
   {
        vidaActual = vidaActual - cantidad;
    }

    private void OnCollisionEnter2D(Collision2D collision)
   
    {
        if (collision.gameObject.CompareTag("BalaJugador"))
        {
            Debug.Log("DañoRecibido");
            DañoRecibido(vidaPerdida);

            Destroy(collision.gameObject); // Destruye la bala 


            if (vidaActual <= 0)
            {
                Muerte();
            }

        }
    }
}
