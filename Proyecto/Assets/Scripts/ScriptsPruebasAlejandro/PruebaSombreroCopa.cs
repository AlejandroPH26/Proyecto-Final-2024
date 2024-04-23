using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaSombreroCopa : MonoBehaviour, ISombreros
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    public Transform firePoint;
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
        // Determinar la direcci�n de disparo y el desplazamiento en el eje y
        Vector3 offset = Vector3.zero;

        switch (hatDir)
        {
            case Direction.UP:
                direction = Vector3.up;
                offset = new Vector3(0.5f, 0f, 0f);
                Debug.Log("Sombrero dispara arriba");
                break;
            case Direction.DOWN:
                direction = Vector3.down;
                offset = new Vector3(-0.5f, 0f, 0f);
                Debug.Log("Sombrero dispara abajo");
                break;
            case Direction.RIGHT:
                direction = Vector3.right;
                offset = new Vector3(0f, 0.5f, 0f);
                Debug.Log("Sombrero dispara derecha");
                break;
            case Direction.LEFT:
                direction = Vector3.left;
                offset = new Vector3(0f, -0.5f, 0f);
                Debug.Log("Sombrero dispara izquierda");
                break;
            default:
                // Si la direcci�n no est� definida, no disparamos
                Debug.LogWarning("La direcci�n del sombrero no est� definida. No se puede disparar.");
                return;
        }

        // Instanciar dos balas separadas por 0.5 unidades en la direcci�n del sombrero
        
        GameObject bullet1 = Instantiate(bulletPrefab, firePoint.position + offset, Quaternion.identity);
        GameObject bullet2 = Instantiate(bulletPrefab, firePoint.position - offset, Quaternion.identity);

        // Establecer la direcci�n de movimiento de las balas
        bullet1.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        bullet2.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

        // Destruir las balas despu�s de un cierto tiempo
        Destroy(bullet1, bulletLifetime);
        Destroy(bullet2, bulletLifetime);

    }

    public void SetDirection(Direction dir)
    {
        hatDir = dir;
    }


}
