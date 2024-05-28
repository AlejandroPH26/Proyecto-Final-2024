using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefeFinal : MonoBehaviour
{
    public int vidaTotal = 100;
    private int vidaActual;
    private Animator animator;
    private Transform jugador;
    private bool esAtacando = false;

    public Transform tpPosicion;
    public Transform[] spawnEsferas;
    public GameObject esferaPrefab;
    public GameObject balaPrefab;
    public Transform posDis;

    private List<GameObject> esferas = new List<GameObject>();

    void Start()
    {
        vidaActual = vidaTotal;
        animator = GetComponent<Animator>();
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(FaseIdle());
    }

    IEnumerator FaseIdle()
    {
        //animator.Play("idle");
        yield return new WaitForSeconds(2);
        StartCoroutine(Fase1());
    }

    IEnumerator Fase1()
    {
        while (vidaActual > 75)
        {
            //animator.Play("caminar");
            PerseguirJugador();
            yield return null;
        }
        //animator.Play("dormir");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        StartCoroutine(Fase2());
    }

    IEnumerator Fase2()
    {
        while (vidaActual > 50)
        {
            //animator.Play("girar");
            PerseguirJugador();
            if (!esAtacando)
            {
                esAtacando = true;
                StartCoroutine(DispararProyectiles());
            }
            yield return null;
        }
        esAtacando = false;
        StartCoroutine(Fase3());
    }

    IEnumerator Fase3()
    {
        // Desaparecer
        gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        // Reaparecer
        transform.position = tpPosicion.position;
        gameObject.SetActive(true);
        // Invocar esferas
        foreach (Transform spawn in spawnEsferas)
        {
            GameObject esfera = Instantiate(esferaPrefab, spawn.position, spawn.rotation);
            esferas.Add(esfera);
        }
        while (vidaActual > 25)
        {
            yield return null;
        }
        // Destruir esferas al terminar la fase
        foreach (GameObject esfera in esferas)
        {
            Destroy(esfera);
        }
        esferas.Clear();
        StartCoroutine(Fase4());
    }

    IEnumerator Fase4()
    {
        while (vidaActual > 0)
        {
            animator.Play("girar");
            yield return new WaitForSeconds(2);
            DispararBalasContinuas();
            yield return new WaitForSeconds(3);
            DispararBalaHaciaJugador();
            yield return new WaitForSeconds(0.5f);
        }
        Morir();
    }

    void PerseguirJugador()
    {
        transform.position = Vector2.MoveTowards(transform.position, jugador.position, 2 * Time.deltaTime);
    }

    IEnumerator DispararProyectiles()
    {
        while (vidaActual > 50)
        {
            Instantiate(balaPrefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(0.3f);
        }
    }

    void DispararBalasContinuas()
    {
        // Implementar disparo de balas continuas en diagonal
    }

    void DispararBalaHaciaJugador()
    {
        Vector3 direccion = (jugador.position - transform.position).normalized;
        Instantiate(balaPrefab, transform.position, Quaternion.LookRotation(direccion));
    }

    void Morir()
    {
        // Implementar lógica de muerte
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("BalaJugador"))
        {
            vidaActual -= 10; // o la cantidad de daño que cause la bala
            if (vidaActual <= 0)
            {
                vidaActual = 0;
            }

            Destroy(collision.gameObject);
        }
    }
}
