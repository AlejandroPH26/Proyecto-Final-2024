using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Enum para posicion de la cabeza y el disparo
public enum Direction {UP, DOWN, LEFT, RIGHT};
public class MovimientoCabezaJugador : MonoBehaviour
{
    public Direction dirCabeza;
    public Animator pAnimator;
    public float speedBullet = 3f;
    public GameObject balaprefab;
    bool canShoot;
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
            Shoot();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            dirCabeza = Direction.RIGHT;
            //  pAnimator.Play("Mirar_Derecha");
            Shoot();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            dirCabeza = Direction.LEFT;
            // pAnimator.Play("Mirar_Izquierda");
            Shoot();
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            dirCabeza = Direction.DOWN;
            // pAnimator.Play("Mirar_Abajo");
            Shoot();
        }
    }
           // transform.position += new Vector3(0, speedBullet, 0) * Time.deltaTime;
           //transform.position += new Vector3(speedBullet, 0, 0) * Time.deltaTime;
           // transform.position += new Vector3(-speedBullet, 0, 0) * Time.deltaTime;
           // transform.position += new Vector3(0, -speedBullet, 0) * Time.deltaTime;
    public void Shoot()
    {
        // TODO cambiar posicion según dirección de la cabeza
        GameObject bala = Instantiate(balaprefab, transform.position, Quaternion.identity);
        // Obtener componente BalaJugador del objeto
        BalaJugador bullet = bala.GetComponent<BalaJugador>();
        // A esa BalaJugador le asignamos la dirección (la de la cabeza)
        if (dirCabeza == Direction.UP)
        {
            new Vector2(0, 1);
        }
        if (dirCabeza == Direction.RIGHT)
        {
            new Vector2(1, 0);
        }
        if (dirCabeza == Direction.LEFT)
        {
            new Vector2(-1, 0);
        }
        if (dirCabeza == Direction.DOWN)
        {
            new Vector2(0, -1);
        }
        // Desactivar el disparo (p.ej. booleano canShoot)
        canShoot = false;
        // Hacer un invoke para volver a activar canShoot en un tiempo p.ej. delayDisparo)

    }
}
