using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaSheriff : MonoBehaviour
{
    public Direction dirBala;
    public float speed = 3f;
    private Rigidbody2D rb;
    private Vector2[] direcciones = { new Vector2(0, 1), new Vector2(0, -1), new Vector2(-1, 0), new Vector2(1, 0) };

    public int damage = 20;

    // Referencia al prefab del sprite de destrucción
    public GameObject spriteDestruccionPrefab;
    public float tiempoDestruccionSprite = 0.1f;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void asignarDireccion(Direction dir)
    {
        // Asignar dirBala     
        dirBala = dir;
        // Modificar rb.velocity
        // direcciones para usar la lista, el corchete [] es para seleccionar la posición en la lista,
        // que dirBala es int y no otro tipo de dato (int),
        // mutiplicarlo por speed para indicarle la velocidad
        rb.velocity = direcciones[(int)dirBala] * speed;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            EnemigosComun enemigo = collision.gameObject.GetComponent<EnemigosComun>();
            if (enemigo != null)
            {
                enemigo.DañoRecibido(damage);
            }
            InstanciarSpriteDestruccion();
            Destroy(this.gameObject); // Destruye la bala            
        }

        if (collision.gameObject.CompareTag("ParedBala") || collision.gameObject.CompareTag("Roca"))
        {
            InstanciarSpriteDestruccion();
            Destroy(this.gameObject); // Destruye la bala            
        }
    }

    private void InstanciarSpriteDestruccion()
    {
        if (spriteDestruccionPrefab != null)
        {
            GameObject spriteDestruccion = Instantiate(spriteDestruccionPrefab, transform.position, transform.rotation);
            Destroy(spriteDestruccion, tiempoDestruccionSprite);
        }
    }
}
