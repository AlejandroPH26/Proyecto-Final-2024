using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFinal : MonoBehaviour

{
    public ActivarBoss ActivacionBoss;


    public float health = 4000f;
    public float movementSpeed = 5f;
    public Transform spawnPointFase2; // Punto de aparición para la fase 2
    public GameObject spherePrefab; // El prefab de la esfera
    public Transform sphereSpawn1; // Posición de spawn para la primera esfera
    public Transform sphereSpawn2; // Posición de spawn para la segunda esfera

    private GameObject player;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isMoving = true;
    private bool spheresSpawned = false;

    public float shootInterval = 2f; // Intervalo de tiempo entre disparos
    public GameObject bulletPrefabFase3; // El prefab de la bala
    public GameObject bulletPrefabFase4; // El prefab de la bala
    private float shootTimer = 0f; // Temporizador para el disparo
    public Transform bulletPos; // Punto de aparición de las balas

    private bool isPhase3 = false;
    private bool isPhase4 = false;
    

    private GameObject spawnedSphere1;
    private GameObject spawnedSphere2;

    public Transform phase4Position; // Posición a la que se moverá en la fase 4
    
 
    public float shootIntervalFase4 = 2f; // Intervalo de tiempo entre disparos

    public GameObject trophy;
    public Transform trophyPos;

    private GameManagerHats gm;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gm = FindObjectOfType<GameManagerHats>();
       
        ActivacionBoss = FindObjectOfType<ActivarBoss>();
    }

    void Update()
    {

        if (player != null && ActivacionBoss.PuedeMoverse == true)
        {
            ActivarFases();
        }
        else
        {
           
            rb.velocity = Vector2.zero;
        }

        
    }

    public void ActivarFases()
    {
        if (health <= 0)
        {
            Defeated();
            return;
        }

        if (health > 3000 && isMoving)
        {
            Phase1();
        }
        else if (health <= 3000 && health > 2000 && isMoving)
        {
            Phase2();
        }
        else if (health <= 2000 && health > 1000 && !isPhase3)
        {
            Phase3();
        }
        else if (health <= 1000)
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
            Instantiate(bulletPrefabFase3, bulletPos.position, Quaternion.identity);

            shootTimer = 0f;
        }
    }

    void Phase4()
    {
            CancelInvoke("Phase3Behavior"); // cancelo la phase3 para parla y poder ejecutar la fase4


            Debug.Log("Fase4");
            isPhase4 = true;
            // Movimiento hacia la posición designada
            rb.velocity = (phase4Position.position - transform.position).normalized * movementSpeed;

            // Si el jefe está cerca de la posición designada, detén su movimiento
            if (Vector2.Distance(transform.position, phase4Position.position) < 0.1f)
            {
                rb.velocity = Vector2.zero;

            shootTimer += Time.deltaTime;

            if (shootTimer >= shootIntervalFase4)
            {
                Instantiate(bulletPrefabFase4, bulletPos.position, Quaternion.identity);

                shootTimer = 0f;
            }
        }

    }

    public void MoveToPosition() // se llama en el último frame de la animación de desaparecer
    {
        transform.position = spawnPointFase2.position;
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
        Instantiate(trophy, trophyPos.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("BalaJugador"))
        {
            Destroy(collision.gameObject);
            TakeDamage(25);
        }

        if (collision.collider.CompareTag("Player"))
        {
            gm.RestarVidas();
        }
    }
}