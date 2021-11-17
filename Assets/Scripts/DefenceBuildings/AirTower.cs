using UnityEngine;
using UnityEngine.UI;

public class AirTower : Towers
{
    public GameObject bulletPrefab;

    void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        objectPool = FindObjectOfType<ObjectPool>();

        coast = 200f;
        level = 0;
        MaxLevel = 3;
        upgradePrice = 200f;
        attackPower = 2f;
        range = 3f;
        upgradeRatio = 1.2f;
        enemyTag = "AirEnemy";
        fireRate = 1f;
        fireCountDown = 0f;
        firePoint = transform.GetChild(0);

        levelBarText = gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<Text>();
        upgradeCostText = gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).GetComponent<Text>();
        upgradeButton = gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).GetComponent<Button>();

        UIUpdate();
    }

    void FixedUpdate()
    {
        if(target == null)
        {
            UpdateTarget();
        }
        CheckForUpgrade();
    }

    // Update is called once per frame
    void Update()
    {

        if (target == null)
        {
            return;
        }
        else
        {
            if(!target.gameObject.activeInHierarchy || Vector2.Distance(target.gameObject.transform.position, transform.position) > range)
            {
                target = null;
            }
        }

        if (fireCountDown <= 0f)
        {
            Shoot(bulletPrefab);
            fireCountDown = 1f / fireRate;
        }

        fireCountDown -= Time.deltaTime;
    }

    public void Upgrade()
    {
        if (PlayerStats.coin >= upgradePrice && level < MaxLevel)
        {
            level++;
            PlayerStats.coin -= upgradePrice;
            TowerUpgrade();
            UIUpdate();
        }
    }
}
