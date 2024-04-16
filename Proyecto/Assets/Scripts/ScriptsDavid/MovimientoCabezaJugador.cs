using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Enum para posicion de la cabeza y el disparo
public enum Direction {UP, DOWN, LEFT, RIGHT};
public class MovimientoCabezaJugador : MonoBehaviour
{
    public Direction dirCabeza;
    public Animator pAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HeadPosition();
    }
    public void HeadPosition() // Método para determinar la posición de la cabeza
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            dirCabeza = Direction.UP;
           // pAnimator.Play("Mirar_Arriba");
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            dirCabeza = Direction.LEFT;
          //  pAnimator.Play("Mirar_Derecha");
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            dirCabeza = Direction.LEFT;
           // pAnimator.Play("Mirar_Izquierda");
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            dirCabeza = Direction.DOWN;
           // pAnimator.Play("Mirar_Abajo");
        }
    }
}
