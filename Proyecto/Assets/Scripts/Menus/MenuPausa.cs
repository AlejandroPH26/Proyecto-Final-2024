using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    //PANTALLA PAUSA

    public GameObject mPausa;
    public bool Pausa = false;
    public GameObject opciones;


    // Start is called before the first frame update
    void Start()
    {
        mPausa.SetActive(false);
        opciones.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        MenuuPausa();
    }

    public void MenuuPausa()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
           if(Pausa == false)
           {
                mPausa.SetActive(true);
                Pausa = true;

                Time.timeScale = 0f;
            }

        }
    }

    public void ReanudarJuego()
    {
        mPausa.SetActive(false);
        Pausa = false;

        Time.timeScale = 1;


    }
    

}
