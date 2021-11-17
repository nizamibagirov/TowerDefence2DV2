using UnityEngine;
using UnityEngine.UI;

public class Enemy : EnemyTypes
{
    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    public void SetEnemy(float Speed, float Health, float AttackPower, float DeadPrice, float UpgradeRatio, string EnemyTag)
    {
        speed = Speed;
        health = Health;
        attackPower = AttackPower;
        deadPrice = DeadPrice;
        upgradeRatio = UpgradeRatio;
        enemyTag = EnemyTag;
        gameObject.tag = enemyTag;
        healthBarSlider.minValue = 0;
        healthBarSlider.maxValue = health;
    }

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        objectPool = FindObjectOfType<ObjectPool>();
        target = Waypoints.points[wavePointIndex];
        healthBarSlider = gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Slider>();
    }

    private void OnEnable()
    {
        wavePointIndex = 0;
        target = Waypoints.points[wavePointIndex];
    }

    private void Update()
    {
        SetWayPoint();
        healthBarSlider.value = health;
    }
}
