using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIBaseScript : MonoBehaviour {


    //AI VERSION OF PLAYERBASESCRIPT, DOES NOT HAVE A HUD, CALL FUNCTIONS BASED ON HOW AI IS CODED
    //TYPES CONSTANTS
    const int PHONE_TECH_TYPE = 0;
    const int PHONE_DESIGN_TYPE = 1;
    const int PHONE_SALE_TYPE = 2;

    const int LAPTOP_TECH_TYPE = 3;
    const int LAPTOP_DESIGN_TYPE = 4;
    const int LAPTOP_SALE_TYPE = 5;

    const int HEADPHONE_TECH_TYPE = 6;
    const int HEADPHONE_DESIGN_TYPE = 7;
    const int HEADPHONE_SALE_TYPE = 8;

    //COSTS CHANGE CONSTANTS
    const int PHONE_TECH_COST_CHANGE = 600;
    const int LAPTOP_TECH_COST_CHANGE = 800;
    const int HEADPHONE_TECH_COST_CHANGE = 100;

    //SLAESMAN TRANING COST
    const int SALE_TRAINING_COST = 3;

    //PRODUCT INTEREST CHANGE
    const float PHONE_INTEREST_CHANGE = 0.04f;
    const float LAPTOP_INTEREST_CHANGE = 0.06f;
    const float HEADPHONE_INTEREST_CHANGE = 0.02f;

    public int companyColor;

    // Cash, in k dollar
    private float resource;

    //0 FOR PHONE TECH, 1 FOR ...
    private int type;

    private int phoneTechLevel;
    private int phoneTechCost;
    private int phoneDesignLevel;
    private int phoneDesignCost;
    private int phonePS;
    private float phoneInterest;
    

    private int laptopTechLevel;
    private int laptopTechCost;
    private int laptopDesignLevel;
    private int laptopDesignCost;
    private int laptopPS;
    private float laptopInterest;

    private int headphoneTechLevel;
    private int headphoneTechCost;
    private int headphoneDesignLevel;
    private int headphoneDesignCost;
    private int headphonePS;
    private float headphoneInterest;


    public Transform spawnspot;

    int deltaPS;

    int rate;
    int perc;
    float lastTime;

    public GameObject saleAgent;
    List<SalesmanScript> allSalesMans;


    //CALL THIS TO TELL IF THERE IS A ONGOING PROCESS
    public int getRate()
    {
        return rate;
    }



    // Use this for initialization
    void Start()
    {
        rate = -1;
        type = -1;
        // Starting resource
        resource = 5000;
        phoneTechLevel = 1;
        phoneTechCost = 4000;
        phoneDesignLevel = 1;
        phoneDesignCost = 500;
        phonePS = 50;
        //INIT 0.2 INTEREST
        phoneInterest = 0.15f;

        laptopTechLevel = 1;
        laptopTechCost = 7000;
        laptopDesignLevel = 1;
        laptopDesignCost = 600;
        laptopPS = 100;
        laptopInterest = 0.2f;

        headphoneTechLevel = 1;
        headphoneTechCost = 900;
        headphoneDesignLevel = 1;
        headphoneDesignCost = 200;
        headphonePS = 30;
        headphoneInterest = 0.08f;


        deltaPS = 0;

        allSalesMans = new List<SalesmanScript>();
    }


    //FROM HERE, THESE 9 FUNCTIONS HANDLE THE 9 BUTTONS FOR PHONE, LAPTOP AND HEADPHONE
    public void UpgradePhoneTech()
    {
        if (rate == -1)
        {
            //IF NO OTHER IS BEING RESEARCHED

            //DETERMINE IF THERE IS ENOUGH RESOURCE FOR IT
            if (resource < phoneTechCost)
            {
                return;
            }

            type = PHONE_TECH_TYPE;
            updateResourceAndCosts();

            lastTime = Time.time;
            perc = 0;
            rate = 5;

            deltaPS = 80;
        }
    }

    public void UpgradePhoneDesign()
    {
        if (rate == -1)
        {
            if (resource < phoneDesignCost)
            {
                return;
            }
            type = PHONE_DESIGN_TYPE;
            updateResourceAndCosts();

            lastTime = Time.time;
            perc = 0;
            rate = 5;
            deltaPS = 60;
        }
    }

    public void TrainPhoneSalesman()
    {
        if (rate == -1)
        {
            if (resource < SALE_TRAINING_COST)
            {
                return;
            }
            type = PHONE_SALE_TYPE;
            updateResourceAndCosts();

            lastTime = Time.time;
            perc = 0;
            rate = 20;
        }
    }

    public void UpgradeLaptopTech()
    {
        if (rate == -1)
        {
            if (resource < laptopTechCost)
            {
                return;
            }
            type = LAPTOP_TECH_TYPE;
            updateResourceAndCosts();

            lastTime = Time.time;
            perc = 0;
            rate = 3;

            deltaPS = 150;
        }
    }

    public void UpgradeLaptopDesign()
    {
        if (rate == -1)
        {
            if (resource < laptopDesignCost)
            {
                return;
            }
            type = LAPTOP_DESIGN_TYPE;
            updateResourceAndCosts();

            lastTime = Time.time;
            perc = 0;
            rate = 3;

            deltaPS = 100;
        }
    }

    public void TrainLaptopSalesman()
    {
        if (rate == -1)
        {
            if (resource < SALE_TRAINING_COST)
            {
                return;
            }
            type = LAPTOP_SALE_TYPE;
            updateResourceAndCosts();

            lastTime = Time.time;
            perc = 0;
            rate = 15;

        }
    }


    public void UpgradeHeadphoneTech()
    {
        if (rate == -1)
        {
            if (resource < headphoneTechCost)
            {
                return;
            }
            type = HEADPHONE_TECH_TYPE;
            updateResourceAndCosts();

            lastTime = Time.time;
            perc = 0;
            rate = 10;

            deltaPS = 60;
        }
    }

    public void UpgradeHeadphoneDesign()
    {
        if (rate == -1)
        {
            if (resource < headphoneDesignCost)
            {
                return;
            }
            type = HEADPHONE_DESIGN_TYPE;
            updateResourceAndCosts();

            lastTime = Time.time;
            perc = 0;
            rate = 20;

            deltaPS = 30;
        }
    }

    public void TrainHeadphoneSalesman()
    {
        if (rate == -1)
        {
            if (resource < SALE_TRAINING_COST)
            {
                return;
            }
            type = HEADPHONE_SALE_TYPE;
            updateResourceAndCosts();

            lastTime = Time.time;
            perc = 0;
            rate = 25;

        }
    }



    // Update is called once per frame. 
    void Update()
    {
        if (rate > 0)
        {
            if (Time.time - lastTime >= 1)
            {
                perc += rate;
                lastTime = Time.time;
            }
            if (perc >= 100)
            {
                rate = -1;
                updateStats();
            }

        }
    }


    //Update the stats of the reseached or create an instance of a salesman of the corresponding type
    void updateStats()
    {
        if (type == PHONE_TECH_TYPE)
        {
            phonePS += deltaPS;
            phoneTechLevel++;
            phoneInterest += PHONE_INTEREST_CHANGE;
            type = -1;
            deltaPS = 0;
        }
        else if (type == PHONE_DESIGN_TYPE)
        {
            phonePS += deltaPS;
            phoneDesignLevel++;
            type = -1;
            deltaPS = 0;
        }
        else if (type == PHONE_SALE_TYPE)
        {
            GameObject newAgent = (GameObject)Instantiate(saleAgent, spawnspot.position, Quaternion.identity);
            newAgent.GetComponent<SalesmanScript>().setSaleType(0, companyColor, phonePS);
            allSalesMans.Add(newAgent.GetComponent<SalesmanScript>());
            type = -1;
        }
        else if (type == LAPTOP_TECH_TYPE)
        {
            laptopPS += deltaPS;
            laptopTechLevel++;
            laptopInterest += LAPTOP_INTEREST_CHANGE;
            type = -1;
            deltaPS = 0;
        }
        else if (type == LAPTOP_DESIGN_TYPE)
        {
            laptopPS += deltaPS;
            laptopDesignLevel++;
            type = -1;
            deltaPS = 0;
        }
        else if (type == LAPTOP_SALE_TYPE)
        {
            GameObject newAgent = (GameObject)Instantiate(saleAgent, spawnspot.position, Quaternion.identity);
            newAgent.GetComponent<SalesmanScript>().setSaleType(1, companyColor, laptopPS);
            allSalesMans.Add(newAgent.GetComponent<SalesmanScript>());
            type = -1;
        }
        else if (type == HEADPHONE_TECH_TYPE)
        {
            headphonePS += deltaPS;
            headphoneTechLevel++;
            headphoneInterest += HEADPHONE_INTEREST_CHANGE;
            type = -1;
            deltaPS = 0;
        }
        else if (type == HEADPHONE_DESIGN_TYPE)
        {
            headphonePS += deltaPS;
            headphoneDesignLevel++;
            
            type = -1;
            deltaPS = 0;
        }
        else if (type == HEADPHONE_SALE_TYPE)
        {
            GameObject newAgent = (GameObject)Instantiate(saleAgent, spawnspot.position, Quaternion.identity);
            newAgent.GetComponent<SalesmanScript>().setSaleType(2, companyColor, headphonePS);
            allSalesMans.Add(newAgent.GetComponent<SalesmanScript>());
            type = -1;
        }

        updateSalesmansProduct();
    }


    void updateResourceAndCosts()
    {
        if (type == PHONE_TECH_TYPE)
        {
            resource -= phoneTechCost;
            phoneTechCost += PHONE_TECH_COST_CHANGE;
        }
        else if (type == PHONE_DESIGN_TYPE)
        {
            resource -= phoneDesignCost;
        }
        else if (type == PHONE_SALE_TYPE)
        {
            resource -= SALE_TRAINING_COST;
        }
        else if (type == LAPTOP_TECH_TYPE)
        {
            resource -= laptopTechCost;
            laptopTechCost += LAPTOP_TECH_COST_CHANGE;
        }
        else if (type == LAPTOP_DESIGN_TYPE)
        {
            resource -= laptopDesignCost;
        }
        else if (type == LAPTOP_SALE_TYPE)
        {
            resource -= SALE_TRAINING_COST;
        }
        else if (type == HEADPHONE_TECH_TYPE)
        {
            resource -= headphoneTechCost;
            headphoneTechCost += HEADPHONE_TECH_COST_CHANGE;
        }
        else if (type == HEADPHONE_DESIGN_TYPE)
        {
            resource -= headphoneDesignCost;
        }
        else if (type == HEADPHONE_SALE_TYPE)
        {
            resource -= SALE_TRAINING_COST;
        }
    }

    public int getCompanyColor()
    {
        return companyColor;
    }

    public void addToResource(int saleType)
    {
        //0 FOR PHONE, 1 FOR LAPTOP, 2 FOR HEADPHONE
        if (saleType == 0)
        {
            resource += phoneInterest;
        }
        else if (saleType == 1)
        {
            resource += laptopInterest;
        }
        else if (saleType == 2)
        {
            resource += headphoneInterest;
        }
    }

    void updateSalesmansProduct()
    {
        foreach(SalesmanScript seller in allSalesMans)
        {
            seller.setProductScoreIfIsType(phonePS, 0);
            seller.setProductScoreIfIsType(laptopPS, 1);
            seller.setProductScoreIfIsType(headphonePS, 2);
        }
    }

}
