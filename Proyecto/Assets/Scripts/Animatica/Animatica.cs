using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Animatica : MonoBehaviour
{

      public GameObject pag1;
      public GameObject pag2;
      public GameObject pag3;
      private int number = 1;

// Start is called before the first frame update
    void Start()
    {
        pag1.SetActive(true);
        pag2.SetActive(false);
        pag3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        cambiarPag();
        SkipAnimatica();
    }

    public void cambiarPag()
    {

        if (Input.GetKeyDown(KeyCode.Space) && number == 1)
        {

            pag1.SetActive(false);
            pag2.SetActive(true);
            pag3.SetActive(false);
            number = number + 1;


        }
        else if (Input.GetKeyDown(KeyCode.Space) && number == 2)
        {
            pag1.SetActive(false);
            pag2.SetActive(false);
            pag3.SetActive(true);
            number = number + 1;

        }
        else if (Input.GetKeyDown(KeyCode.Space) && number == 3)
        {
            
            SceneManager.LoadScene("Tutorial");

        }

    }

    public void SkipAnimatica()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {

            SceneManager.LoadScene("Tutorial");

        }



    }
}
