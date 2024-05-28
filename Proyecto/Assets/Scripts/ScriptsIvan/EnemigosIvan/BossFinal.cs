using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFinal : MonoBehaviour
{
    public int vidaActual;
    public int vidaMax = 100;

    public Color colorDaño;
    public Color colorOriginal;
    public SpriteRenderer rbSprite;
    public Collider2D collider;
    public float TCambioColor = 0.1f;

    public Rigidbody2D rb;
   

    public int speed = 4;

    public JugadorV1 Player;

    public Transform tpPosicion;
    public Transform[] spawnEsferas;
    public GameObject esferaPrefab;
    private List<GameObject> esferas = new List<GameObject>();
    bool esferasInstanciadas = false;

    public GameObject balaPrefab;
    public Transform bulletPos;
    public float rangoDispersion = 20f;
    public float contador = 0f;
    public float frecuenciaDisparo = 2f;
    public BalaJefe balaJefe;

    public Transform TpFase4;
    public float fuerzaLanzamiento = 5f; // Fuerza de lanzamiento diagonal hacia la izquierda
    public float coeficienteRestitucion = 0.5f;

    [SerializeField]
    private Animator animator;

    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject prefab3;

    public Transform[] wave1SpawnPositions;
    public Transform[] wave2SpawnPositions;
    public Transform[] wave3SpawnPositions;
    public Transform[] wave4SpawnPositions;

    private SpriteRenderer spriteRenderer;
    private int enemiesToDefeat = 0;






    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<JugadorV1>();
        rbSprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        balaJefe = FindObjectOfType<BalaJefe>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        colorOriginal = rbSprite.color;
        vidaActual = vidaMax;

    }

    // Update is called once per frame
    void Update()
    {
        Fases();
    }

    private void Fases()
    {
       
        if (vidaActual > 75 && vidaActual <= 100  )
        {
            PerseguirAlJugador();
            animator.Play("Boss_SpinLoop");
        }

        else if (vidaActual > 50 && vidaActual <= 75  )
        {
            Fase2();
            animator.Play("Boss_Sleep");
        }

        else if (vidaActual > 25 && vidaActual <= 50 )
        {
            DestruirEsferas();
            Fase3();
            animator.Play("Boss_SpinLoop"); // En el ultimo frame se llama al metodo continuar animacion.
        }

        else if (vidaActual >= 0)
        {
            Fase4();
        }

        else if (vidaActual <= 0 )
        {
            Muerte();
        }

    }

    public void IdleFase()
    {
        animator.Play("Boss_Idle");
    }


    public void PerseguirAlJugador()
    {
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
    }

    

    void Fase2()
    {
        // Desactivar y esperar
        //rbSprite.enabled = false;
        //collider.enabled = false;
        Invoke("Reaparecer", 1f); // Llamar a Reaparecer después de 1 segundo
    }

    void Reaparecer()
    {
        // Reaparecer en la nueva posición
        transform.position = tpPosicion.position;
        //rbSprite.enabled = true;
        //collider.enabled = true;


        // Instanciar esferas si aún no se han instanciado
        if (!esferasInstanciadas)
        {
            InstanciarEsferas();
            esferasInstanciadas = true;
           
        }

        // Verificar la vida actual
     
    }

    void InstanciarEsferas()
    {
        // Instanciar una sola vez las esferas en posiciones asignadas en el editor
        for (int i = 0; i < 2 && i < spawnEsferas.Length; i++) // Asegurarse de no exceder el tamaño del array
        {
            GameObject esfera = Instantiate(esferaPrefab, spawnEsferas[i].position, spawnEsferas[i].rotation);
            esferas.Add(esfera);
        }
    }

    void DestruirEsferas()
    {
        // Destruir esferas al terminar la fase
        foreach (GameObject esfera in esferas)
        {
            Destroy(esfera);
        }
        esferas.Clear();
    }

    void Fase3()
    {
        PerseguirAlJugador();
        

      
        contador += Time.deltaTime;


        if (contador >= frecuenciaDisparo)
        {
          
            // Instanciar la bala
            Instantiate(balaPrefab, bulletPos.position, Quaternion.identity);
            
            contador = 0f;
        }


    }

    void Fase4()
    {
        // Detener el movimiento del jefe
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // Teletransportar al jefe a la posición de destino
        transform.position = TpFase4.position;



        // Aplicar una fuerza de lanzamiento diagonal hacia la izquierda al jefe
        rb.AddForce(Vector2.left * fuerzaLanzamiento, ForceMode2D.Impulse);




        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("BalaJugador"))
        {
            Destroy(collision.gameObject);
            DañoRecibido(25);
        }

        if (collision.gameObject.CompareTag("Pared"))
        {
            // Obtener la velocidad antes del rebote
            Vector2 velocidadAntesDelRebote = rb.velocity;

            // Calcular la velocidad reflejada
            Vector2 velocidadReflejada = Vector2.Reflect(velocidadAntesDelRebote, collision.contacts[0].normal) * coeficienteRestitucion;

            // Asignar la velocidad reflejada al jefe
            rb.velocity = velocidadReflejada;
        }
    }

    public void Muerte()

    {
        Destroy(this.gameObject);
      
    }

    public void DañoRecibido(int cantidad)

    {
        vidaActual = vidaActual - cantidad;
        CambiarColor(colorDaño);
       
       

        StartCoroutine(RevertirColorDespuesDeTiempo(TCambioColor)); // Llamo a la co-rutina para devolver el color original
    }

    IEnumerator RevertirColorDespuesDeTiempo(float tiempo) // Co-rutina para devolver el color original al enemigo
    {
        yield return new WaitForSeconds(tiempo); // Espera el tiempo especificado

        // Vuelve al color original después del tiempo especificado
        rbSprite.color = colorOriginal;
    }

    public void CambiarColor(Color color)
    {
        rbSprite.color = color; // Cambia el color del jugador                                
    }

    public void ActivoFases()
    {
        //puedeSeguir = true;
    }

    public void AnimacionSpinLoop()
    {
        animator.Play("Boss_SpinLoop");
    }



    IEnumerator StartBattle()
    {
        // Desaparecer el sprite del jefe al inicio del combate
        spriteRenderer.enabled = false;

        // Invocar la primera oleada de enemigos
        yield return InvokeWave(new GameObject[] { prefab1 }, 4, wave1SpawnPositions);

        // Invocar la segunda oleada con 2 prefab2 y 2 prefab1
        yield return InvokeWave(new GameObject[] { prefab2, prefab1 }, 2, wave2SpawnPositions);

        // Invocar la tercera oleada con solo prefab3
        yield return InvokeWave(new GameObject[] { prefab3 }, 4, wave3SpawnPositions);

        // Invocar la cuarta oleada con 2 prefab1, 2 prefab2, y 2 prefab3
        yield return InvokeWave(new GameObject[] { prefab1, prefab2, prefab3 }, 2, wave4SpawnPositions);

        // Esperar 1 segundo antes de reaparecer el sprite del jefe
        yield return new WaitForSeconds(1f);

        // Reaparecer el sprite del jefe al finalizar todas las oleadas
        spriteRenderer.enabled = true;
        Debug.Log("All waves completed, boss appears!");
    }

    IEnumerator InvokeWave(GameObject[] enemyPrefabs, int enemyCountPerPrefab, Transform[] spawnPositions)
    {
        Debug.Log("Invoking wave with enemies: " + string.Join(", ", (object[])enemyPrefabs));

        enemiesToDefeat = enemyCountPerPrefab * enemyPrefabs.Length;

        // Esperar 1 segundo antes de invocar enemigos si no es la primera oleada
        if (enemyPrefabs.Length > 1 || enemyPrefabs[0] != prefab1)
        {
            yield return new WaitForSeconds(1f);
        }

        int spawnIndex = 0;

        for (int i = 0; i < enemyCountPerPrefab; i++)
        {
            foreach (var prefab in enemyPrefabs)
            {
                if (spawnIndex >= spawnPositions.Length)
                {
                    Debug.LogWarning("Not enough spawn positions set in the editor. Repeating spawn positions.");
                    spawnIndex = 0;
                }

                Instantiate(prefab, spawnPositions[spawnIndex].position, Quaternion.identity);
                spawnIndex++;
            }
        }

        // Esperar hasta que todos los enemigos sean derrotados
        while (enemiesToDefeat > 0)
        {
            yield return null;

        }
    }

    public void EnemyDefeated()
    {
        enemiesToDefeat--;
    }
}



