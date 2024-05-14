using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;

public class BalaJugador : MonoBehaviour
{
    public Direction dirBala;
    public int damage = 20;
    public float speed = 3f;
    private Rigidbody2D rb;
    private Vector2[] direcciones = {new Vector2(0,1), new Vector2(0,-1), new Vector2(-1,0), new Vector2(1,0)};
    public EnemigosComun eComun;

    // Start is called before the first frame update
    void Awake()
    { 
        rb = GetComponent<Rigidbody2D>();
        eComun = FindObjectOfType<EnemigosComun>();
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
        // Modificar rotacion bala
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            
            eComun.DañoRecibido(damage);
            Destroy(this.gameObject); // Destruye la bala            
        }
    }

}
