using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Enum para posicion de la cabeza y el disparo
public enum Direction {UP, DOWN, LEFT, RIGHT};

public class MovimientoCabezaJugador : MonoBehaviour
{
    public Direction dirCabeza = Direction.DOWN;
    public Animator cAnimator;
    public float speedBullet = 3f;
    public GameObject balaprefab;
    bool canShoot=true;
    public float delay = 1f;
    public Transform GameObjectUp;
    public Transform GameObjectLeft;
    public Transform GameObjectRight;
    public Transform GameObjectDown;

    // Start is called before the first frame update
    void Start()
    {
        cAnimator = GetComponent<Animator>();   
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
            Shoot();
            cAnimator.Play("WalkUp_Cabeza");
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            dirCabeza = Direction.RIGHT;
            //  pAnimator.Play("Mirar_Derecha");
            Shoot();
            cAnimator.Play("WalkRight_Cabeza");
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            dirCabeza = Direction.LEFT;
            // pAnimator.Play("Mirar_Izquierda");
            Shoot();
            cAnimator.Play("WalkLeft_Cabeza");
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            dirCabeza = Direction.DOWN;
            // pAnimator.Play("Mirar_Abajo");
            Shoot();
            cAnimator.Play("WalkDown_Cabeza");
        }
    }
           // transform.position += new Vector3(0, speedBullet, 0) * Time.deltaTime;
           //transform.position += new Vector3(speedBullet, 0, 0) * Time.deltaTime;
           // transform.position += new Vector3(-speedBullet, 0, 0) * Time.deltaTime;
           // transform.position += new Vector3(0, -speedBullet, 0) * Time.deltaTime;
    public void Shoot()
    {
        if (canShoot == true)
        {
            if (dirCabeza == Direction.UP)
            {
                // TODO cambiar posicion según dirección de la cabeza
                GameObject bala = Instantiate(balaprefab, GameObjectUp.position, Quaternion.identity);
                // Obtener componente BalaJugador del objeto
                BalaJugador bullet = bala.GetComponent<BalaJugador>();
                // A esa BalaJugador le asignamos la dirección (la de la cabeza)
                bullet.asignarDireccion(dirCabeza);

                // Desactivar el disparo (p.ej. booleano canShoot)
                canShoot = false;
                // Hacer un invoke para volver a activar canShoot en un tiempo p.ej. delayDisparo)
                Invoke("activaDisparo", delay);
            }
            if (dirCabeza == Direction.LEFT)
            {
                // TODO cambiar posicion según dirección de la cabeza
                GameObject bala = Instantiate(balaprefab, GameObjectLeft.position, Quaternion.identity);
                // Obtener componente BalaJugador del objeto
                BalaJugador bullet = bala.GetComponent<BalaJugador>();
                // A esa BalaJugador le asignamos la dirección (la de la cabeza)
                bullet.asignarDireccion(dirCabeza);

                // Desactivar el disparo (p.ej. booleano canShoot)
                canShoot = false;
                // Hacer un invoke para volver a activar canShoot en un tiempo p.ej. delayDisparo)
                Invoke("activaDisparo", delay);
            }
            if (dirCabeza == Direction.RIGHT)
            {
                // TODO cambiar posicion según dirección de la cabeza
                GameObject bala = Instantiate(balaprefab, GameObjectRight.position, Quaternion.identity);
                // Obtener componente BalaJugador del objeto
                BalaJugador bullet = bala.GetComponent<BalaJugador>();
                // A esa BalaJugador le asignamos la dirección (la de la cabeza)
                bullet.asignarDireccion(dirCabeza);

                // Desactivar el disparo (p.ej. booleano canShoot)
                canShoot = false;
                // Hacer un invoke para volver a activar canShoot en un tiempo p.ej. delayDisparo)
                Invoke("activaDisparo", delay);
            }
            if (dirCabeza == Direction.DOWN)
            {
                // TODO cambiar posicion según dirección de la cabeza
                GameObject bala = Instantiate(balaprefab, GameObjectDown.position, Quaternion.identity);
                // Obtener componente BalaJugador del objeto
                BalaJugador bullet = bala.GetComponent<BalaJugador>();
                // A esa BalaJugador le asignamos la dirección (la de la cabeza)
                bullet.asignarDireccion(dirCabeza);

                // Desactivar el disparo (p.ej. booleano canShoot)
                canShoot = false;
                // Hacer un invoke para volver a activar canShoot en un tiempo p.ej. delayDisparo)
                Invoke("activaDisparo", delay);
            }





        }
    }
   private void activaDisparo()
    {
        canShoot = true;
    }
   //Metodo con dirCabeza para usarlo en el script de movimiento y orientar la cabeza en la dirección en la que se anda
   public void orientarCabeza(Direction dir)
    {
        dirCabeza = dir;

        if (dirCabeza == Direction.UP)
        {
            cAnimator.Play("WalkUp_Cabeza");
        }
        if (dirCabeza == Direction.LEFT)
        {
            cAnimator.Play("WalkLeft_Cabeza");
        }
        if (dirCabeza == Direction.RIGHT)
        {
            cAnimator.Play("WalkRight_Cabeza");
        }
        if (dirCabeza == Direction.DOWN)
        {
            cAnimator.Play("WalkDown_Cabeza");
        }
    }
}
