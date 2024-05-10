using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControles1 : MonoBehaviour
{
    public GameObject pag1, pag2;
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
        CambiarPag();
        SaltarPag();
    }

    public void CambiarPag()
    {
        if(Input.GetKeyDown(KeyCode.Space) && number == 1)
        {

            pag1.SetActive(false);
            pag2.SetActive(true);
            number = number + 1;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && number == 2)
        {
            pag1.SetActive(false);
            pag2.SetActive(false);

        }
    }

    public void SaltarPag()
    {
        
        if(Input.GetKeyDown(KeyCode.Return))
        {

            SceneManager.LoadScene("Nivel 1");

        }


    }
}
