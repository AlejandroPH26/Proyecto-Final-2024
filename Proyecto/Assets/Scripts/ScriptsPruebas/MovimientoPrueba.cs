using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPrueba : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento del jugador
    private Rigidbody2D rb; // Referencia al Rigidbody2D del jugador

    public Transform mainCamera; // Referencia al transform de la cámara principal
    public Transform player; // Referencia al transform del jugador
    public string teleportTargetTag = "TeleportTarget"; // Tag de los objetos de teletransporte
    public string currentRoomTag = "SalaActual"; // Tag del objeto vacío que representa la sala actual

    private bool playerInRoom = false; // Variable para verificar si el jugador está en la sala actual

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtener el Rigidbody2D del jugador
    }

    // Update se llama una vez por frame
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
                mainCamera.position = cameraTarget.position;
            }

        }
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
