using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SalasManager : MonoBehaviour
{

    public string teleportTargetTag = "TeleportTarget";
    public string currentRoomTag = "SalaActual";
    public GameObject bombaPrefab;
    public GameObject botiquinPrefab;

    public List<Comportamiento_PuertasV1> puertas;// Lista de comportamientos de puertas

    private List<GameObject> enemigosEnSala = new List<GameObject>();
    private bool jugadorEnSala = false;

    void Start()
    {
        puertas = GetComponentsInChildren<Comportamiento_PuertasV1>().ToList();

        foreach (Transform child in transform)
        {
            if (child.CompareTag("Enemigo"))
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
                // Cambiar emurcielago por futuro script comun
                EnemigosComun comportamientoEnemigo = enemigo.GetComponent<EnemigosComun>();
                if (comportamientoEnemigo != null)
                {
                    comportamientoEnemigo.enabled = false;
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
            DesactivarCollidersPuertas();
        }

        if (other.CompareTag("Player"))
        {
            jugadorEnSala = true;
            if (enemigosEnSala.Count > 0 && jugadorEnSala)
            {
                ActivarEnemigos();
                // Si hay al menos un enemigo en la sala cuando el jugador entra, cerrar las puertas
                foreach (var puerta in puertas)
                {
                    puerta.CerrandoPuertas();
                    Debug.Log("Se cierran las puertas");
                }
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

                foreach (var puerta in puertas)
                {
                    puerta.AbriendoPuertas();
                    Debug.Log("Se abren las puertas");
                }

                // Generar un número aleatorio entre 0 y 1
                float randomValue = Random.Range(0f, 1f);

                // Si el número aleatorio es mayor que 0.5, se instancia un objeto
                if (randomValue > 0.5f)
                {
                    Debug.Log("Na de na");                                  
                }

                else
                {
                    // Generar otro número aleatorio para determinar si se instancia una bomba o un botiquín
                    float itemTypeChance = Random.Range(0f, 1f);

                    if (itemTypeChance > 0.5f)
                    {
                        // Instanciar una bomba
                        Instantiate(bombaPrefab, transform.position, Quaternion.identity);
                    }
                    else
                    {
                        // Instanciar un botiquín
                        Instantiate(botiquinPrefab, transform.position, Quaternion.identity);
                    }
                }
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
