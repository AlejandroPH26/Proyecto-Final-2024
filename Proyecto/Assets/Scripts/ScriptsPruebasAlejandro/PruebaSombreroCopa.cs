using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaSombreroCopa : MonoBehaviour, ISombreros
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    public Transform firePointDown;
    public Transform firePointUp;
    public Transform firePointRight;
    public Transform firePointLeft;
    public Transform anchorUp;

    public Transform anclajeSuperior { 
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
        // Establecer la dirección de movimiento de las balas basada en la dirección del sombrero
        Vector3 direction = Vector3.zero;
        // Determinar la dirección de disparo y el desplazamiento en el eje y
        Vector3 offset = Vector3.zero;

        switch (hatDir)
        {
            case Direction.UP:
                direction = Vector3.up;
                offset = new Vector3(0.5f, 0f, 0f);
                //pAnimator.Play("Hat_Noble_Up");
                Debug.Log("Sombrero dispara arriba");
                InstantiateBullet(firePointUp, direction, offset);
                break;
            case Direction.DOWN:
                direction = Vector3.down;
                offset = new Vector3(-0.5f, 0f, 0f);
                //pAnimator.Play("Hat_Noble_Down");
                Debug.Log("Sombrero dispara abajo");
                InstantiateBullet(firePointDown, direction, offset);
                break;
            case Direction.RIGHT:
                direction = Vector3.right;
                offset = new Vector3(0f, 0.5f, 0f);
                //pAnimator.Play("Hat_Noble_Right");
                Debug.Log("Sombrero dispara derecha");
                InstantiateBullet(firePointRight, direction, offset);
                break;
            case Direction.LEFT:
                direction = Vector3.left;
                offset = new Vector3(0f, -0.5f, 0f);
                //pAnimator.Play("Hat_Noble_Left");
                Debug.Log("Sombrero dispara izquierda");
                InstantiateBullet(firePointLeft, direction, offset);
                break;
            default:
                // Si la dirección no está definida, no disparamos
                Debug.LogWarning("La dirección del sombrero no está definida. No se puede disparar.");
                break;
        }
    }

    void InstantiateBullet(Transform firePoint, Vector3 direction, Vector3 offset)
    {
        // Instanciar dos balas separadas por 0.5 unidades en la dirección del sombrero
        GameObject bullet1 = Instantiate(bulletPrefab, firePoint.position + offset, Quaternion.identity);
        GameObject bullet2 = Instantiate(bulletPrefab, firePoint.position - offset, Quaternion.identity);

        // Establecer la dirección de movimiento de las balas
        bullet1.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        bullet2.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

        // Destruir las balas después de un cierto tiempo
        Destroy(bullet1, bulletLifetime);
        Destroy(bullet2, bulletLifetime);
    }

    public void SetDirection(Direction dir)
    {
        hatDir = dir;

        switch (hatDir)
        {
            case Direction.UP:
                pAnimator.Play("Hat_Noble_Up");
                break;
            case Direction.DOWN:
                pAnimator.Play("Hat_Noble_Down");
                break;
            case Direction.RIGHT:
                pAnimator.Play("Hat_Noble_Right");
                break;
            case Direction.LEFT:
                pAnimator.Play("Hat_Noble_Left");
                break;
            default:
                Debug.LogWarning("La dirección del sombrero no está definida.");
                break;
        }
    }

    public void SombreroRecogido()
    {
        // Aquí puedes realizar cualquier acción que necesites cuando el sombrero es recogido.
        Debug.Log("Sombrero Copa recogido por el jugador.");
    }


}
