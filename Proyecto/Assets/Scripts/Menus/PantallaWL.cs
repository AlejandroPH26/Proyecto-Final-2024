using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PantallaWL : MonoBehaviour
{
    public MusicManager mm;
    public AudioClip Victoria;

    // Start is called before the first frame update
    void Start()
    {
        mm = MusicManager.instance;
        mm.PlaySFX(Victoria); 
    }

    // Update is called once per frame
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
        mm.StopMusic();
    }

    public void ResetLvl()
    {
        SceneManager.LoadScene("Nivel 1");
        Debug.Log("reiniciando");

    }
}
