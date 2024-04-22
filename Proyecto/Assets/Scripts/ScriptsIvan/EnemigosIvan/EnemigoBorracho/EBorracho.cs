using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBorracho : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    public GameObject Player;

    public float RangoMin = 8;
    private float timer;
    private float TiempoEntreDisparos = 1.5f;

   

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

    }
    // Update is called once per frame
    void Update()
    {

        Rango();
    }

    void shoot()
    {
       Instantiate(bullet,bulletPos.position, Quaternion.identity);
    }
    
    void Rango()
    {
        float distancia = Vector2.Distance(transform.position, Player.transform.position);
        Debug.Log(distancia);

        if (distancia < RangoMin)
        {
            timer += Time.deltaTime;

            if (timer > TiempoEntreDisparos)
            {
                timer = 0;
                shoot();
            }
        }
    }
  
    
}
