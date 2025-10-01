using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{

    [Header("Prefabs")]
    [SerializeField] private GameObject enemyPrefab;

    [Header("Settings")]
    [SerializeField] private int spawnCD = 5;
    private int spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnCD * 60;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer--;
        if (spawnTimer <= 0)
        {
            // Randomly generate enemies on a square that is 15 unit around the player
            int parameter = Random.Range(-15, 16);
            int line = Random.Range(0, 4);
            Vector3 loc;
            if (line == 0 || line == 2)
                loc = new Vector3((line - 1) * 15, 0, parameter);
            else
                loc = new Vector3(parameter, 0, (line - 2) * 15);

            GameObject enemy = Instantiate(enemyPrefab, loc, Quaternion.identity);
            spawnTimer = spawnCD * 60;
        }
    }
}
