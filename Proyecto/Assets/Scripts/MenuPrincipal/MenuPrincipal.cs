using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{

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
    }

    public void EmpezarNivel()
    {

        SceneManager.LoadScene("AnimacionInicial");

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
