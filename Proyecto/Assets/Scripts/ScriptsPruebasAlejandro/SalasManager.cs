using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalasManager : MonoBehaviour
{

    public string teleportTargetTag = "TeleportTarget";     // Tag de los objetos de teletransporte
    public string currentRoomTag = "SalaActual";            // Tag del objeto vacío que representa la sala actual

    private List<GameObject> enemigosEnSala = new List<GameObject>(); // Lista para almacenar enemigos en la sala

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Método para desactivar los colliders de las puertas
    void DesactivarCollidersPuertas()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Door"))
            {
                Collider2D collider = child.GetComponent<Collider2D>();
                if (collider != null)
                {
                    collider.enabled = false;
                }
            }
        }
    }

    // Método para activar los colliders de las puertas
    void ActivarCollidersPuertas()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Door"))
            {
                Collider2D collider = child.GetComponent<Collider2D>();
                if (collider != null)
                {
                    collider.enabled = true;
                }
            }
        }
    }

    public void DesactivarTeleportTargets()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag(teleportTargetTag))
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    public void ActivarTeleportTargets()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag(teleportTargetTag))
            {
                child.gameObject.SetActive(true);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemigo"))
        {
            enemigosEnSala.Add(other.gameObject);
        }

        if (other.CompareTag(currentRoomTag))
        {
            // Cada vez que se entra a una habitación nueva, se desactivan los colliders de las puertas y se activa la animación de cerrar puertas
            // DesactivarCollidersPuertas();
            DesactivarTeleportTargets();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(currentRoomTag))
        {
            ActivarTeleportTargets();           
        }
    }

    // En el GameManager, cuando se detecte que la sala ya no tiene enemigos, se llama al método, por ejemplo, HabitacionLimpia()
    /*
    public void HabitacionLimpia()
    {
        salasManager.ActivarCollidersPuertas(); // Activar colliders de puertas
    }
    */

    public void EnemigoDestruido(GameObject enemigo)
    {
        enemigosEnSala.Remove(enemigo);
    }

    // En el script Enemigo, de debería tener el siguiente código cada vez que se destruye
    /*
            public class Enemigo : MonoBehaviour
            {
                public SalasManager salasManager;

                private void OnDestroy()
                {
                    if (salasManager != null)
                    {
                        salasManager.EnemigoDestruido(gameObject);
                    }
                }
            }
    */

    // Método para verificar si no hay enemigos en la sala y activar los colliders de las puertas
    void ActualizarCollidersPuertas()
    {
        if (enemigosEnSala.Count == 0)
        {
            ActivarCollidersPuertas();
        }
        else
        {
            DesactivarCollidersPuertas();
        }
    }
}
