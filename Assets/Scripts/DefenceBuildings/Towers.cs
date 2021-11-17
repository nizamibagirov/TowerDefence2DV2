using UnityEngine;
using UnityEngine.UI;

public class Towers : MonoBehaviour
{

    protected GameManager gm;
    protected ObjectPool objectPool;

    protected Transform target;
    protected Transform firePoint;

    protected float coast;
    protected float level;
    protected float MaxLevel;
    protected float range;
    protected float upgradePrice;
    protected float attackPower;
    protected float upgradeRatio;
    protected string enemyTag;
    protected float fireRate;
    protected float fireCountDown;

    public float Coast
    {
        get { return coast; }
    }

    //For UI
    protected Text levelBarText;
    protected Text upgradeCostText;
    protected Button upgradeButton;

    protected void UpdateTarget()
    {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            float shortestDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;
            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy != null && shortestDistance <= range)
            {
                target = nearestEnemy.transform;
            }
            else
            {
                target = null;
            }
    }

    protected void Shoot(GameObject bulletPrefab)
    {
        GameObject bulletGO = objectPool.GetObject(bulletPrefab);
        bulletGO.transform.position = firePoint.position;
        bulletGO.transform.rotation = firePoint.rotation;
        bullet bullet = bulletGO.GetComponent<bullet>();

        if (bullet != null)
        {
            bullet.CarryTarget(target, attackPower);
        }
    }

    protected void TowerUpgrade()
    {
        upgradePrice *= upgradeRatio;
        attackPower *= upgradeRatio;
        range *= upgradeRatio;
        fireRate *= upgradeRatio;
    }

    protected void UIUpdate()
    {
        levelBarText.text = level.ToString() + "/" + MaxLevel.ToString();
        upgradeCostText.text = upgradePrice.ToString();
    }

    protected void CheckForUpgrade()
    {
        if (PlayerStats.coin >= upgradePrice && level < MaxLevel)
        {
            upgradeButton.interactable = true;
        }
        else if (level == MaxLevel)
        {
            level++;
            upgradeButton.gameObject.SetActive(false);
            return;
        }
        else
        {
            upgradeButton.interactable = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
