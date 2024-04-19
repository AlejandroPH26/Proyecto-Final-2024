using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBorracho : MonoBehaviour
{
    public Transform Jugador;
    private bool isFacingRight = true;
    public int speed;

    void Start()
    {


    }
    // Update is called once per frame
    void Update()
    {
       transform.position = Vector2.MoveTowards(transform.position, Jugador.position, speed * Time.deltaTime);
 
      bool isPlayerRight = transform.position.x < Jugador.transform.position.x;
      Voltear(isPlayerRight);
    }

   private void Voltear(bool isPlayerRight)
   {
        if((isFacingRight && !isPlayerRight) || (!isFacingRight && isPlayerRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
   }
}
