using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JugadorV1 : MonoBehaviour
{
    [SerializeField] public List<ISombreros> sombreros = new List<ISombreros>(); // Lista de sombreros del jugador
    private MovimientoCabezaJugador cabeza;
    public GameManagerHats gm;

    public float speed = 5f;
    public float initialSpeed;
    public float slowedSpeed = 3f;
    private Rigidbody2D rb;

    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveDown = KeyCode.S;
    public KeyCode moveRight = KeyCode.D;
    public float y = -1;
    public float x = 0;

    public SpriteRenderer rbSprite;
    public Animator pAnimator;
    private bool isMoving = false;

    // public int vidaActual;
    // public int vidaMax = 100;

    // Agrega una variable para el tiempo que el SpriteRenderer debe estar activo
    public float activationTime;
    private bool isActivating = false;

    public Transform player;
    public Transform anchorInitial;
    public string teleportTargetTag = "TeleportTarget";
    public string currentRoomTag = "SalaActual";

    private bool hatInFrame = false;


    private CameraManager cameraManager;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManagerHats.instance;
        rbSprite = GetComponent<SpriteRenderer>();
        pAnimator = GetComponent<Animator>();
        cabeza = GetComponentInChildren<MovimientoCabezaJugador>();
        rb = GetComponent<Rigidbody2D>();                           // Obtener el Rigidbody2D del jugador
        cameraManager = FindObjectOfType<CameraManager>();
        sombreros = GetComponentsInChildren<ISombreros>().ToList();
        // vidaActual = vidaMax;
        activationTime = gm.DelayInvulnerabilidad;
        initialSpeed = speed;

    }

    // Update se llama una vez por frame
    void Update()
    {
        // Manejo de la entrada de movimiento del jugador
        InputJugador();
        // Manejo de la entrada de sombreros (disparo)
        InputHats();
        hatInFrame = false;
        DamageTaken();

    }

    private void InputJugador()
    {
        Vector2 aux = Vector2.zero; // Declaramos el Vector2 para el movimiento y lo identificamos como aux la variable y ponemos que es 0 para que en caso de no pulsar nada se quede quieto
        if (Input.GetKey(moveUp) && Input.GetKey(moveLeft))// Diagonal arriba izquierda
        {
            aux.y = 1;
            aux.x = -1;
            pAnimator.Play("WalkUp_Personaje");
            if (!cabeza.isShooting)
            {
                cabeza.orientarCabeza(Direction.UP);
                SetDirectionForHats(Direction.UP);
            }

        }
        else if (Input.GetKey(moveUp) && Input.GetKey(moveRight))// Diagonal arriba izquierda
        {
            aux.y = 1;
            aux.x = 1;
            pAnimator.Play("WalkUp_Personaje");
            if (!cabeza.isShooting)
            {
                cabeza.orientarCabeza(Direction.UP);
                SetDirectionForHats(Direction.UP);
            }
        }
        else if (Input.GetKey(moveDown) && Input.GetKey(moveLeft))// Diagonal arriba izquierda
        {
            aux.y = -1;
            aux.x = -1;
            pAnimator.Play("WalkDown_Personaje");
            if (!cabeza.isShooting)
            {
                cabeza.orientarCabeza(Direction.DOWN);
                SetDirectionForHats(Direction.DOWN);
            }
        }
        else if (Input.GetKey(moveDown) && Input.GetKey(moveRight))// Diagonal arriba izquierda
        {
            aux.y = -1;
            aux.x = 1;
            pAnimator.Play("WalkDown_Personaje");
            if (!cabeza.isShooting)
            {
                cabeza.orientarCabeza(Direction.DOWN);
                SetDirectionForHats(Direction.DOWN);
            }
        }
        else if (Input.GetKey(moveUp)) // Comprobamos que se está pulsando la tecla W
        {
            // Nos desplazamos (sumamos movimiento) hacia arriba (eje y = 1), multiplicamos por deltatime
            // para que el movimiento no dependa del framerate ya que lo gestiona el motor de fisicas.
            aux.y = 1;
            pAnimator.Play("WalkUp_Personaje");
            if (!cabeza.isShooting)
            {
                cabeza.orientarCabeza(Direction.UP);
                SetDirectionForHats(Direction.UP);
            }
        }
        else if (Input.GetKey(moveDown))
        {
            // Nos desplazamos (sumamos movimiento) hacia abajo (eje y = -1), multiplicamos por deltatime
            aux.y = -1;
            pAnimator.Play("WalkDown_Personaje");
            if (!cabeza.isShooting)
            {
                cabeza.orientarCabeza(Direction.DOWN);
                SetDirectionForHats(Direction.DOWN);
            }
        }
        else if (Input.GetKey(moveLeft))
        {
            // Nos desplazamos (sumamos movimiento) hacia la izquierda (eje x = -1), multiplicamos por deltatime
            aux.x = -1;
            pAnimator.Play("WalkLeft_Personaje");
            if (!cabeza.isShooting)
            {
                cabeza.orientarCabeza(Direction.LEFT);
                SetDirectionForHats(Direction.LEFT);
            }
        }
        else if (Input.GetKey(moveRight))
        {
            // Nos desplazamos (sumamos movimiento) hacia la derecha (eje x = 1), multiplicamos por deltatime
            aux.x = 1;
            pAnimator.Play("WalkRight_Personaje");
            if (!cabeza.isShooting)
            {
                cabeza.orientarCabeza(Direction.RIGHT);
                SetDirectionForHats(Direction.RIGHT);
            }

        }
        if (aux.x == 0 && aux.y == 0)
        {
            // Nos desplazamos (sumamos movimiento) hacia la derecha (eje x = 1), multiplicamos por deltatime
            //pAnimator.Play("IdleDown_Personaje");
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
        pAnimator.SetBool("Moving", isMoving);
        rb.velocity = aux.normalized * speed; // Aqui lo normalizamos y lo multiplicamos por speed para que no vaya mas rapido en diagonal


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el jugador colisionó con una puerta
        if (other.CompareTag("Door"))
        {
            GameObject[] teleportTargets = GameObject.FindGameObjectsWithTag(teleportTargetTag);
            Transform closestTarget = null;
            float closestDistance = Mathf.Infinity;

            // Itera sobre todos los objetivos de teletransporte encontrados
            foreach (GameObject target in teleportTargets)
            {
                // Calcula la distancia entre el jugador y el objetivo actual
                float distanceToTarget = Vector2.Distance(player.position, target.transform.position);
                // Verifica si la distancia actual es menor que la distancia más cercana encontrada hasta el momento
                if (distanceToTarget < closestDistance)
                {
                    closestTarget = target.transform;
                    closestDistance = distanceToTarget;
                }
            }

            if (closestTarget != null)
            {
                player.position = closestTarget.position;
            }
        }
        // Verifica si el jugador colisionó con la sala actual
        if (other.CompareTag(currentRoomTag))
        {
            SalasManager salaManager = other.GetComponent<SalasManager>();
            if (salaManager != null)
            {
                salaManager.DesactivarTeleportTargets();
            }

            Transform cameraTarget = other.transform.Find("CameraTarget");
            if (cameraTarget != null)
            {
                cameraManager.MoveCameraSmoothly(cameraManager.mainCamera.position, cameraTarget.position);
            }
        }
        // Verificar si el jugador colisionó con un sombrero
        if (other.CompareTag("Sombrero") && !hatInFrame)
        {
            hatInFrame = true;
            // Obtener el sombrero
            GameObject sombrero = other.gameObject;

            Collider2D sombreroCollider = sombrero.GetComponent<Collider2D>();
            if (sombreroCollider != null)
            {
                sombreroCollider.enabled = false;
            }

            
            sombrero.transform.SetParent(gameObject.transform); // Hacer que el sombrero sea un hijo del jugador         
            sombreros.Add(sombrero.GetComponent<ISombreros>()); // Agregar el sombrero a la lista de sombreros del jugador           
            PosicionarSombreros();                              // Posicionar los sombreros uno sobre otro

            // SONIDO PARA CUANDO SE POE EL SOMBRERO

            // Informar al sombrero de que ha sido recogido
            // InformarSombreroRecogido(sombrero.GetComponent<ISombreros>());

        }
        // Verificar si el jugador colisionó con un botiquín
        if (other.CompareTag("Botiquin"))
        {
            Debug.Log("Se ha chocado con un botiquin");
            Destroy(other.gameObject);

            gm.SumarVidas();
        }
        // Verificar si el jugador colisionó con una bomba
        if (other.CompareTag("BombaItem"))
        {
            Destroy(other.gameObject);
            gm.SumarBombas();
        }
        // Verificar si el jugador colisionó con los clavos
        if (other.CompareTag("ClavosObstaculo"))
        {
            gm.RestarVidas();
        }
        // Verificar si el jugador colisionó con las zarzas
        if (other.CompareTag("ZarzasObstaculo"))
        {
            speed = speed - slowedSpeed;
        }
    }

    private void PosicionarSombreros()
    {
        int n = sombreros.Count;
        Debug.Log(n.ToString() + " Sombreros");
        // Verificar si hay menos de un sombrero en la lista
        if (n <= 1)
        {
            // Obtener la posicion de anchorInitial
            Vector3 anchorInitialPosition = anchorInitial.position;

            // Iterar a traves de los sombreros desde el segundo hasta el ultimo
            for (int i = 0; i < n; i++)
            {
                // Obtener el transform del sombrero actual
                Transform sombreroTransform = sombreros[i].gameObject.transform;

                // Establecer la posicion del sombrero actual en la posicion de anchorInitial
                sombreroTransform.position = anchorInitialPosition;
            }
        }
        // Verificar si hay al menos dos sombreros en la lista
        if (n >= 2)
        {
            // Iterar a través de los sombreros desde el segundo hasta el último
            for (int i = 1; i < n; i++)
            {
                // Obtener el transform del sombrero actual
                Transform sombreroTransform = sombreros[i].gameObject.transform;
                // Obtener el SpriteRenderer del sombrero actual
                SpriteRenderer spriteRenderer = sombreros[i].gameObject.GetComponent<SpriteRenderer>();

                Transform anclajeSuperior = sombreros[i - 1].anclajeSuperior;

                // Verificar si los sombreros tienen los anchors necesarios
                if (anclajeSuperior != null)
                {
                    // Posicionar el sombrero actual utilizando el anchorDown del sombrero anterior
                    sombreroTransform.position = anclajeSuperior.position;
                    // Obtener el orden en la capa del sombrero anterior
                    int orderInLayer = sombreros[i - 1].gameObject.GetComponent<SpriteRenderer>().sortingOrder;
                    // Establecer el mismo orden en la capa para el sombrero actual
                    spriteRenderer.sortingOrder = i;

                }

                else
                {
                    Debug.LogError("Los anchors 'anchorUp' o 'anchorDown' no están asignados en los sombreros correspondientes.");
                }
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        // Verificar si el jugador ya no está en contacto con la SalaActual
        if (other.CompareTag(currentRoomTag))
        {
            SalasManager salaManager = other.GetComponent<SalasManager>();
            if (salaManager != null)
            {
                salaManager.ActivarTeleportTargets();
            }
        }
        // Verificar si el jugador ya no está en contacto con las zarzas
        if (other.CompareTag("ZarzasObstaculo"))
        {
            speed = initialSpeed;
        }
    }

    /*void InformarSombreroRecogido(ISombreros sombreroRecogido)
    {
        // Notificar al sombrero recogido de que ha sido recogido
        if (sombreroRecogido != null)
        {
            sombreroRecogido.SombreroRecogido();
        }
    }*/

    public void InputHats()
    {
        // Manejo de la entrada para disparar y asignar dirección a los sombreros
        Vector3 direction = Vector3.zero;
        // Establecer las direcciones de las teclas de disparo
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = Vector3.up;
            SetDirectionForHats(Direction.UP);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = Vector3.down;
            SetDirectionForHats(Direction.DOWN);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Vector3.right;
            SetDirectionForHats(Direction.RIGHT);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Vector3.left;
            SetDirectionForHats(Direction.LEFT);
        }
        // Después de establecer la dirección, realizar el disparo
        if (direction != Vector3.zero)
        {
            Shoot();
        }
    }

    void SetDirectionForHats(Direction dir)
    {
        // Establecer la dirección para todos los sombreros asociados
        foreach (var sombrero in sombreros)
        {
            sombrero.SetDirection(dir);
        }
    }

    public void Shoot()
    {
        // Iterar sobre cada objeto en el arreglo isombreros
        foreach (var sombrero in sombreros)
        {
            // Llamar al método Shoot() en cada objeto
            sombrero.Shoot();
        }
    }

    //Codigo Ivan (Vida y muerte del jugador), (provisional)
    public void DamageTaken()
    {
        if (gm.vidas <= 0)
        {
            MuerteJugador();
            Debug.Log("Se destruye el cuerpo");
        }
    }

    private void MuerteJugador()
    {
        // AUDIO DE MUERTE

        pAnimator.Play("Anim_Muerte");
        Debug.Log("Se ha muerto");
        // Elimina todos los sombreros de la lista
        foreach (ISombreros sombrero in sombreros)
        {
            // Destruye el GameObject asociado al sombrero
            Destroy(sombrero.gameObject);
        }

        // Limpia la lista de sombreros
        sombreros.Clear();
    }

    // Cambia el color del SpriteRenderer a blanco o rojo
    public void CambiarColor(Color color)
    {
        // Verifica si el jugador está vivo antes de cambiar el color
        if (gm.vidas > 0)
        {
            rbSprite.color = color; // Cambia el color del jugador
                                    // Obtiene todos los SpriteRenderer de los hijos del jugador, incluyendo los hijos de los hijos
            SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
            foreach (SpriteRenderer renderer in spriteRenderers)
            {
                if (renderer != rbSprite) // Evita cambiar el color del jugador dos veces
                {
                    renderer.color = color; // Cambia el color de los hijos
                }
            }
        }
    }
}
