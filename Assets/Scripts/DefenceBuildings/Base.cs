using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    public GameManager gm;

    public Button UpgradeButton;

    bool isUpgraded;
    float upgradePrice;
    int upgradeX = 10;
    int repairPercentOfX = 5;

    private void Start()
    {
        upgradePrice = 250f;
        isUpgraded = false;
    }

    private void FixedUpdate()
    {
        CheckForUpgrade();
    }

    public void BaseHealthRepair()
    {
        if (isUpgraded)
        {
            PlayerStats.health = PlayerStats.health + upgradeX;
        }
        PlayerStats.health = PlayerStats.health + ((PlayerStats.health * repairPercentOfX) / 100);
    }

    void CheckForUpgrade()
    {
        if (PlayerStats.coin >= upgradePrice && !isUpgraded)
        {
            UpgradeButton.interactable = true;
        }
        else
        {
            UpgradeButton.interactable = false;
        }
    }

    public void BaseUpgradeButton()
    {
        BaseUpgrade();
    }

    void BaseUpgrade()
    {
        PlayerStats.coin -= upgradePrice;
        isUpgraded = true;
    }
}
