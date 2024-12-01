using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float timeBtwSpawn = 1.5f; // Tiempo inicial entre generación de enemigos
    public float minTimeBtwSpawn = 0.3f; // Tiempo mínimo permitido entre generación
    public float decreaseRate = 0.1f; // Cantidad por la cual reducir el tiempo entre cada generación
    private float timer = 0;

    public Transform leftpoint;
    public Transform rightpoint;
    public GameObject EnemyPrefab;

    void Update()
    {
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        if (timer < timeBtwSpawn)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            float x = Random.Range(leftpoint.position.x, rightpoint.position.x);
            Vector3 newpost = new Vector3(x, transform.position.y, 0);
            Instantiate(EnemyPrefab, newpost, Quaternion.Euler(0, 0, 0));

            // Disminuir el tiempo entre spawns, asegurando que no baje del mínimo
            timeBtwSpawn = Mathf.Max(timeBtwSpawn - decreaseRate, minTimeBtwSpawn);
        }
    }
}
