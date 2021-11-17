using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    public GameManager gm;
    public UIManager um;
    public GameObject archerTower;
    public GameObject airTower;

    private GameObject go;

    bool canBuild = true;
    bool changePreviewColor = false;

    private void Awake()
    {
        go = null;
    }
    // Update is called once per frame
    void Update()
    {
        if (go != null)
        {
            um.showInfoMenu(true);
            if (Input.GetKey(KeyCode.Escape))
            {
                Destroy(go);
                go = null;
                return;
            }
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (!hit)
            {
                canBuild = true;
                if (changePreviewColor)
                {
                    changePreviewColor = false;
                    go.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                }
            }
            else
            {
                canBuild = false;
                if (!changePreviewColor)
                {
                    changePreviewColor = true;
                    go.gameObject.GetComponent<SpriteRenderer>().color = new Color(188f / 255, 66f / 255, 66f / 255, 255);
                }
            }

            float hitX = Mathf.Round(worldPosition.x);
            float hitY = Mathf.Round(worldPosition.y);

            go.transform.position = new Vector2(
                Mathf.Clamp(hitX, Camera.main.GetComponent<cameraMovement>().mapMinX + 1, Camera.main.GetComponent<cameraMovement>().mapMaxX - 1),
                Mathf.Clamp(hitY, Camera.main.GetComponent<cameraMovement>().mapMinY + 1, Camera.main.GetComponent<cameraMovement>().mapMaxY - 1));

            if (Input.GetKeyDown(KeyCode.Mouse0) && canBuild)
            {
                GameObject Build = go.gameObject;
                Build.transform.position = new Vector2(
                    Mathf.Clamp(hitX, Camera.main.GetComponent<cameraMovement>().mapMinX + 1, Camera.main.GetComponent<cameraMovement>().mapMaxX - 1),
                    Mathf.Clamp(hitY, Camera.main.GetComponent<cameraMovement>().mapMinY + 1, Camera.main.GetComponent<cameraMovement>().mapMaxY - 1));
                Build.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                Build.transform.GetChild(2).gameObject.SetActive(true);
                PlayerStats.coin -= Build.gameObject.GetComponent<Towers>().Coast;
                go = null;
                if (!gm.isBuyed)
                {
                    //if player don't buy Tower,can't start game
                    gm.isBuyed = true;
                }
            }
        }
        else
        {
            um.showInfoMenu(false);
        }
    }

    public void BuyButton1()
    {
        if (go == null)
        {
            go = Instantiate(archerTower);
        }
    }

    public void BuyButton2()
    {
        if (go == null)
        {
            go = Instantiate(airTower);
        }
    }
}
