using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaPuerta : MonoBehaviour
{
    public Transform cameraTarget; // El punto al que la cámara se moverá
    public float detectionRadius = 5f; // Radio de detección para encontrar puertas cercanas

    private bool isTransitioning = false; // Variable para verificar si se está realizando la transición

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si el objeto que colisiona es el jugador
        if (collision.CompareTag("Player"))
        {
            // Iniciar la transición solo si no se está realizando ya
            if (!isTransitioning)
            {
                isTransitioning = true;

                // Buscar todas las puertas cercanas
                Collider2D[] nearbyDoors = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

                // Encontrar la puerta más cercana
                Transform closestDoor = FindClosestDoor(nearbyDoors, collision.transform.position);

                if (closestDoor != null)
                {
                    // Teletransportar al jugador a la posición de la puerta más cercana
                    collision.transform.position = closestDoor.position + new Vector3(4f, 0f, 0f); // Añadir 1 unidad en el eje x para evitar colisión inmediata

                    // Mover la cámara al centro de la habitación
                    MoveCameraToRoomCenter(cameraTarget.position);
                }

                // Reiniciar la variable de transición
                isTransitioning = false;
            }
        }
    }

    private Transform FindClosestDoor(Collider2D[] doors, Vector3 playerPosition)
    {
        Transform closestDoor = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D doorCollider in doors)
        {
            if (doorCollider.CompareTag("Door"))
            {
                float distanceToDoor = Vector3.Distance(playerPosition, doorCollider.transform.position);
                if (distanceToDoor < closestDistance)
                {
                    closestDistance = distanceToDoor;
                    closestDoor = doorCollider.transform;
                }
            }
        }

        return closestDoor;
    }


    IEnumerator TransitionCoroutine(Transform playerTransform)
    {
        // Pausar el juego
        Time.timeScale = 0f;

        // Mover la cámara hacia el punto objetivo (centro de la siguiente sala)
        Vector3 initialPosition = Camera.main.transform.position;
        Vector3 targetPosition = cameraTarget.position;
        float transitionDuration = 2f; // Duración del movimiento de transición
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            // Calcular la posición intermedia
            float t = elapsedTime / transitionDuration;
            Camera.main.transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        // Asegurarse de que la cámara esté exactamente en el objetivo
        Camera.main.transform.position = targetPosition;

        // Reanudar el juego
        Time.timeScale = 1f;

        // Reiniciar la variable de transición
        isTransitioning = false;
    }

    private void MoveCameraToRoomCenter(Vector3 roomCenter)
    {
        // Mover la cámara hacia el centro de la habitación
        Vector3 initialPosition = Camera.main.transform.position;
        Vector3 targetPosition = new Vector3(roomCenter.x, roomCenter.y, initialPosition.z);
        float transitionDuration = 2f; // Duración del movimiento de transición
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            // Calcular la posición intermedia
            float t = elapsedTime / transitionDuration;
            Camera.main.transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
        }

        // Asegurarse de que la cámara esté exactamente en el objetivo
        Camera.main.transform.position = targetPosition;
    }

}
