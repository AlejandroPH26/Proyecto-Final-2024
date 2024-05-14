using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaSombreroPaja : MonoBehaviour, ISombreros
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

    public void Shoot()
    {
        if (!canShoot) return; // Si no puede disparar, sal del m�todo

        // Establecer la direcci�n de movimiento de las balas basada en la direcci�n del sombrero
        Vector3 direction = Vector3.zero;

        switch (hatDir)
        {
            case Direction.UP:
                direction = Vector3.up;
                pAnimator.Play("Hat_Straw_Up");
                Debug.Log("Sombrero dispara arriba");
                InstantiateBullet(firePointUp, direction);
                break;
            case Direction.DOWN:
                direction = Vector3.down;
                Debug.Log("Sombrero dispara abajo");
                pAnimator.Play("Hat_Straw_Down");
                InstantiateBullet(firePointDown, direction);
                break;
            case Direction.RIGHT:
                direction = Vector3.right;
                Debug.Log("Sombrero dispara derecha");
                pAnimator.Play("Hat_Straw_Right");
                InstantiateBullet(firePointRight, direction);
                break;
            case Direction.LEFT:
                direction = Vector3.left;
                Debug.Log("Sombrero dispara izquierda");
                pAnimator.Play("Hat_Straw_Left");
                InstantiateBullet(firePointLeft, direction);
                break;
            default:
                // Si la direcci�n no est� definida, no disparamos
                Debug.LogWarning("La direcci�n del sombrero no est� definida. No se puede disparar.");
                break;
        }

        canShoot = false; // Desactivar disparo temporalmente
        Invoke("ActivateShoot", delay); // Invocar m�todo para activar el disparo despu�s del delay
    }

    void ActivateShoot()
    {
        canShoot = true; // Volver a activar el disparo
    }

    void InstantiateBullet(Transform firePoint, Vector3 direction)
    {
        // Instanciar la bala en el punto de fuego del sombrero
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        // Establecer la direcci�n de movimiento de la bala
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        // Destruir la bala despu�s de un cierto tiempo
        Destroy(bullet, bulletLifetime);
    }

    public void SetDirection(Direction dir)
    {
        hatDir = dir;

        switch (hatDir)
        {
            case Direction.UP:
                pAnimator.Play("Hat_Straw_Up");
                break;
            case Direction.DOWN:
                pAnimator.Play("Hat_Straw_Down");
                break;
            case Direction.RIGHT:
                pAnimator.Play("Hat_Straw_Right");
                break;
            case Direction.LEFT:
                pAnimator.Play("Hat_Straw_Left");
                break;
            default:
                Debug.LogWarning("La direcci�n del sombrero no est� definida.");
                break;
        }
    }

    public void SombreroRecogido()
    {
        // Aqu� puedes realizar cualquier acci�n que necesites cuando el sombrero es recogido.
        Debug.Log("Sombrero Sheriff recogido por el jugador.");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Reflejar la velocidad de la bala cuando colisiona con cualquier cosa
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 reflection = Vector2.Reflect(rb.velocity, collision.contacts[0].normal);
        rb.velocity = reflection;
    }
}
