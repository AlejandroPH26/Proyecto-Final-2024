using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSombreros : MonoBehaviour
{
    public List<GameObject> sombrerosPrefab; // Lista de prefabs de sombreros que implementan ISombreros
    public Transform spawner1; // Referencia al primer spawner
    public Transform spawner2; // Referencia al segundo spawner

    private List<ISombreros> sombreros = new List<ISombreros>(); // Lista que contendrá los sombreros que implementan ISombreros

    void Start()
    {
        foreach (GameObject prefab in sombrerosPrefab)
        {
            ISombreros sombrero = prefab.GetComponent<ISombreros>(); // Obtener el componente ISombreros del prefab
            if (sombrero != null)
            {
                sombreros.Add(sombrero); // Si el componente ISombreros es válido, agregarlo a la lista
            }
            else
            {
                Debug.LogWarning("El prefab " + prefab.name + " no implementa ISombreros."); // Si no se encuentra el componente, emitir una advertencia
            }
        }

        if (sombreros.Count < 2)
        {
            Debug.LogError("No hay suficientes sombreros en la lista para spawnear."); // Asegurarse de que hay suficientes sombreros para spawnear
            return;
        }

        int index1 = Random.Range(0, sombreros.Count); // Elegir dos sombreros diferentes de la lista
        int index2;
        do
        {
            index2 = Random.Range(0, sombreros.Count);
        } while (index2 == index1);

        Instantiate(sombrerosPrefab[index1], spawner1.position, Quaternion.identity); // Spawnear los sombreros en los spawners
        Instantiate(sombrerosPrefab[index2], spawner2.position, Quaternion.identity);
    }
}
