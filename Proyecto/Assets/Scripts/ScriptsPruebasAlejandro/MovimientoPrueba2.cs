using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPrueba2 : MonoBehaviour
{
    public float speed = 5f;                                // Velocidad de movimiento del jugador
    public float cameraSpeed = 2f;                          // Velocidad del paneo de la cámara
    private Rigidbody2D rb;                                 // Referencia al Rigidbody2D del jugador

    public Transform mainCamera;                            // Referencia al transform de la cámara principal
    public Transform player;                                // Referencia al transform del jugador
    public string teleportTargetTag = "TeleportTarget";     // Tag de los objetos de teletransporte
    public string currentRoomTag = "SalaActual";            // Tag del objeto vacío que representa la sala actual

    private bool playerInRoom = false;                      // Variable para verificar si el jugador está en la sala actual
    private bool isCameraMoving;                            // Variable para rastrear si la cámara está en movimiento

    // Posición inicial y final de la cámara para el paneo
    private Vector3 initialCameraPosition;
    private Vector3 targetCameraPosition;
    private float lerpProgress = 0.01f;                     // Progreso del paneo

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtener el Rigidbody2D del jugador
    }

    void Update()
    {
        // Obtener la entrada del teclado
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calcular el movimiento basado en la entrada del teclado
        Vector2 movement = new Vector2(horizontalInput, verticalInput) * speed;

        // Aplicar el movimiento al Rigidbody2D del jugador
        rb.velocity = movement;


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el jugador ha colisionado con una puerta
        if (other.CompareTag("Door"))
        {
            // Buscar todos los objetos vacíos con la etiqueta TeleportTarget
            GameObject[] teleportTargets = GameObject.FindGameObjectsWithTag(teleportTargetTag);

            // Encontrar el objetivo de teletransporte más cercano al jugador
            Transform closestTarget = null;
            float closestDistance = Mathf.Infinity;

            foreach (GameObject target in teleportTargets)
            {
                float distanceToTarget = Vector2.Distance(player.position, target.transform.position);
                if (distanceToTarget < closestDistance)
                {
                    closestTarget = target.transform;
                    closestDistance = distanceToTarget;
                }
            }

            // Teletransportar al jugador al objetivo de teletransporte más cercano
            if (closestTarget != null)
            {
                player.position = closestTarget.position;
            }
        }

        // Verificar si el jugador está en contacto con el trigger de SalaActual
        if (other.CompareTag(currentRoomTag))
        {
            // Obtener el objeto de la SalaActual
            GameObject salaActual = other.gameObject;

            // Desactivar todos los TeleportTarget que sean hijos de la SalaActual
            foreach (Transform child in salaActual.transform)
            {
                if (child.CompareTag(teleportTargetTag))
                {
                    child.gameObject.SetActive(false);
                }
            }

            // Teletransportar la cámara al objeto CameraTarget de la sala actual
            Transform cameraTarget = salaActual.transform.Find("CameraTarget");
            if (cameraTarget != null)
            {
                // Configurar los valores para el paneo de la cámara
                initialCameraPosition = mainCamera.position;
                targetCameraPosition = cameraTarget.position;
                // Iniciar el paneo de la cámara
                StartCoroutine(MoveCameraSmoothly());
            }
        }
    }

    // Corrutina para mover suavemente la cámara
    IEnumerator MoveCameraSmoothly()
    {
        // Marcar que la cámara está en movimiento
        isCameraMoving = true;
        // Pausar el tiempo del juego
        Time.timeScale = 0f;
        // Esperar un frame para asegurarse de que el tiempo se haya pausado correctamente
        yield return null;

        lerpProgress = 0f;
        while (lerpProgress < 1f)
        {
            // Incrementar el progreso del paneo
            lerpProgress += Time.unscaledDeltaTime * cameraSpeed;
            // Aplicar la función Lerp para mover suavemente la cámara hacia su destino
            mainCamera.position = Vector3.Lerp(initialCameraPosition, targetCameraPosition, lerpProgress);
            yield return null; // Esperar hasta el siguiente frame
        }

        // Restablecer valores y reanudar el tiempo del juego
        isCameraMoving = false;
        Time.timeScale = 1f;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Verificar si el jugador ya no está en contacto con la SalaActual
        if (other.CompareTag(currentRoomTag))
        {
            playerInRoom = false; // Establecer que el jugador ya no está en la sala actual

            // Obtener el objeto de la SalaActual
            GameObject salaActual = other.gameObject;

            // Activar todos los TeleportTarget que sean hijos de la SalaActual
            foreach (Transform child in salaActual.transform)
            {
                if (child.CompareTag(teleportTargetTag))
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }
}
