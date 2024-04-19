using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalasManager : MonoBehaviour
{

    public string teleportTargetTag = "TeleportTarget";     // Tag de los objetos de teletransporte
    public string currentRoomTag = "SalaActual";            // Tag del objeto vacío que representa la sala actual

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if (other.CompareTag(currentRoomTag))
        {
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

}
