using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalasManager : MonoBehaviour
{

    public string teleportTargetTag = "TeleportTarget";     // Tag de los objetos de teletransporte
    public string currentRoomTag = "SalaActual";            // Tag del objeto vacío que representa la sala actual

    public List<GameObject> puertas;
    [SerializeField] private List<GameObject> enemigosEnSala = new List<GameObject>(); // Lista para almacenar enemigos en la sala

    public bool jugadorEnSala = false; // Variable para rastrear si el jugador está dentro de la sala

    // Start is called before the first frame update
    void Start()
    {

        foreach (Transform child in transform)
        {
            if (child.CompareTag("Door"))
            {
                puertas.Add(child.gameObject);
            }
            if (child.CompareTag("Enemigo"))
            {
                enemigosEnSala.Add(child.gameObject);
            }
        }
        DesactivarEnemigos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Método para desactivar o activar los colliders de las puertas
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

    // Método para desactivar los colliders de las puertas
    private void DesactivarCollidersPuertas()
    {
        SetCollidersPuertas(false);
    }

    // Método para activar los colliders de las puertas
    private void ActivarCollidersPuertas()
    {
        if (enemigosEnSala.Count == 0)
        {
            SetCollidersPuertas(true);
        }
    }

    // Método para desactivar los GameObjects de los enemigos en la sala
    void DesactivarEnemigos()
    {
        Debug.Log("Cantidad de enemigos en la lista: " + enemigosEnSala.Count);

        // Verificar si hay enemigos en la lista
        if (enemigosEnSala.Count > 0)
        {
            Debug.Log("Enemigo desactivado");
            for (int i = 0; i < enemigosEnSala.Count; i++)
            {
                GameObject enemigo = enemigosEnSala[i];
                enemigo.SetActive(false);
                Debug.Log("El enemigo se ha desactivado");
            }
            enemigosEnSala.Clear();
        }
        else
        {
            Debug.Log("No hay enemigos en la lista para desactivar.");
        }
    }

    // Método para activar los GameObjects de los enemigos en la sala
    void ActivarEnemigos()
    {
        Debug.Log("Enemigo activado");
        for (int i = 0; i < enemigosEnSala.Count; i++)
        {
            GameObject enemigo = enemigosEnSala[i];
            enemigo.SetActive(true);
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
            // DesactivarEnemigos();
            DesactivarCollidersPuertas();
        }

        if (other.CompareTag("Player"))
        {
            jugadorEnSala = true;
            // Activa los enemigos solo si el jugador está en la sala

            ActivarEnemigos();
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


}
