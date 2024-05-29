using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PantallaWL : MonoBehaviour
{
    public MusicManager mm;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per 3rame
    void Update()
    {
    }

    public void SALIR()
    {
        Application.Quit();
        Debug.Log("salir");
    }

    public void MenuPrincipal()
    {
        
        SceneManager.LoadScene("MenúPrincipal");
       // mm.PlaySFX(MenuMusica);
    }

    public void ResetLvl()
    {
        SceneManager.LoadScene("Nivel 1");
        Debug.Log("reiniciando");

    }
}
