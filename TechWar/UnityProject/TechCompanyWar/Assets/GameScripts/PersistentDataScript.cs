using UnityEngine;
using System.Collections;

public class PersistentDataScript : MonoBehaviour {

    // Use this for initialization
    public static PersistentDataScript Instance;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    int Winner;

    int playerPhoneCount;
    int playerLaptopCount;
    int playerHeadphoneCount;
    int enemyPhoneCount;
    int enemyLaptopCount;
    int enemyHeadphoneCount;

    void Start()
    {
        Winner = -1;
    }

    //0 FOR PLAYER, 1 FOR AI
    public void setWinner(int win)
    {
        Winner = win;
    }

    public int getWinner()
    {
        return Winner;
    }

    public void setSellData(int a, int b, int c, int d, int e, int f)
    {
        playerPhoneCount = a;
        playerLaptopCount = b;
        playerHeadphoneCount = c;
        enemyPhoneCount = d;
        enemyLaptopCount = e;
        enemyHeadphoneCount = f;
    }


}
