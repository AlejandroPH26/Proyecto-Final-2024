using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PruebaSombreroSheriff : MonoBehaviour, ISombreros
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    public Transform firePointDown;
    public Transform firePointUp;
    public Transform firePointRight;
    public Transform firePointLeft;
    public Transform anchorUp;

    public float delay = 1f; // Delay entre disparos
    private bool canShoot = true; // Variable para controlar si puede disparar o no

    public Transform anclajeSuperior
    {
        get { return anchorUp; }
        set { anchorUp = value; }
    }

    public Animator pAnimator;

    public float bulletLifetime = 2f; // Tiempo de vida de las balas
    public Direction hatDir;

    // Start is called before the first frame update
    void Start()
    {
        pAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot()
    {
        if (!canShoot) return; // Si no puede disparar, sal del método

        // Establecer la dirección de movimiento de las balas basada en la dirección del sombrero
        Vector3 direction = Vector3.zero;

        switch (hatDir)
        {
            case Direction.UP:
                direction = Vector3.up;
                pAnimator.Play("Hat_Sheriff_Up");
                Debug.Log("Sombrero dispara arriba");
                InstantiateBullet(firePointUp, direction);
                break;
            case Direction.DOWN:
                direction = Vector3.down;
                Debug.Log("Sombrero dispara abajo");
                pAnimator.Play("Hat_Sheriff_Down");
                InstantiateBullet(firePointDown, direction);
                break;
            case Direction.RIGHT:
                direction = Vector3.right;
                Debug.Log("Sombrero dispara derecha");
                pAnimator.Play("Hat_Sheriff_Right");
                InstantiateBullet(firePointRight, direction);
                break;
            case Direction.LEFT:
                direction = Vector3.left;
                Debug.Log("Sombrero dispara izquierda");
                pAnimator.Play("Hat_Sheriff_Left");
                InstantiateBullet(firePointLeft, direction);
                break;
            default:
                // Si la dirección no está definida, no disparamos
                Debug.LogWarning("La dirección del sombrero no está definida. No se puede disparar.");
                break;
        }

        canShoot = false; // Desactivar disparo temporalmente
        Invoke("ActivateShoot", delay); // Invocar método para activar el disparo después del delay
    }
    void ActivateShoot()
    {
        canShoot = true; // Volver a activar el disparo
    }

    void InstantiateBullet(Transform firePoint, Vector3 direction)
    {
        // Instanciar la bala en el punto de fuego del sombrero
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Establecer la dirección de movimiento de la bala
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

        // Destruir la bala después de un cierto tiempo
        Destroy(bullet, bulletLifetime);
    }

    public void SetDirection(Direction dir)
    {
        hatDir = dir;

        switch (hatDir)
        {
            case Direction.UP:
                pAnimator.Play("Hat_Sheriff_Up");
                break;
            case Direction.DOWN:
                pAnimator.Play("Hat_Sheriff_Down");
                break;
            case Direction.RIGHT:
                pAnimator.Play("Hat_Sheriff_Right");
                break;
            case Direction.LEFT:
                pAnimator.Play("Hat_Sheriff_Left");
                break;
            default:
                Debug.LogWarning("La dirección del sombrero no está definida.");
                break;
        }
    }

    public void SombreroRecogido()
    {
        // Aquí puedes realizar cualquier acción que necesites cuando el sombrero es recogido.
        Debug.Log("Sombrero Sheriff recogido por el jugador.");
    }
}
