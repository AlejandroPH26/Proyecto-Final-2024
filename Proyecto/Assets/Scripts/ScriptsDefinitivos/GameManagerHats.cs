using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerHats : MonoBehaviour
{
    static public GameManagerHats instance;     // Se crea la instancia como variable
    public int vidas = 6;
    public int bombas = 3;
    public TextMeshProUGUI ContadorBombas;
    public TextMeshProUGUI ContadorVidas;
    public bool Invulnerabilidad = false;
    public int DelayInvulnerabilidad = 3;

    //VIDAS
    public GameObject[] spritesVidas;

    //BOMBAS
    public GameObject[] spritesBombas;

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
        UIvidas();
        UIbombas();
    }
   
    public void SumarBombas()
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

    public void SumarVidas()
    {
        vidas++;
        if (vidas > 6)
        {
            vidas = 6;
        }
            ContadorVidas.text = vidas.ToString();
    }
    public void RestarVidas()
    {
        if (Invulnerabilidad == false)
        {
            vidas--;
            if (vidas <= 0)
            {
                vidas = 0;
            }
            ContadorVidas.text = vidas.ToString();
            Invulnerabilidad = true;
            Invoke("Invulnerable", DelayInvulnerabilidad);
        }
    }

    public void UIbombas()
    {
       int i = 0;

       foreach(GameObject bomba in spritesBombas)
       {
            if (bombas >= i) //est� activa
            {
                bomba.SetActive(true);
            }
            else
            {

                bomba.SetActive(false);

            }

            i++;
       }

    }

    public void UIvidas()
    {
        int i = 0;

        foreach (GameObject vida in spritesVidas)
        {
            if (vidas >= i) //est� activa
            {
                vida.SetActive(true);
            }
            else
            {

                vida.SetActive(false);

            }

            i++;
        }

    }

    public void Invulnerable()
    {
        Invulnerabilidad = true;
    }
}
