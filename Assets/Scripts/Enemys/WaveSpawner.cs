using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    GameManager gm;
    ObjectPool objectPool;

    public EnemyStats[] Enemys;
    public Transform spawnPoint;
    public Base gameBase;

    private void Awake()
    {
        gm = gameObject.GetComponent<GameManager>();
        objectPool = gameObject.GetComponent<ObjectPool>();
    }

    private void Update()
    {
        if (gm.activeEnemy == 0)
        {
            gm.wave++;
            StartCoroutine(SpawnWave());
        }
        if (gm.currentWave < gm.wave)
        {
            gm.currentWave = gm.wave;
            EnemyUpgrade();
            gameBase.BaseHealthRepair();
        }
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < Random.Range(gm.wave, gm.wave + 10); i++)
        {
            gm.activeEnemy++;
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }
    void SpawnEnemy()
    {
        int index = Random.Range(0, Enemys.Length);
        GameObject instantiatedEnemy = objectPool.GetObject(Enemys[index].enemy);
        instantiatedEnemy.transform.position = spawnPoint.position;
        instantiatedEnemy.transform.rotation = spawnPoint.rotation;
        instantiatedEnemy.GetComponent<Enemy>().SetEnemy(Enemys[index].enemyStats[0], Enemys[index].enemyStats[1], Enemys[index].enemyStats[2], Enemys[index].enemyStats[3], Enemys[index].upgradeRatio, Enemys[index].enemyTag);
    }

    void EnemyUpgrade()
    {
        foreach (EnemyStats objects in Enemys)
        {
            int Index = Random.Range(0, objects.enemyStats.Length); ;
            for (int i = 0; i < Index; i++)
            {
                int randomIndex = Random.Range(0, objects.enemyStats.Length);
                objects.enemyStats[randomIndex] = objects.enemyStats[randomIndex] * objects.upgradeRatio;
            }
        }
    }
}