using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class MovimientoPrueba : MonoBehaviour
{
    public ISombreros[] isombreros;
    public List<GameObject> sombreros = new List<GameObject>(); // Lista de sombreros del jugador

    public float speed = 5f;
    private Rigidbody2D rb;

    public int vidaActual;
    public int vidaMax = 100;

    public Transform player;
    public string teleportTargetTag = "TeleportTarget";
    public string currentRoomTag = "SalaActual";

    private CameraManager cameraManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtener el Rigidbody2D del jugador
        cameraManager = FindObjectOfType<CameraManager>();
        isombreros = GetComponentsInChildren<ISombreros>();
        vidaActual = vidaMax;
    }

    // Update se llama una vez por frame
    void Update()
    {
        // Manejo de la entrada de movimiento del jugador
        HandleMovementInput();
        // Manejo de la entrada de sombreros (disparo)
        InputHats();
    }

    void HandleMovementInput()
    {
        // Manejo de la entrada de movimiento del jugador
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontalInput, verticalInput) * speed;
        rb.velocity = movement;
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
        if (other.CompareTag("Sombrero"))
        {
            // Obtener el sombrero
            GameObject sombrero = other.gameObject;

            // Hacer que el sombrero sea un hijo del jugador
            sombrero.transform.parent = transform;

            // Agregar el sombrero a la lista de sombreros del jugador
            sombreros.Add(sombrero);

            // Posicionar los sombreros uno sobre otro
            PosicionarSombreros();

            // Informar al sombrero de que ha sido recogido
            InformarSombreroRecogido(sombrero.GetComponent<ISombreros>());
        }
    }

    private void PosicionarSombreros()
    {
        // Verificar si hay al menos dos sombreros en la lista
        if (sombreros.Count >= 2)
        {
            // Iterar a través de los sombreros desde el segundo hasta el último
            for (int i = 1; i < sombreros.Count; i++)
            {
                // Obtener el transform del sombrero actual
                Transform sombreroTransform = sombreros[i].transform;

                // Verificar si los sombreros tienen los anchors necesarios
                if (sombreroTransform.Find("anchorDown") != null && sombreroTransform.Find("anchorUp") != null)
                {
                    // Posicionar el sombrero actual utilizando el anchorDown del sombrero anterior
                    sombreroTransform.position = sombreros[i - 1].transform.Find("anchorDown").position;
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
    }

    void InformarSombreroRecogido(ISombreros sombreroRecogido)
    {
        // Notificar al sombrero recogido de que ha sido recogido
        if (sombreroRecogido != null)
        {
            sombreroRecogido.SombreroRecogido();
        }
    }

    public void InputHats()
    {
        // Manejo de la entrada para disparar y asignar dirección a los sombreros
        Vector3 direction = Vector3.zero;

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
        foreach (var sombrero in isombreros)
        {
            sombrero.SetDirection(dir);
        }
    }

    public void Shoot()
    {
        // Iterar sobre cada objeto en el arreglo isombreros
        foreach (var sombrero in isombreros)
        {
            // Llamar al método Shoot() en cada objeto
            sombrero.Shoot();
        }
    }

    //Codigo Ivan (Vida y muerte del jugador), (provisional)
    public void DamageTaken(int cantidad)

    {
        vidaActual -= cantidad;

        if (vidaActual <= 0)
        {
            Muerte();
        }
    }

    public void Muerte()

    {
        Destroy(this.gameObject);
    }


}
