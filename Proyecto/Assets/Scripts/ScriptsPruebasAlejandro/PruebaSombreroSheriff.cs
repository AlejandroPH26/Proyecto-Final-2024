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
    public Transform anclajeSuperior
    {
        get { return anchorUp; }
        set { anchorUp = value; }
    }
    public float bulletLifetime = 2f; // Tiempo de vida de las balas
    public Direction hatDir;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot()
    {
        // Establecer la direcci�n de movimiento de las balas basada en la direcci�n del sombrero
        Vector3 direction = Vector3.zero;

        switch (hatDir)
        {
            case Direction.UP:
                direction = Vector3.up;
                Debug.Log("Sombrero dispara arriba");
                InstantiateBullet(firePointUp, direction);
                break;
            case Direction.DOWN:
                direction = Vector3.down;
                Debug.Log("Sombrero dispara abajo");
                InstantiateBullet(firePointDown, direction);
                break;
            case Direction.RIGHT:
                direction = Vector3.right;
                Debug.Log("Sombrero dispara derecha");
                InstantiateBullet(firePointRight, direction);
                break;
            case Direction.LEFT:
                direction = Vector3.left;
                Debug.Log("Sombrero dispara izquierda");
                InstantiateBullet(firePointLeft, direction);
                break;
            default:
                // Si la direcci�n no est� definida, no disparamos
                Debug.LogWarning("La direcci�n del sombrero no est� definida. No se puede disparar.");
                break;
        }
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
    }

    public void SombreroRecogido()
    {
        // Aqu� puedes realizar cualquier acci�n que necesites cuando el sombrero es recogido.
        Debug.Log("Sombrero Sheriff recogido por el jugador.");
    }
}
