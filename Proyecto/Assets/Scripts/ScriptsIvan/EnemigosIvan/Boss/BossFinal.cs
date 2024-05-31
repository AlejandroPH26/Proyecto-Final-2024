using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossFinal : MonoBehaviour

{
    public ActivarBoss ActivacionBoss;

    public float maxHealth = 4000f;
    public float Health;

   
    public BarraDeVida barraDeVida;
   

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

    private bool isPhase2 = false;
    private bool isPhase3 = false;
    private bool isPhase4 = false;
    private bool isPhase4Shooting = false; // Variable para controlar la corrutina de disparo en fase 4

    private GameObject spawnedSphere1;
    private GameObject spawnedSphere2;

    public Transform phase4Position; // Posición a la que se moverá en la fase 4

    public float phase4ShootInterval = 0.1f; // Intervalo de disparo dentro del ciclo de 5 segundos en fase 4

    public GameObject trophy;
    public Transform trophyPos;

  

    private GameManagerHats gm;


    public void Awake()
    {
       barraDeVida = FindObjectOfType<BarraDeVida>();
    }
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gm = FindObjectOfType<GameManagerHats>();
        ActivacionBoss = FindObjectOfType<ActivarBoss>();
        Health = maxHealth;

        
        barraDeVida.InicializarBarraDeVida(Health);
        
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
        if (Health <= 0)
        {
            Defeated();
            return;
        }

        if (Health > 3000 && isMoving)
        {
            Phase1();
        }
        else if (Health <= 3000 && Health > 2000 && isMoving)
        {
            Phase2();
        }
        else if (Health <= 2000 && Health > 1000 && !isPhase3)
        {
            Phase3();
        }
        else if (Health <= 1000)
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

        // variable de control para convertir el Rb del jefe en kinematic
        isPhase2 = true; 
        rb.isKinematic = true;


        // Logica de la fase 2
        animator.SetTrigger("Disappear");
        isMoving = false;
        rb.velocity = Vector2.zero;
    }

    void Phase3()
    {
        // Se Restablece si rigidBody
        isPhase2 = false;
        rb.isKinematic = false;

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

        // Si el jefe está cerca de la posición designada, detén su movimiento e inicia la corrutina de disparo
        if (Vector2.Distance(transform.position, phase4Position.position) < 0.1f)
        {
            rb.velocity = Vector2.zero;

            // Iniciar la corrutina de disparo si no se ha iniciado ya
            if (!isPhase4Shooting)
            {
                isPhase4Shooting = true;
                StartCoroutine(Phase4ShootingCycle());
            }
        }
    }

    private IEnumerator Phase4ShootingCycle()
    {
        while (true)
        {
            // Disparar continuamente cada `phase4ShootInterval` segundos durante 5 segundos
            float shootDuration = 5f;
            float shootEndTime = Time.time + shootDuration;

            while (Time.time < shootEndTime)
            {
                Instantiate(bulletPrefabFase4, bulletPos.position, Quaternion.identity);
                yield return new WaitForSeconds(phase4ShootInterval);
            }

            // Pausa de 3 segundos
            yield return new WaitForSeconds(3f);
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
        Health -= damage;

        barraDeVida.CambiarVidaActual(Health);
       
        if (Health <= 0)
            Defeated();
    }

    void Defeated()
    {
        Debug.Log("El jefe ha sido derrotado!");
        Destroy(this.gameObject);
        rb.velocity = Vector2.zero;
        barraDeVida.gameObject.SetActive(false);
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