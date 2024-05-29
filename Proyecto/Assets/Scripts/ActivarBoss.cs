using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarBoss : MonoBehaviour
{
    private BossFinal jefe;
    public bool PuedeMoverse = false;
   
    // Start is called before the first frame update
    void Start()
    {
        jefe = FindObjectOfType<BossFinal>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {         
            PuedeMoverse=true;
        }
    }
}
