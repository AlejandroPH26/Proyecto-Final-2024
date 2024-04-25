using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControles : MonoBehaviour
{
    public GameObject pag1;
    public GameObject pag2;
    private int number = 1;

    // Start is called before the first frame update
    void Start()
    {
        pag1.SetActive(true);
        pag2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        cambiarPag();
    }

    public void cambiarPag()
    {
        
        if (Input.GetKeyDown(KeyCode.Return) && (number % 2) == 1)
        {

            pag1.SetActive(false);
            pag2.SetActive(true);
            number = number + 1;


        }
        else if (Input.GetKeyDown(KeyCode.Return) && (number % 2) == 0)
        {
            pag1.SetActive(false);
            pag2.SetActive(false);
            

        }



    }
}
