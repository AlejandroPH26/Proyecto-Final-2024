using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerHats : MonoBehaviour
{
    static public GameManagerHats instance;     // Se crea la instancia como variable
    static public JugadorV1 jugador;
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
       jugador = FindObjectOfType<JugadorV1>();
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
        UIvidas();

    }
    public void RestarVidas()
    {
        if (!Invulnerabilidad)
        {
            vidas--;
            if (vidas <= 0)
            {
                vidas = 0;
            }
            ContadorVidas.text = vidas.ToString();
            Invulnerabilidad = true;
            // Llama al método para cambiar el color del jugador tres veces durante el intervalo de DelayInvulnerabilidad
            StartCoroutine(CambiarColorJugadorDuranteDelay());
            Invoke("DesactivarInvulnerabilidad", DelayInvulnerabilidad);
            UIvidas();
        }
    }

    public void UIbombas()
    {
       int i = 0;

       foreach(GameObject bomba in spritesBombas)
       {
            if (bombas >= i) //esta activa
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
            if (vidas >= i) //esta activa
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

    public void DesactivarInvulnerabilidad()
    {
        Invulnerabilidad = false;
        Debug.Log("Ya no es invulnerable 1");
    }

    // Cambia el color del jugador entre blanco y rojo tres veces durante el intervalo de DelayInvulnerabilidad
    IEnumerator CambiarColorJugadorDuranteDelay()
    {
        Color rojo = new Color(1f, 0.675f, 0.675f); // Define el color rojo en formato RGB
        for (int i = 0; i < 3; i++) // Repite el proceso tres veces
        {
            jugador.CambiarColor(rojo); // Cambia el color del jugador y sus hijos a rojo
            yield return new WaitForSeconds(DelayInvulnerabilidad / 6f); // Espera un tiempo determinado
            jugador.CambiarColor(Color.white); // Cambia el color del jugador y sus hijos a blanco
            yield return new WaitForSeconds(DelayInvulnerabilidad / 6f); // Espera otro tiempo determinado
        }
        jugador.CambiarColor(Color.white); // Asegura que el color final sea blanco
    }
}
