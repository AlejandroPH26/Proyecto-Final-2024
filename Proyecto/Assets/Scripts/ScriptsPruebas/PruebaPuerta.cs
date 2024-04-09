using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaPuerta : MonoBehaviour
{
    public Transform cameraTarget; // El punto al que la c�mara se mover�
    public float detectionRadius = 5f; // Radio de detecci�n para encontrar puertas cercanas

    private bool isTransitioning = false; // Variable para verificar si se est� realizando la transici�n

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
            // Iniciar la transici�n solo si no se est� realizando ya
            if (!isTransitioning)
            {
                isTransitioning = true;

                // Buscar todas las puertas cercanas
                Collider2D[] nearbyDoors = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

                // Encontrar la puerta m�s cercana
                Transform closestDoor = FindClosestDoor(nearbyDoors, collision.transform.position);

                if (closestDoor != null)
                {
                    // Teletransportar al jugador a la posici�n de la puerta m�s cercana
                    collision.transform.position = closestDoor.position + new Vector3(4f, 0f, 0f); // A�adir 1 unidad en el eje x para evitar colisi�n inmediata

                    // Mover la c�mara al centro de la habitaci�n
                    MoveCameraToRoomCenter(cameraTarget.position);
                }

                // Reiniciar la variable de transici�n
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

        // Mover la c�mara hacia el punto objetivo (centro de la siguiente sala)
        Vector3 initialPosition = Camera.main.transform.position;
        Vector3 targetPosition = cameraTarget.position;
        float transitionDuration = 2f; // Duraci�n del movimiento de transici�n
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            // Calcular la posici�n intermedia
            float t = elapsedTime / transitionDuration;
            Camera.main.transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        // Asegurarse de que la c�mara est� exactamente en el objetivo
        Camera.main.transform.position = targetPosition;

        // Reanudar el juego
        Time.timeScale = 1f;

        // Reiniciar la variable de transici�n
        isTransitioning = false;
    }

    private void MoveCameraToRoomCenter(Vector3 roomCenter)
    {
        // Mover la c�mara hacia el centro de la habitaci�n
        Vector3 initialPosition = Camera.main.transform.position;
        Vector3 targetPosition = new Vector3(roomCenter.x, roomCenter.y, initialPosition.z);
        float transitionDuration = 2f; // Duraci�n del movimiento de transici�n
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            // Calcular la posici�n intermedia
            float t = elapsedTime / transitionDuration;
            Camera.main.transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
        }

        // Asegurarse de que la c�mara est� exactamente en el objetivo
        Camera.main.transform.position = targetPosition;
    }

}
