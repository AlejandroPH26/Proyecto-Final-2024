using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFinal : MonoBehaviour

{
    public float health = 100f;
    public float movementSpeed = 5f;
    public Transform spawnPoint; // Punto de aparición para la fase 2
    public GameObject spherePrefab; // El prefab de la esfera
    public Transform sphereSpawn1; // Posición de spawn para la primera esfera
    public Transform sphereSpawn2; // Posición de spawn para la segunda esfera

    private GameObject player;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isMoving = true;
    private bool spheresSpawned = false;

    public float shootInterval = 2f; // Intervalo de tiempo entre disparos
    public GameObject bulletPrefab; // El prefab de la bala
    private float shootTimer = 0f; // Temporizador para el disparo
    public Transform bulletSpawnPoint; // Punto de aparición de las balas

    private bool isPhase3 = false;
    private bool isPhase4 = false;

    private GameObject spawnedSphere1;
    private GameObject spawnedSphere2;

    public Transform phase4Position; // Posición a la que se moverá en la fase 4

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (health <= 0)
        {
            Defeated();
            return;
        }

        if (health > 75 && isMoving)
        {
            Phase1();
        }
        else if (health <= 75 && health > 50 && isMoving)
        {
            Phase2();
        }
        else if (health <= 50 && health > 25 && !isPhase3)
        {
            Phase3();
        }
        else if (health <= 25 && !isPhase4)
        {
            Phase4();
        }

        if (isPhase3)
        {
            Invoke("Phase3Behavior", 3);
        }
    }

    void Phase1()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.velocity = direction * movementSpeed;
        animator.SetBool("IsMoving", true);
    }

    void Phase2()
    {
        animator.SetTrigger("Disappear");
        isMoving = false;
        rb.velocity = Vector2.zero;
    }

    void Phase3()
    {
        isPhase3 = true;
        rb.velocity = Vector2.zero;
        DestroySpawnedSpheres(); // Destruir esferas al comienzo de la fase 3
        animator.SetTrigger("startSpin");
    }

    void DestroySpawnedSpheres()
    {
        if (spawnedSphere1 != null)
        {
            Destroy(spawnedSphere1);
        }
        if (spawnedSphere2 != null)
        {
            Destroy(spawnedSphere2);
        }
    }

    void Phase3Behavior()
    {
        // Persigue al jugador
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.velocity = direction * movementSpeed;

        // Controlar el disparo de balas
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

            shootTimer = 0f;
        }
    }

    void Phase4()
    {
       
            isPhase4 = true;
            // Movimiento hacia la posición designada
            rb.velocity = (phase4Position.position - transform.position).normalized * movementSpeed;

            // Si el jefe está cerca de la posición designada, detén su movimiento
            if (Vector2.Distance(transform.position, phase4Position.position) < 0.1f)
            {
                rb.velocity = Vector2.zero;
            }

            // Aquí puedes definir el comportamiento adicional de la fase 4 si es necesario.
    }

    public void MoveToPosition() // se llama en el último frame de la animación de desaparecer
    {
        transform.position = spawnPoint.position;
        animator.SetTrigger("Appear");
        isMoving = true;
    }

    public void OnDisappearAnimationComplete() // se llama desde la animación de aparecer
    {
        // Una vez que la animación de "desaparecer" ha terminado, instanciamos las esferas
        if (!spheresSpawned)
        {
            spawnedSphere1 = Instantiate(spherePrefab, sphereSpawn1.position, Quaternion.identity);
            spawnedSphere2 = Instantiate(spherePrefab, sphereSpawn2.position, Quaternion.identity);
            spheresSpawned = true;
        }
    }

    public void OnAppearAnimationComplete() // se llama en el último frame de la animación de aparecer
    {
        animator.SetTrigger("Sleep");
    }

    public void OnStartSpinAnimationComplete() // se llama al final de la animación "StartSpin"
    {
        animator.SetTrigger("Spin");
    }

    public void OnSpin2AnimationComplete() // se llama al final de la animación "Spin2"
    {
        // Aquí no hacemos nada ya que la animación "Spin2" es loopeada y la fase 3 continúa
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
            Defeated();
    }

    void Defeated()
    {
        Debug.Log("El jefe ha sido derrotado!");
        Destroy(this.gameObject);
        rb.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("BalaJugador"))
        {
            Destroy(collision.gameObject);
            TakeDamage(25);
        }
    }
}