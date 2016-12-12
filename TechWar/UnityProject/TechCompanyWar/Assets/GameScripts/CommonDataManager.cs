using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommonDataManager : MonoBehaviour {
    //THIS CLASS STORES INFORMATION AVAILABLE AND NECESSARY FOR ALL PLAYERS
    const float MAX_DETECT_DISTANCE = 100.0f;

    //AI DOES NOT HAVE A MAX RANGE
    public int AIcolor;
    public int PlayerColor;
    
    GameObject[] citizens;

    int playerPhoneCount;
    int playerLaptopCount;
    int playerHeadphoneCount;
    int enemyPhoneCount;
    int enemyLaptopCount;
    int enemyHeadphoneCount;

    GameStateScript gameState;

    public PlayerBaseScript playerBase;
    public AIBaseScript AIBase;

    PersistentDataScript data;

    int day;
    int month;
    int year;
    //DISPLAYS
    float lastTime;


    public int getPlayerProductSellStat(int callerType)
    {
        int count = 0;
        foreach (GameObject c in citizens)
        {
            if(c.GetComponent<CitizenScript>().getColorWithType(callerType) == PlayerColor)
            {
                count++;
            }
        }
        //UPDATE HERE TO SAVE NUMBER OF TIMES THIS IS CALLED
        if(callerType == 0)
        {
            playerPhoneCount = count;
        }
        else if(callerType == 1)
        {
            playerLaptopCount = count;
        }else if(callerType == 2)
        {
            playerHeadphoneCount = count;
        }
        return count;
    }

    public int getAIProductSellStat(int callerType)
    {
        int count = 0;
        foreach (GameObject c in citizens)
        {
            if (c.GetComponent<CitizenScript>().getColorWithType(callerType) == AIcolor)
            {
                count++;
            }
        }
        //UPDATE HERE TO SAVE NUMBER OF TIMES THIS IS CALLED
        if (callerType == 0)
        {
            enemyPhoneCount = count;
        }
        else if (callerType == 1)
        {
            enemyLaptopCount = count;
        }
        else if (callerType == 2)
        {
            enemyHeadphoneCount = count;
        }
        return count;
    }

    public int getTotalPopulation()
    {
        return citizens.Length;
    }

	// Use this for initialization
	void Start () {
        citizens = GameObject.FindGameObjectsWithTag("targets");
        gameState = GetComponent<GameStateScript>();
        day = 1;
        month = 1;
        year = 2017;
        data = GameObject.FindGameObjectWithTag("GameOverData").GetComponent<PersistentDataScript>();
    }

    public GameObject getClosestCitizenWithDifferentColor(Transform callerTransform, int callerType, int callerColor, List<CitizenScript> unwillingBuyers)
    {
        float dist = MAX_DETECT_DISTANCE;
        if(callerColor == AIcolor)
        {
            dist = 10000.0f;
        }
        GameObject target = null;
        foreach (GameObject c in citizens)
        {
            //THIS ONE ONLY RETURNS THE CLOSEST CITIZEN WITH THE TYPE OF PRODUCT IN A DIFFERENT COLOR
            if (c.GetComponent<CitizenScript>().getColorWithType(callerType) != callerColor 
                && (!unwillingBuyers.Contains(c.GetComponent<CitizenScript>()))
                && (!c.GetComponent<CitizenScript>().isTypeInSession(callerType)))
            {
                float temp = Vector3.Distance(callerTransform.position, c.transform.position);
                if (dist > temp)
                {
                    dist = temp;
                    target = c;
                }
            }
        }
        return target;
    }

    public void updateStats()
    {

    }

    // Update is called once per frame
    void Update () {
        //update time, this number controls how fast one day is
        if (Time.time - lastTime >= 0.1)
        {
            updateTime();
            lastTime = Time.time;
        }

        //CHECK IF GAME IS FINISHED
        if(playerBase.getResource() <= 0)
        {
            data.setWinner(1);
            data.setSellData(playerPhoneCount,
            playerLaptopCount,
            playerHeadphoneCount,
            enemyPhoneCount,
            enemyLaptopCount,
            enemyHeadphoneCount);
            gameState.goToGameOverScreen();
        }

        if(AIBase.getResource() <= 0)
        {
            data.setWinner(0);
            data.setSellData(playerPhoneCount,
            playerLaptopCount,
            playerHeadphoneCount,
            enemyPhoneCount,
            enemyLaptopCount,
            enemyHeadphoneCount);
            gameState.goToGameOverScreen();
        }

        if (playerPhoneCount*2 >= getTotalPopulation() &&
            playerLaptopCount*2 >= getTotalPopulation() &&
            playerHeadphoneCount*2 >= getTotalPopulation())
        {
            data.setWinner(0);
            data.setSellData(playerPhoneCount,
            playerLaptopCount,
            playerHeadphoneCount,
            enemyPhoneCount,
            enemyLaptopCount,
            enemyHeadphoneCount);
            gameState.goToGameOverScreen();
        }

        if (enemyPhoneCount * 2 >= getTotalPopulation() &&
            enemyLaptopCount * 2 >= getTotalPopulation() &&
            enemyHeadphoneCount * 2 >= getTotalPopulation())
        {
            data.setWinner(1);
            data.setSellData(playerPhoneCount,
            playerLaptopCount,
            playerHeadphoneCount,
            enemyPhoneCount,
            enemyLaptopCount,
            enemyHeadphoneCount);
            gameState.goToGameOverScreen();
        }

    }

    void updateTime()
    {
        //10 seconds a day
        day++;
        if (day > 30)
        {
            day = 1;
            month++;
            playerBase.paySalesmanMonthlySalary();
            AIBase.paySalesmanMonthlySalary();
        }
        if (month > 12)
        {
            month = 1;
            year++;
            playerBase.payLoan();
        }
        playerBase.updateTimeHelperMethod(month, day, year);
    }
}
