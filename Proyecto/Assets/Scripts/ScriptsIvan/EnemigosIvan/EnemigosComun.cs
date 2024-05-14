using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigosComun : MonoBehaviour
{
    public int vidaActual;
    public int vidaMax;
    public int vidaPerdida;

    public bool activo = true;
    public float TCambioColor = 0.1f;

    public SpriteRenderer rbSprite;

    public Color colorDa�o;
    public Color colorOriginal;

    void Start()
    {
        vidaActual = vidaMax;
        rbSprite = GetComponent<SpriteRenderer>();
        colorOriginal = rbSprite.color;
    }

    
    void Update()
    {
        
    }

    public void Muerte()

    {
        Destroy(this.gameObject);
    }

    public void Da�oRecibido(int cantidad)
   
    {
        vidaActual = vidaActual - cantidad;
    }

    private void OnCollisionEnter2D(Collision2D collision)
   
    {
        if (collision.gameObject.CompareTag("BalaJugador"))
        {
            CambiarColor(colorDa�o);

            Debug.Log("Da�oRecibido");

            Da�oRecibido(vidaPerdida);

            Destroy(collision.gameObject); // Destruye la bala 


            if (vidaActual <= 0)
            {
                Muerte();
            }

            StartCoroutine(RevertirColorDespuesDeTiempo(TCambioColor)); // Llamo a la co-rutina para devolver el color original

        }
    }

    IEnumerator RevertirColorDespuesDeTiempo(float tiempo) // Co-rutina para devolver el color original al enemigo
    {
        yield return new WaitForSeconds(tiempo); // Espera el tiempo especificado

        // Vuelve al color original despu�s del tiempo especificado
        rbSprite.color = colorOriginal;
    }

    public void CambiarColor(Color color)
    {   
            rbSprite.color = color; // Cambia el color del jugador                                
    }
}
