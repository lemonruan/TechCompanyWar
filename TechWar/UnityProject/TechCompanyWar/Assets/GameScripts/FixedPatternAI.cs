using UnityEngine;
using System.Collections;

public class FixedPatternAI : MonoBehaviour {

    //TYPES OF PATTERNS
    const int PHONE_LOVER_TYPE = 0;
    const int LAPTOP_LOVER_TYPE = 1;
    const int HEADPHONE_LOVER_TYPE = 2;
    const int WHATEVER_TYPE = 3;


    int patternType;


    public float AIActionInterval;
    
    float timeLeft;

    //9 button, 0-8 represent each action
    int actionIndex;

    AIBaseScript AIBase;

	// Use this for initialization
	void Start () {
        timeLeft = AIActionInterval;
        actionIndex = 2;
        //INIT TO 3 FOR A DEMO
        patternType = 3;
        AIBase = GetComponent<AIBaseScript>();
	}
	
	// Update is called once per frame
	void Update () {
        if(timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else
        {
            //NEXT ACTION
            if(AIBase.getRate() == -1)
            {
                takeAction();
                actionIndex++;
                if (actionIndex > 8)
                {
                    actionIndex = 0;
                }
            }
            timeLeft = AIActionInterval;
        }
	}

    //TAKE AN ACTION BASED ON THE CURRENT actionIndex
    void takeAction()
    {
        if(actionIndex == 0)
        {
            AIBase.UpgradePhoneTech();
        }else if(actionIndex == 1)
        {
            AIBase.UpgradePhoneDesign();
        }else if(actionIndex == 2)
        {
            AIBase.TrainPhoneSalesman();
        }else if(actionIndex == 3)
        {
            AIBase.UpgradeLaptopTech();
        }else if(actionIndex == 4)
        {
            AIBase.UpgradeLaptopDesign();
        }else if(actionIndex == 5)
        {
            AIBase.TrainLaptopSalesman();
        }else if(actionIndex == 6)
        {
            AIBase.UpgradeHeadphoneTech();
        }else if(actionIndex == 7)
        {
            AIBase.UpgradeHeadphoneDesign();
        }else if(actionIndex == 8)
        {
            AIBase.TrainHeadphoneSalesman();
        }
    }
}
