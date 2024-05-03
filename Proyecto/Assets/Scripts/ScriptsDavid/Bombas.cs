using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombas : MonoBehaviour
{
    private GameManagerHats gm;
    public bool destruyeBomba = false;
    private bool segundoFrame = false;
    public Animator bAnimator;
    public CircleCollider2D bombaCollider;
    // Start is called before the first frame update
    void Start()
    {
        bAnimator = gameObject.GetComponent<Animator>();
        bombaCollider = gameObject.GetComponent<CircleCollider2D>();
        gm = GameManagerHats.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(destruyeBomba == true)
        {   if (segundoFrame == true)
            { Destroy(gameObject); }
            segundoFrame = true;
        }
    }
    private void Explosion()
    {
            bombaCollider.enabled = true;
            destruyeBomba = true;
            // Particulas
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Colision con bomba");
        if (collision.tag == "Player")
        {
            gm.RestarVidas();
        }
        if (collision.tag == "Enemigo")
        {
            // Para hacer, quitar vida al enemigo
        }
    }
}
