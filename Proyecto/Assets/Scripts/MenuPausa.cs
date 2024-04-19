using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    //PANTALLA PAUSA

    public GameObject mPausa;

    // Start is called before the first frame update
    void Start()
    {
        mPausa.SetActive(false);
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
            
            mPausa.SetActive(true);

        }


    }
}
