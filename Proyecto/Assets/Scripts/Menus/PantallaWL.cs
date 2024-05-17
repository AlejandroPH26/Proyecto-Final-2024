using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaWL : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SALIR();
        MenuPrincipal();
        ResetLvl();
    }

    public void SALIR()
    {
        Application.Quit();
    }

    public void MenuPrincipal()
    {

        SceneManager.LoadScene("MenúPrincipal");

    }

    public void ResetLvl()
    {
        SceneManager.LoadScene("Nivel1");

    }
}
