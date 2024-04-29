using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerHats : MonoBehaviour
{
    static public GameManagerHats instance;     // Se crea la instancia como variable
    public int vidas = 6;
    public int bombas = 3;
    public TextMeshProUGUI contadorBombas;

    private void Awake()
    {
        if (instance == null)   //Se el valor es nulo
        {
            instance = this;    //Se le da un valor a esa variable
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    //GameManagerHats.instance.Losquesea para el singleton

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
            vidas++;
            if (vidas > 6)
            {
                vidas = 6;
            }
        }
        if (collision.tag == "Bomba")
        {
            Destroy(collision.gameObject);
            SumarBombas();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Botiquin")
        {
            //   Destroy(collision.gameObject);
        }
    }
    private void SumarBombas()
    {
        bombas++;
        if (bombas > 10)
        {
            bombas = 10;
            contadorBombas.text = bombas.ToString();
        }
    }
    private void SumarVidas()
    {
        vidas++;
        if (vidas > 6)
        {
            vidas = 6;
            contadorBombas.text = bombas.ToString();
        }
    }
}
