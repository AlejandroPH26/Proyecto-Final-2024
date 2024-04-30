using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerHats : MonoBehaviour
{
    static public GameManagerHats instance;     // Se crea la instancia como variable
    public int vidas = 6;
    public int bombas = 3;
    public TextMeshProUGUI ContadorBombas;
    public TextMeshProUGUI ContadorVidas;

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
            SumarVidas();
        }
        if (collision.tag == "Bomba")
        {
            Destroy(collision.gameObject);
            SumarBombas();
        }
    }
    private void SumarBombas()
    {
        bombas++;
        if (bombas > 10)
        {
            bombas = 10;
        }
            ContadorBombas.text = bombas.ToString();
    }

    public void RestarBombas()
    {
        bombas--;
        if (bombas <= 0)
        {
            bombas = 0;
        }
        ContadorBombas.text = bombas.ToString();
    }

    private void SumarVidas()
    {
        vidas++;
        if (vidas > 6)
        {
            vidas = 6;
        }
            ContadorVidas.text = vidas.ToString();
    }
}
