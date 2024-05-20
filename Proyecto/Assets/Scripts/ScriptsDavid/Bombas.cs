using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombas : MonoBehaviour
{
    private GameManagerHats gm;
    public GameObject prefabExplosion;
    public bool destruyeBomba = false;
    private bool segundoFrame = false;
    public int damage = 40;
    public Animator bAnimator;
    public CircleCollider2D bombaCollider;
    public MusicManager mm;
    public AudioClip SonidoExplosion;
    // Start is called before the first frame update
    void Start()
    {
        bAnimator = gameObject.GetComponent<Animator>();
        bombaCollider = gameObject.GetComponent<CircleCollider2D>();
        gm = GameManagerHats.instance;
        mm = MusicManager.instance;
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
            Debug.Log("Explosion");
            bombaCollider.enabled = true;
            destruyeBomba = true;
            GameObject Explosion = Instantiate(prefabExplosion, transform.position, Quaternion.identity);
            mm.PlaySFX(SonidoExplosion);
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
            EnemigosComun enemigo = collision.gameObject.GetComponent<EnemigosComun>();
            enemigo.DañoRecibido(damage);
        }
        if (collision.tag == "Roca")
        {
            Destroy(collision.gameObject);
        }
    }
}
