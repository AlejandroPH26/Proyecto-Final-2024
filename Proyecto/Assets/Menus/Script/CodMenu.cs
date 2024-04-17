using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CodMenu : MonoBehaviour
{
    //PAGINA CREDITOS
    public GameObject MenuInicio;
    public GameObject MenuOpciones;
    public GameObject MenuCreditos;



    // Start is called before the first frame update
    void Start()
    {
        MenuInicio.SetActive(true);
        MenuOpciones.SetActive(false);
        MenuCreditos.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CambioEscena()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void SalirJuego()
    {

        Application.Quit();

    }
}
