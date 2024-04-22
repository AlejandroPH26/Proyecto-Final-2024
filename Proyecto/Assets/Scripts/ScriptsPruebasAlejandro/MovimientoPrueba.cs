using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class MovimientoPrueba : MonoBehaviour
{
    // public float speed = 5f;
    // private Rigidbody2D rb;

    public Transform player;
    public string teleportTargetTag = "TeleportTarget";
    public string currentRoomTag = "SalaActual";

    private CameraManager cameraManager;

    // Start is called before the first frame update
    void Start()
    {
        // rb = GetComponent<Rigidbody2D>(); // Obtener el Rigidbody2D del jugador
        cameraManager = FindObjectOfType<CameraManager>();
    }

    // Update se llama una vez por frame
    void Update()
    {
        // Obtener la entrada del teclado
        // float horizontalInput = Input.GetAxis("Horizontal");
        // float verticalInput = Input.GetAxis("Vertical");

        // Calcular el movimiento basado en la entrada del teclado
        // Vector2 movement = new Vector2(horizontalInput, verticalInput) * speed;

        // Aplicar el movimiento al Rigidbody2D del jugador
        // rb.velocity = movement;
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
}
