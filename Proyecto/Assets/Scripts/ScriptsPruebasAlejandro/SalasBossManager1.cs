using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SalasBossManager : MonoBehaviour
{

    public string teleportTargetTag = "TeleportTarget";
    public string currentRoomTag = "SalaActual";

    private List<GameObject> enemigosEnSala = new List<GameObject>();
    private bool jugadorEnSala = false;

    void Start()
    {

        foreach (Transform child in transform)
        {
            if (child.CompareTag("Enemigo"))
            {
                enemigosEnSala.Add(child.gameObject);
            }
        }
    }

    void DesactivarEnemigos()
    {
        if (enemigosEnSala.Count > 0)
        {
            Debug.Log("Enemigos desactivados");
            foreach (var enemigo in enemigosEnSala)
            {
                // Cambiar emurcielago por futuro script comun
                EnemigosComun comportamientoEnemigo = enemigo.GetComponent<EnemigosComun>();
                if (comportamientoEnemigo != null)
                {
                    //comportamientoEnemigo.enabled = false;
                    enemigo.GetComponent<SpriteRenderer>().enabled = false; // Desactivar el SpriteRenderer
                    comportamientoEnemigo.activo = false;
                }
            }
        }
        else
        {
            Debug.Log("No hay enemigos en la lista para desactivar.");
        }
    }

    void ActivarEnemigos()
    {
        Debug.Log("Enemigo activado");
        foreach (var enemigo in enemigosEnSala)
        {
            // Cambiar emurcielago por futuro script comun
            EnemigosComun comportamientoEnemigo = enemigo.GetComponent<EnemigosComun>();
            if (comportamientoEnemigo != null)
            {
                comportamientoEnemigo.enabled = true;
                enemigo.GetComponent<SpriteRenderer>().enabled = true; // Activar el SpriteRenderer
                comportamientoEnemigo.activo = true;
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
            DesactivarEnemigos();
        }

        if (other.CompareTag("Player"))
        {
            jugadorEnSala = true;
            DesactivarTeleportTargets();
            ActivarEnemigos();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemigo"))
        {
            Debug.Log("EnemigoEliminado");

            enemigosEnSala.Remove(other.gameObject);
            if (jugadorEnSala && enemigosEnSala.Count == 0)
            {
                Debug.Log("Has ganado");
                SceneManager.LoadScene("Win");
                DesactivarEnemigos();
            }
        }

        if (other.CompareTag("Player"))
        {
            jugadorEnSala = false;
            DesactivarEnemigos();
            ActivarTeleportTargets();
        }
    }
}
