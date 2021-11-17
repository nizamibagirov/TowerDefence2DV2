using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameManager gm;

    //For UI
    //First Scene
    public GameObject buildPanel;
    public GameObject playerPanel;
    //Game Scene
    public Text coinText;
    public Text healthText;
    public Text waveText;
    //Restart Scene
    public Text waveTextForRestartMenu;
    public GameObject gameOverPanel;
    public Text killedEnemyCountText;
    //Dialogue Manager
    public GameObject panel;
    public GameObject gameUI;
    //BuildPanel
    public Button buyButton1;
    public Button buyButton2;
    public Button startGameButton;
    public GameObject infoPanel;

    private void Start()
    {
        UIUpdate();
        CheckButtons();
    }

    private void Update()
    {
        UIUpdate();
        CheckButtons();
    }

    void UIUpdate()
    {
        coinText.text = PlayerStats.coin.ToString("0");
        healthText.text = PlayerStats.health.ToString("0");
        waveText.text = gm.wave.ToString();
        killedEnemyCountText.text = gm.killedEnemyCount.ToString("0");
        waveTextForRestartMenu.text = gm.wave.ToString("0");
    }

    void CheckButtons()
    {
        if (PlayerStats.coin >= int.Parse(buyButton1.transform.GetChild(2).GetComponent<Text>().text))
        {
            buyButton1.interactable = true;
        }
        else
        {
            buyButton1.interactable = false;
        }
        if (PlayerStats.coin >= int.Parse(buyButton2.transform.GetChild(2).GetComponent<Text>().text))
        {
            buyButton2.interactable = true;
        }
        else
        {
            buyButton2.interactable = false;
        }
        if (gm.isBuyed)
        {
            startGameButton.interactable = true;
        }
        else
        {
            startGameButton.interactable = false;
        }
    }

    public void showInfoMenu(bool check)
    {
        if(check)
        {
            infoPanel.SetActive(true);
        }
        else
        {
            infoPanel.SetActive(false);
        }
    }
}
