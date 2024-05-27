using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EBoss : MonoBehaviour

{
    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject prefab3;

    public Transform[] wave1SpawnPositions;
    public Transform[] wave2SpawnPositions;
    public Transform[] wave3SpawnPositions;
    public Transform[] wave4SpawnPositions;

    private SpriteRenderer spriteRenderer;
    private int enemiesToDefeat = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(StartBattle());
    }

    IEnumerator StartBattle()
    {
        // Desaparecer el sprite del jefe al inicio del combate
        spriteRenderer.enabled = false;

        // Invocar la primera oleada de enemigos
        yield return InvokeWave(new GameObject[] { prefab1 }, 4, wave1SpawnPositions);

        // Invocar la segunda oleada con 2 prefab2 y 2 prefab1
        yield return InvokeWave(new GameObject[] { prefab2, prefab1 }, 2, wave2SpawnPositions);

        // Invocar la tercera oleada con solo prefab3
        yield return InvokeWave(new GameObject[] { prefab3 }, 4, wave3SpawnPositions);

        // Invocar la cuarta oleada con 2 prefab1, 2 prefab2, y 2 prefab3
        yield return InvokeWave(new GameObject[] { prefab1, prefab2, prefab3 }, 2, wave4SpawnPositions);

        // Esperar 1 segundo antes de reaparecer el sprite del jefe
        yield return new WaitForSeconds(1f);

        // Reaparecer el sprite del jefe al finalizar todas las oleadas
        spriteRenderer.enabled = true;
        Debug.Log("All waves completed, boss appears!");
    }

    IEnumerator InvokeWave(GameObject[] enemyPrefabs, int enemyCountPerPrefab, Transform[] spawnPositions)
    {
        Debug.Log("Invoking wave with enemies: " + string.Join(", ", (object[])enemyPrefabs));

        enemiesToDefeat = enemyCountPerPrefab * enemyPrefabs.Length;

        // Esperar 1 segundo antes de invocar enemigos si no es la primera oleada
        if (enemyPrefabs.Length > 1 || enemyPrefabs[0] != prefab1)
        {
            yield return new WaitForSeconds(1f);
        }

        int spawnIndex = 0;

        for (int i = 0; i < enemyCountPerPrefab; i++)
        {
            foreach (var prefab in enemyPrefabs)
            {
                if (spawnIndex >= spawnPositions.Length)
                {
                    Debug.LogWarning("Not enough spawn positions set in the editor. Repeating spawn positions.");
                    spawnIndex = 0;
                }

                Instantiate(prefab, spawnPositions[spawnIndex].position, Quaternion.identity);
                spawnIndex++;
            }
        }

        // Esperar hasta que todos los enemigos sean derrotados
        while (enemiesToDefeat > 0)
        {
            yield return null;
        }
    }

    public void EnemyDefeated()
    {
        enemiesToDefeat--;
    }
}

