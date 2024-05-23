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
    // public TextMeshProUGUI ContadorBombas;
    // public TextMeshProUGUI ContadorVidas;
    public bool Invulnerabilidad = false;
    public int DelayInvulnerabilidad = 3;
    public MusicManager mm;
    public AudioClip dañoJugador;
    public AudioClip RecolectableBomba;
    public AudioClip RecolectableVida;
    public AudioClip muertePersonaje;
    public AudioClip dañoPersonaje;

    //VIDAS
    public GameObject[] spritesVidas;

    //BOMBAS
    public GameObject[] spritesBombas;
    public int MAX_BOMBAS = 3;

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
       UIbombas();
       UIvidas();
    }

    // Update is called once per frame
    void Update()
    {
        // UIvidas();
        // UIbombas();
    }

    public void SumarBombas()
    {
        mm.PlaySFX(RecolectableBomba);
        bombas++;
        if (bombas > MAX_BOMBAS)
        {
            bombas = MAX_BOMBAS;
        }
        UIbombas();
    }

    public void RestarBombas()
    {
        bombas--;
        if (bombas < 0)
        {
            bombas = 0;
        }
        UIbombas();
    }

    public void SumarVidas()
    {
        vidasActuales++; // Cambio: Se ajusta el incremento de vidas actuales
        mm.PlaySFX(RecolectableVida);
        jugador.ActivarParticulasRecuperacion(); // Método para llamar a las partículas de vida
        if (vidasActuales > vidasMaximas) // Cambio: Se compara con la nueva variable vidasMaximas
        {
            vidasActuales = vidasMaximas; // Cambio: Se ajusta el límite de vidas actuales
        }
        // ContadorVidas.text = vidasActuales.ToString();
        UIvidas();

    }
    public void RestarVidas()
    {
        if (!Invulnerabilidad)
        {
            vidasActuales--;
            mm.PlaySFX(dañoJugador);
            if (vidasActuales <= 0)
            {
                vidasActuales = 0;
                mm.PlaySFX(muertePersonaje);
            }
            UIvidas();
            Invulnerabilidad = true;
            StartCoroutine(CambiarColorJugadorDuranteDelay());
            Invoke("DesactivarInvulnerabilidad", DelayInvulnerabilidad);
        }
    }

    public void ReducirVidaMaxima()
    {
        vidasMaximas--; // Reducir la vida máxima en un punto
        if (vidasActuales > vidasMaximas)
        {
            vidasActuales = vidasMaximas; // Ajustar las vidas actuales si exceden la nueva máxima
            // ContadorVidas.text = vidasActuales.ToString(); // Actualizar el texto de las vidas en el UI
            UIvidas();
        }
    }

    public void UIbombas()
    {
        int o = 0;

        foreach (GameObject bomba in spritesBombas)
        {
            if (bombas > o) // esta activa
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
            if (vidasActuales > a) // esta activa
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
        DesactivarInvulnerabilidad(); // Desactiva la invulnerabilidad al final del cambio de color
    }

}
