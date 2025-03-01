using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Robot enemyPrefab;
    [SerializeField] float spawnTime = 5f;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            Instantiate(enemyPrefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
