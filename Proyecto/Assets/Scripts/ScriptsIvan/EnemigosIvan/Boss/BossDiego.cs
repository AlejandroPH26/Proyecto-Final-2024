using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDiego : MonoBehaviour
{
    public enum BossState { PHASE1, PHASE2, PHASE3, PHASE4, DEATH, DEFAULT };
    public BossState state = BossState.PHASE1;

	// Variables script boss original
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
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ComportamientoBoss()
    {
        switch (state)
        {
            case BossState.PHASE1:
				Phase1();
                break;
            case BossState.PHASE2: 
				Phase2();
                break;  
            case BossState.PHASE3: 
				Phase3();
                break;
            case BossState.PHASE4:
				Phase4();
                break;
                        
        }
    }

    private void Phase1()
    {
		// Código fase 1
		
		// comprobación cambio a fase 2
		if(Health >= 0.75f * maxHealth){
			state = BossState.PHASE2;
		}
    }

    private void Phase2()
    {
		// Código fase 2
		
		// comprobación cambio a fase 3
		if(Health >= 0.5f * maxHealth){
			state = BossState.PHASE2;
		}
    }

    private void Phase3() 
    {
		// Código fase 3
		
		// comprobación cambio a fase 2
		if(Health >= 0.25f * maxHealth){
			state = BossState.PHASE2;
		}
    }

    private void Phase4() 
    { 
		// Código fase 4
		
		// comprobación muerte
		if(Health <= 0){
			state = BossState.DEATH;
		}
    }
}
