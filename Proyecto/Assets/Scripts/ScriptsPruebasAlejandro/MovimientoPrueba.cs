using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class MovimientoPrueba : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;

    public Transform player;
    public string teleportTargetTag = "TeleportTarget";
    public string currentRoomTag = "SalaActual";

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Door"))
        {
            GameObject[] teleportTargets = GameObject.FindGameObjectsWithTag(teleportTargetTag);
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

            if (closestTarget != null)
            {
                player.position = closestTarget.position;
            }
        }

        if (other.CompareTag(currentRoomTag))
        {
            Transform cameraTarget = other.transform.Find("CameraTarget");
            if (cameraTarget != null)
            {
                CameraManager cameraManager = FindObjectOfType<CameraManager>();
                cameraManager.MoveCameraSmoothly(cameraTarget.position, cameraTarget.position);
            }
        }
    }
}
