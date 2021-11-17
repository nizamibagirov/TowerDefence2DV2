using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    UIManager um;

    public int activeEnemy;
    public int wave = 0;
    public int killedEnemyCount = 0;
    public bool isBuyed;
    public bool gameIsOver;
    public int currentWave = 1;
    DialogueTrigger trigger;

    private void Awake()
    {
        Time.timeScale = 1f;
        trigger = gameObject.GetComponent<DialogueTrigger>();
    }
    // Start is called before the first frame update
    void Start()
    {
        trigger.TriggerDialogue();
        gameIsOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerStats.health <= 0 || Input.GetKey(KeyCode.R)) //For testing game over  
        {
            isBuyed = false;
            gameIsOver = true;
            GameOver();
        }
        //for test
        if(Input.GetKey(KeyCode.G))
        {
            activeEnemy--;
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        Camera.main.GetComponent<cameraMovement>().enabled = false;
        um.playerPanel.SetActive(false);
        um.gameOverPanel.SetActive(true);
        um.gameOverPanel.GetComponentInParent<Canvas>().sortingOrder = 2;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
}
