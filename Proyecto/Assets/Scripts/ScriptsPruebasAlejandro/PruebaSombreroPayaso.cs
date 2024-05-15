using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaSombreroPayaso : MonoBehaviour, ISombreros
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
        if (!canShoot) return; // Si no puede disparar, sal del m�todo

        Vector3 direction = Vector3.zero;
        // Determinar la direcci�n aleatoria de la bala
        Vector3 ramdomDirection = Random.insideUnitCircle.normalized;

        // Calcular el �ngulo de disparo de la bala
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        /*
        switch (hatDir)
        {
            case Direction.UP:
                direction = Vector3.up;
                pAnimator.Play("Hat_Clown_Up");
                break;
            case Direction.DOWN:
                direction = Vector3.down;
                Debug.Log("Sombrero dispara abajo");
                break;
            case Direction.RIGHT:
                direction = Vector3.right;
                Debug.Log("Sombrero dispara derecha");
                break;
            case Direction.LEFT:
                direction = Vector3.left;
                Debug.Log("Sombrero dispara izquierda");
                break;
            default:
                // Si la direcci�n no est� definida, no disparamos
                Debug.LogWarning("La direcci�n del sombrero no est� definida. No se puede disparar.");
                break;
        }
        */

        // Determinar la animaci�n del sombrero seg�n el �ngulo de disparo de la bala
        if ((angle >= 315 && angle <= 360) || (angle >= 0 && angle < 45))
        {
            pAnimator.Play("Hat_Clown_Right");
        }
        else if (angle >= 45 && angle < 135)
        {
            pAnimator.Play("Hat_Clown_Up");
        }
        else if (angle >= 135 && angle < 225)
        {
            pAnimator.Play("Hat_Clown_Left");
        }
        else if (angle >= 225 && angle < 315)
        {
            pAnimator.Play("Hat_Clown_Down");
        }

        // Instanciar la bala en la direcci�n aleatoria
        InstantiateBullet(anchorUp, ramdomDirection);

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
                pAnimator.Play("Hat_Clown_Up");
                break;
            case Direction.DOWN:
                pAnimator.Play("Hat_Clown_Down");
                break;
            case Direction.RIGHT:
                pAnimator.Play("Hat_Clown_Right");
                break;
            case Direction.LEFT:
                pAnimator.Play("Hat_Clown_Left");
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
}
