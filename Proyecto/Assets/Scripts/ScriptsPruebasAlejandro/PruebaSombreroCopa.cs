using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaSombreroCopa : MonoBehaviour, ISombreros
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    public Transform firePoint;
    public float bulletLifetime = 2f; // Tiempo de vida de las balas

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Shoot();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Shoot();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Shoot();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        // Determinar la dirección basada en la tecla presionada
        Vector3 direction = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = Vector3.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = Vector3.down;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Vector3.right;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Vector3.left;
        }

        // Instanciar dos balas separadas por 0.5 unidades en el eje x
        Vector3 offset = new Vector3(0.5f, 0f, 0f);
        GameObject bullet1 = Instantiate(bulletPrefab, firePoint.position + offset, Quaternion.identity);
        GameObject bullet2 = Instantiate(bulletPrefab, firePoint.position - offset, Quaternion.identity);

        // Establecer la dirección de movimiento de las balas
        bullet1.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        bullet2.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

        // Destruir las balas después de un cierto tiempo
        Destroy(bullet1, bulletLifetime);
        Destroy(bullet2, bulletLifetime);
    }

 
}
