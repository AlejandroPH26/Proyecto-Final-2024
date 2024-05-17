using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
//Enum para posicion de la cabeza y el disparo
public enum Direction {UP, DOWN, LEFT, RIGHT};

public class MovimientoCabezaJugador : MonoBehaviour
{
    private GameManagerHats gm;
    public Direction dirCabeza = Direction.DOWN;
    public Animator cAnimator;
    public GameObject balaprefab;
    bool canShoot = true;
    public  bool isShooting = true;
    public float delay = 1f;
    public Transform GameObjectUp;
    public Transform GameObjectLeft;
    public Transform GameObjectRight;
    public Transform GameObjectDown;
    public Renderer sprite;
    public MusicManager mm;
    public AudioClip sDisparo;

    public float bulletLifetime = 2f; // Tiempo de vida de las balas

    // Start is called before the first frame update
    void Start()
    {
        cAnimator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        gm = GameManagerHats.instance;
        mm = MusicManager.instance;
}

    // Update is called once per frame
    void Update()
    {
        HeadPosition();
        DestruirCabeza();
    }
    public void HeadPosition() // Método para determinar la posición de la cabeza
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            dirCabeza = Direction.UP;
            sprite.sortingLayerName = "Jugador";
            sprite.sortingOrder = 4;
            Shoot();
            cAnimator.Play("WalkUp_Cabeza");
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            dirCabeza = Direction.RIGHT;
            //  pAnimator.Play("Mirar_Derecha");
            Shoot();
            cAnimator.Play("WalkRight_Cabeza");
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            dirCabeza = Direction.LEFT;
            // pAnimator.Play("Mirar_Izquierda");
            Shoot();
            cAnimator.Play("WalkLeft_Cabeza");
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            dirCabeza = Direction.DOWN;
            sprite.sortingLayerName = "Jugador";
            sprite.sortingOrder = 3;
            // pAnimator.Play("Mirar_Abajo");
            Shoot();
            cAnimator.Play("WalkDown_Cabeza");
        }
        else
        {
            isShooting = false;
        }
    }
          
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
                mm.PlaySFX(sDisparo);
                canShoot = false;
                isShooting = true;
                // Hacer un invoke para volver a activar canShoot en un tiempo p.ej. delayDisparo)
                Invoke("activaDisparo", delay);
                // Destruir la bala después de un cierto tiempo
                Destroy(bala, bulletLifetime); // Aquí se destruye la bala después de bulletLifetime segundos
            }
            else if (dirCabeza == Direction.LEFT)
            {
                // TODO cambiar posicion según dirección de la cabeza
                GameObject bala = Instantiate(balaprefab, GameObjectLeft.position, Quaternion.identity);
                // Obtener componente BalaJugador del objeto
                BalaJugador bullet = bala.GetComponent<BalaJugador>();
                // A esa BalaJugador le asignamos la dirección (la de la cabeza)
                bullet.asignarDireccion(dirCabeza);
                mm.PlaySFX(sDisparo);
                // Desactivar el disparo (p.ej. booleano canShoot)
                canShoot = false;
                isShooting = true;
                // Hacer un invoke para volver a activar canShoot en un tiempo p.ej. delayDisparo)
                Invoke("activaDisparo", delay);
                // Destruir la bala después de un cierto tiempo
                Destroy(bala, bulletLifetime); // Aquí se destruye la bala después de bulletLifetime segundos
            }
            else if (dirCabeza == Direction.RIGHT)
            {
                // TODO cambiar posicion según dirección de la cabeza
                GameObject bala = Instantiate(balaprefab, GameObjectRight.position, Quaternion.identity);
                // Obtener componente BalaJugador del objeto
                BalaJugador bullet = bala.GetComponent<BalaJugador>();
                // A esa BalaJugador le asignamos la dirección (la de la cabeza)
                bullet.asignarDireccion(dirCabeza);
                mm.PlaySFX(sDisparo);
                // Desactivar el disparo (p.ej. booleano canShoot)
                canShoot = false;
                isShooting = true;
                // Hacer un invoke para volver a activar canShoot en un tiempo p.ej. delayDisparo)
                Invoke("activaDisparo", delay);
                // Destruir la bala después de un cierto tiempo
                Destroy(bala, bulletLifetime); // Aquí se destruye la bala después de bulletLifetime segundos
            }
            else if (dirCabeza == Direction.DOWN)
            {
                // TODO cambiar posicion según dirección de la cabeza
                GameObject bala = Instantiate(balaprefab, GameObjectDown.position, Quaternion.identity);
                // Obtener componente BalaJugador del objeto
                BalaJugador bullet = bala.GetComponent<BalaJugador>();
                // A esa BalaJugador le asignamos la dirección (la de la cabeza)
                bullet.asignarDireccion(dirCabeza);
                mm.PlaySFX(sDisparo);
                // Desactivar el disparo (p.ej. booleano canShoot)
                canShoot = false;
                isShooting = true;
                // Hacer un invoke para volver a activar canShoot en un tiempo p.ej. delayDisparo)
                Invoke("activaDisparo", delay);
                // Destruir la bala después de un cierto tiempo
                Destroy(bala, bulletLifetime); // Aquí se destruye la bala después de bulletLifetime segundos
            }
        }
    }

   private void activaDisparo()
    {
        canShoot = true;
    }

   //Metodo con dirCabeza para usarlo en el script de movimiento y orientar la cabeza en la dirección en la que se anda
   public void OrientarCabeza(Direction dir)
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

    public void DestruirCabeza()
    {
        if(gm.vidasActuales <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Se destruye la cabeza");
        }
    }
}
