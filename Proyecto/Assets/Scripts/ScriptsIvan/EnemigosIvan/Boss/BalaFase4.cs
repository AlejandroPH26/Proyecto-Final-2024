using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BalaFase4 : MonoBehaviour
{
    public float velocidad = 7f;
    public int daño = 10;
    public Vector2 direccion;
    private Rigidbody2D rb;

    private GameManagerHats gm;
    public JugadorV1 Player;

    void Start()
    {
        // Obtener el componente Rigidbody2D de la bala
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Player = FindObjectOfType<JugadorV1>();
        gm = FindObjectOfType<GameManagerHats>();

        // Obtener la dirección de movimiento basada en la posición de spawn de la bala
        Vector3 direction = Player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * velocidad;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 180);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            gm.RestarVidas();
            Destroy(this.gameObject);
        }

        else if (collision.collider.CompareTag("Pared") || collision.collider.CompareTag("Boss"))
        {
            Destroy(this.gameObject);
        }

        else if (collision.collider.CompareTag("BalaJugador"))
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
