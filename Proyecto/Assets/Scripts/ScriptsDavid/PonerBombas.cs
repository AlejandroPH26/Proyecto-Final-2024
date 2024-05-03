using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PonerBombas : MonoBehaviour
{
    private GameManagerHats gm;
    public KeyCode dropBomb = KeyCode.Space;
    public GameObject prefabBomba;
    public Animator bAnimator;
    public bool colocarBomba = true;
    public float delay = 10f;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManagerHats.instance;
        bAnimator = gameObject.GetComponent<Animator>();
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
            bAnimator.Play("BombaActivacion");
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
