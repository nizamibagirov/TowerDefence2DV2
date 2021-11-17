using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static float coin;
    public static float health;

    // Start is called before the first frame update
    void Awake()
    {
        coin = 1000f; //for testing
        health = 100f;
    }
}
