using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerHats : MonoBehaviour
{
    public int vidas = 6;
    public int bombas = 3;
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
        if (collision.tag == "Botiquin")
        {
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Bomba")
        {
            Destroy(collision.gameObject);
        }
    }
}
