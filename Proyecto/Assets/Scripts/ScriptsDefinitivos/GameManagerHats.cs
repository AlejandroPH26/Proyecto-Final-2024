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

    public int vidasMaximas = 6; // Cambio: Se añade una nueva variable para la vida máxima
    public int vidasActuales = 6; // Cambio: Se cambia el nombre de la variable vidas a vidasActuales

    public int bombas = 3;
    public TextMeshProUGUI ContadorBombas;
    public TextMeshProUGUI ContadorVidas;
    public bool Invulnerabilidad = false;
    public int DelayInvulnerabilidad = 3;
    public MusicManager mm;
    public AudioClip dañoJugador;

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
       mm = MusicManager.instance;
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
        vidasActuales++; // Cambio: Se ajusta el incremento de vidas actuales
        if (vidasActuales > vidasMaximas) // Cambio: Se compara con la nueva variable vidasMaximas
        {
            vidasActuales = vidasMaximas; // Cambio: Se ajusta el límite de vidas actuales
        }
        ContadorVidas.text = vidasActuales.ToString();
        UIvidas();

    }
    public void RestarVidas()
    {
        if (!Invulnerabilidad)
        {
            vidasActuales--; // Cambio: Se ajusta la disminución de vidas actuales
            mm.PlaySFX(dañoJugador);
            if (vidasActuales <= 0)
            {
                vidasActuales = 0;
            }
            ContadorVidas.text = vidasActuales.ToString();
            Invulnerabilidad = true;
            // Llama al método para cambiar el color del jugador tres veces durante el intervalo de DelayInvulnerabilidad
            StartCoroutine(CambiarColorJugadorDuranteDelay());
            Invoke("DesactivarInvulnerabilidad", DelayInvulnerabilidad);
            UIvidas();
        }
    }

    public void ReducirVidaMaxima()
    {
        vidasMaximas--; // Reducir la vida máxima en un punto
        if (vidasActuales > vidasMaximas)
        {
            vidasActuales = vidasMaximas; // Ajustar las vidas actuales si exceden la nueva máxima
            ContadorVidas.text = vidasActuales.ToString(); // Actualizar el texto de las vidas en el UI
        }
    }

    public void UIbombas()
    {
       int o = 0;

       foreach(GameObject bomba in spritesBombas)
       {
            if (bombas >= o) //esta activa
            {
                bomba.SetActive(true);
            }
            else
            {
                bomba.SetActive(false);
            }
            o++;
       }
    }

    public void UIvidas()
    {
        int a = 0;

        foreach (GameObject vida in spritesVidas)
        {
            if (vidasActuales >= a) //esta activa
            {
                vida.SetActive(true);
            }
            else
            {
                vida.SetActive(false);
            }
            a++;
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
