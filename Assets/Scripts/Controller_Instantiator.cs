using System.Collections.Generic;
using UnityEngine;

public class Controller_Instantiator : MonoBehaviour
{
    public List<GameObject> enemies;
    public GameObject instantiatePos;
    public float timer = 7;
    private float time;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnEnemies();
            timer = 7;
        }
    }

    private void SpawnEnemies()
    {
        int rnd = Random.Range(0, enemies.Count);
        for (int i = 0; i < 5; i++)
        {
            Vector3 pos = new Vector3(
                instantiatePos.transform.position.x + (i * 4),
                instantiatePos.transform.position.y,
                0
            );
            Instantiate(enemies[rnd], pos, Quaternion.identity);
        }
    }
}
