using UnityEngine;
using UnityEngine.UI;

public class EnemyTypes : MonoBehaviour
{
    protected GameManager gm;
    public  ObjectPool objectPool;

    protected string enemyTag;
    protected float speed;
    protected float health;
    protected float attackPower;
    protected float deadPrice;
    protected float upgradeRatio;
    protected Transform target;
    protected int wavePointIndex = 0;

    //FOR UI
    protected Slider healthBarSlider;

    protected void SetWayPoint()
    {
        Vector2 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if (Vector2.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
        if (health <= 0)
        {
            EnemyDead();
        }
    }

    void GetNextWaypoint()
    {
        if (wavePointIndex >= Waypoints.points.Length - 1)
        {
            EnemyFinished();
            PlayerStats.health -= attackPower;
        }

        wavePointIndex++;
        target = Waypoints.points[wavePointIndex];
    }

    protected void EnemyDead()
    {
        PlayerStats.coin += deadPrice;
        gm.activeEnemy--;
        gm.killedEnemyCount++;
        objectPool.ReturnGameObject(gameObject);
    }

    protected void EnemyFinished()
    {
        gm.activeEnemy--;
        objectPool.ReturnGameObject(gameObject);
    }
}
