using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPrueba2 : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento del jugador
    private Rigidbody2D rb; // Referencia al Rigidbody2D del jugador

    public Transform mainCamera; // Referencia al transform de la c�mara principal
    public Transform player; // Referencia al transform del jugador
    public string teleportTargetTag = "TeleportTarget"; // Tag de los objetos de teletransporte
    public string currentRoomTag = "SalaActual"; // Tag del objeto vac�o que representa la sala actual

    private bool playerInRoom = false; // Variable para verificar si el jugador est� en la sala actual

    // Posici�n inicial y final de la c�mara para el paneo
    private Vector3 initialCameraPosition;
    private Vector3 targetCameraPosition;
    private float lerpProgress = 0.5f; // Progreso del paneo

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
            // Buscar todos los objetos vac�os con la etiqueta TeleportTarget
            GameObject[] teleportTargets = GameObject.FindGameObjectsWithTag(teleportTargetTag);

            // Encontrar el objetivo de teletransporte m�s cercano al jugador
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

            // Teletransportar al jugador al objetivo de teletransporte m�s cercano
            if (closestTarget != null)
            {
                player.position = closestTarget.position;
            }
        }

        // Verificar si el jugador est� en contacto con el trigger de SalaActual
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

            // Teletransportar la c�mara al objeto CameraTarget de la sala actual
            Transform cameraTarget = salaActual.transform.Find("CameraTarget");
            if (cameraTarget != null)
            {
                // Configurar los valores para el paneo de la c�mara
                initialCameraPosition = mainCamera.position;
                targetCameraPosition = cameraTarget.position;
                // Iniciar el paneo de la c�mara
                StartCoroutine(MoveCameraSmoothly());
            }
        }
    }

    // Corrutina para mover suavemente la c�mara
    IEnumerator MoveCameraSmoothly()
    {
        lerpProgress = 0f;
        while (lerpProgress < 1f)
        {
            // Incrementar el progreso del paneo
            lerpProgress += Time.deltaTime * speed;
            // Aplicar la funci�n Lerp para mover suavemente la c�mara hacia su destino
            mainCamera.position = Vector3.Lerp(initialCameraPosition, targetCameraPosition, lerpProgress);
            yield return null; // Esperar hasta el siguiente frame
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Verificar si el jugador ya no est� en contacto con la SalaActual
        if (other.CompareTag(currentRoomTag))
        {
            playerInRoom = false; // Establecer que el jugador ya no est� en la sala actual

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
