using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PonerBombas : MonoBehaviour
{
    private GameManagerHats gm;
    public KeyCode dropBomb = KeyCode.Space;
    public GameObject prefabBomba;
    public bool colocarBomba = true;
    public float delay = 1f;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManagerHats.instance;
    }

    // Update is called once per frame
    void Update()
    {
        PonerBomba();
    }
    public void PonerBomba()
    {
        if (Input.GetKeyDown(dropBomb) && colocarBomba == true && gm.bombas > 0)
        {
            GameObject Bomba = Instantiate(prefabBomba, transform.position, Quaternion.identity);
            gm.RestarBombas();
            colocarBomba = false;
            Invoke("SePuedePonerBomba", delay);
        }
    }

    public void SePuedePonerBomba()
    {
        colocarBomba = true;
    }
}
