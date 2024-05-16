using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaPaja : MonoBehaviour
{
    public Direction dirBala;
    public float speed = 3f;
    private Rigidbody2D rb;
    private Vector2[] direcciones = { new Vector2(0, 1), new Vector2(0, -1), new Vector2(-1, 0), new Vector2(1, 0) };
    public int rebotesBala = 3;

    public int damage = 20;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void asignarDireccion(Direction dir)
    {
        dirBala = dir;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        rebotesBala--;
        if(rebotesBala <= 0)
        {
            Destroy(gameObject);
            return;
        }

        if (collision.gameObject.CompareTag("Enemigo"))
        {
            EnemigosComun enemigo = collision.gameObject.GetComponent<EnemigosComun>();
            if (enemigo != null)
            {
                enemigo.DañoRecibido(damage);
            }
            Destroy(this.gameObject); // Destruye la bala            
        }
    }
}
