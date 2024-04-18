using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    //BRILLO
    public Slider sliderBrillo;
    public float valorBrillo;
    public Image panelBrillo;

    //VOLUMEN
    public Slider sliderVolumen;
    public float sliderValor;
    public Image imagenMuteo;

    //PAGINA CREDITOS
    public GameObject MenuInicio;
    public GameObject MenuOpciones;
    public GameObject MenuCreditos;

    // Start is called before the first frame update
    void Start()
    {
        //paginas
        MenuInicio.SetActive(true);
        MenuOpciones.SetActive(false);
        MenuCreditos.SetActive(false);

        //brillo
        sliderBrillo.value = PlayerPrefs.GetFloat("brillo", 0f);

        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, sliderBrillo.value);


        //volumen
        sliderVolumen.value = PlayerPrefs.GetFloat("volumenAudio", 50f); //ponemos el audio del slider a X voluem predeterminado
        AudioListener.volume = sliderVolumen.value;// hacemos que el volumen del AudioListener tenga el volumen predefinidio del slider
        RevisarMuteo(); //revisamos si estamos sin audio para activar un icono
    }

    public void CambiarVoluem(float valor)
    {
        sliderValor = valor;
        PlayerPrefs.SetFloat("VolumenAudio", sliderValor);
        AudioListener.volume = sliderVolumen.value;
        RevisarMuteo();
    }

    public void CambiarBrillo(float valorB)
    {

        valorBrillo = valorB;
        PlayerPrefs.SetFloat("brillo", valorBrillo);
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, valorBrillo);

    }

    public void RevisarMuteo()
    {
        if(sliderValor == 0)
        {
            imagenMuteo.enabled = true; //si el volumen es 0 significa que no hay audio y aparece un icono

        }
        else
        {
            imagenMuteo.enabled = false; //si el volumen no es 0 significa que hay audio y no aparece un icono

        }

    }

    public void EmpezarNivel()
    {

        SceneManager.LoadScene("SampleScene");

    }

    public void SalirJuego()
    {

        Application.Quit();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
