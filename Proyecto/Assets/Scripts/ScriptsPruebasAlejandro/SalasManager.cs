using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalasManager : MonoBehaviour
{

    public string teleportTargetTag = "TeleportTarget";
    public string currentRoomTag = "SalaActual";

    public List<GameObject> puertas;
    private List<GameObject> enemigosEnSala = new List<GameObject>();
    private bool jugadorEnSala = false;

    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Door"))
            {
                puertas.Add(child.gameObject);
            }
            else if (child.CompareTag("Enemigo"))
            {
                enemigosEnSala.Add(child.gameObject);
            }
        }
        DesactivarCollidersPuertas();
    }

    void SetCollidersPuertas(bool estado)
    {
        foreach (var puerta in puertas)
        {
            Collider2D collider = puerta.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = estado;
            }
        }
    }

    private void DesactivarCollidersPuertas()
    {
        SetCollidersPuertas(false);
    }

    private void ActivarCollidersPuertas()
    {
        if (enemigosEnSala.Count == 0)
        {
            SetCollidersPuertas(true);
        }
    }

    void DesactivarEnemigos()
    {
        if (enemigosEnSala.Count > 0)
        {
            Debug.Log("Enemigos desactivados");
            foreach (var enemigo in enemigosEnSala)
            {
                EMurcielagoPrueba comportamientoEnemigo = enemigo.GetComponent<EMurcielagoPrueba>();
                if (comportamientoEnemigo != null)
                {
                    comportamientoEnemigo.enabled = false;
                    CongelarEnemigos(enemigo);
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
            EMurcielagoPrueba comportamientoEnemigo = enemigo.GetComponent<EMurcielagoPrueba>();
            if (comportamientoEnemigo != null)
            {
                comportamientoEnemigo.enabled = true;
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
            DesactivarCollidersPuertas();
        }

        if (other.CompareTag("Player"))
        {
            jugadorEnSala = true;
            if (enemigosEnSala.Count > 0)
            {
                ActivarEnemigos();
            }
            else
            {
                ActivarCollidersPuertas();
            }
            DesactivarTeleportTargets();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemigo"))
        {
            enemigosEnSala.Remove(other.gameObject);
            if (jugadorEnSala && enemigosEnSala.Count == 0)
            {
                DesactivarEnemigos();
                ActivarCollidersPuertas();
            }
        }

        if (other.CompareTag("Player"))
        {
            jugadorEnSala = false;
            DesactivarEnemigos();
            ActivarTeleportTargets();
        }
    }

    void CongelarEnemigos(GameObject enemigo)
    {
        EnemigoV1 comportamientoEnemigo = enemigo.GetComponent<EnemigoV1>();
        if (comportamientoEnemigo != null)
        {
            comportamientoEnemigo.CongelarEnemigo();
        }
    }


}
