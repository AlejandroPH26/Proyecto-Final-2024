using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BalaFase4 : MonoBehaviour
{
    public float speed = 10f; // Velocidad de la bala
    public float lifeTime = 5f; // Tiempo de vida de la bala antes de ser destruida
    public int damage = 10; // Daño que inflige la bala

    private Rigidbody2D rb;

    private GameManagerHats gm;

    void Start()
    {
        gm = FindObjectOfType<GameManagerHats>();
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime); // Destruye la bala después de 'lifeTime' segundos

        // Dispara la bala en cuatro direcciones diferentes
        ShootInFourDirections();
    }

    void ShootInFourDirections()
    {
        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

        foreach (var direction in directions)
        {
            GameObject bullet = Instantiate(gameObject, transform.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = direction * speed;
        }

        // Destruir la bala original para que sólo queden las copias disparadas en cuatro direcciones
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Destroy(this.gameObject);


            gm.RestarVidas();


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
