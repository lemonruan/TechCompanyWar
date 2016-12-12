using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerBaseScript : MonoBehaviour {
    // This is the script that stores the data of all data of each tech tree,
    // as well as other data related to this player

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

    HUDScript HUD;

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

    float loanOwed;

    public int getPhoneTechCost()
    {
        return phoneTechCost;
    }
    public int getPhoneDesignCost()
    {
        return phoneDesignCost;
    }
    public int getLaptopTechCost()
    {
        return laptopTechCost;
    }
    public int getLaptopDesignCost()
    {
        return laptopDesignCost;
    }
    public int getHeadphoneTechCost()
    {
        return headphoneTechCost;
    }
    public int getHeadphoneDesignCost()
    {
        return headphoneDesignCost;
    }
    public int getSaleTrainingCost()
    {
        return SALE_TRAINING_COST;
    }


    public float getResource()
    {
        return resource;
    }
    // Use this for initialization
    void Start () {
        HUD = (HUDScript)(GameObject.FindGameObjectWithTag("HUD").GetComponent(typeof(HUDScript)));
        rate = -1;
        type = -1;
        // Starting resource
        loanOwed = 0;
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
        HUD.updateResource(resource);
        HUD.updatePhoneScore(phonePS);
        HUD.updateLaptopScore(laptopPS);
        HUD.updateHeadphoneScore(headphonePS);

        allSalesMans = new List<SalesmanScript>();
	}

    public void loan()
    {
        resource += 2000;
        loanOwed += 2400;
        HUD.updateResource(resource);
        HUD.updateLoan(loanOwed);
    }

    public void payLoan()
    {
        resource -= loanOwed;
        HUD.updateResource(resource);
        HUD.updateLoan(loanOwed);
    }

    //FROM HERE, THESE 9 FUNCTIONS HANDLE THE 9 BUTTONS FOR PHONE, LAPTOP AND HEADPHONE
    public void UpgradePhoneTech(Button b)
    {
        if (rate == -1)
        {
            //IF NO OTHER IS BEING RESEARCHED

            //DETERMINE IF THERE IS ENOUGH RESOURCE FOR IT
            if(resource < phoneTechCost)
            {
                return;
            }

            type = PHONE_TECH_TYPE;
            updateResourceAndCosts();

            HUD.StartAProgress(b);
            lastTime = Time.time;
            perc = 0;
            rate = 5;
            
            deltaPS = 60;
        }
    }

    public void UpgradePhoneDesign(Button b)
    {
        if (rate == -1)
        {
            if(resource < phoneDesignCost)
            {
                return;
            }
            type = PHONE_DESIGN_TYPE;
            updateResourceAndCosts();

            HUD.StartAProgress(b);
            lastTime = Time.time;
            perc = 0;
            rate = 5;
            deltaPS = 20;
        }
    }

    public void TrainPhoneSalesman(Button b)
    {
        if (rate == -1)
        {
            if (resource < SALE_TRAINING_COST)
            {
                return;
            }
            type = PHONE_SALE_TYPE;
            updateResourceAndCosts();

            HUD.StartAProgress(b);
            lastTime = Time.time;
            perc = 0;
            rate = 50;
        }
    }

    public void UpgradeLaptopTech(Button b)
    {
        if (rate == -1)
        {
            if (resource < laptopTechCost)
            {
                return;
            }
            type = LAPTOP_TECH_TYPE;
            updateResourceAndCosts();

            HUD.StartAProgress(b);
            lastTime = Time.time;
            perc = 0;
            rate = 3;
            
            deltaPS = 80;
        }
    }

    public void UpgradeLaptopDesign(Button b)
    {
        if (rate == -1)
        {
            if (resource < laptopDesignCost)
            {
                return;
            }
            type = LAPTOP_DESIGN_TYPE;
            updateResourceAndCosts();

            HUD.StartAProgress(b);
            lastTime = Time.time;
            perc = 0;
            rate = 3;
            
            deltaPS = 10;
        }
    }

    public void TrainLaptopSalesman(Button b)
    {
        if (rate == -1)
        {
            if (resource < SALE_TRAINING_COST)
            {
                return;
            }
            type = LAPTOP_SALE_TYPE;
            updateResourceAndCosts();

            HUD.StartAProgress(b);
            lastTime = Time.time;
            perc = 0;
            rate = 15;
            
        }
    }


    public void UpgradeHeadphoneTech(Button b)
    {
        if (rate == -1)
        {
            if (resource < headphoneTechCost)
            {
                return;
            }
            type = HEADPHONE_TECH_TYPE;
            updateResourceAndCosts();

            HUD.StartAProgress(b);
            lastTime = Time.time;
            perc = 0;
            rate = 10;

            deltaPS = 30;
        }
    }

    public void UpgradeHeadphoneDesign(Button b)
    {
        if (rate == -1)
        {
            if (resource < headphoneDesignCost)
            {
                return;
            }
            type = HEADPHONE_DESIGN_TYPE;
            updateResourceAndCosts();

            HUD.StartAProgress(b);
            lastTime = Time.time;
            perc = 0;
            rate = 10;

            deltaPS = 30;
        }
    }

    public void TrainHeadphoneSalesman(Button b)
    {
        if (rate == -1)
        {
            if (resource < SALE_TRAINING_COST)
            {
                return;
            }
            type = HEADPHONE_SALE_TYPE;
            updateResourceAndCosts();

            HUD.StartAProgress(b);
            lastTime = Time.time;
            perc = 0;
            rate = 25;
            
        }
    }




    // Update is called once per frame. Used for updating the progress bar in HUD
    void Update () {
	    if(rate > 0)
        {
            if(Time.time - lastTime >= 1)
            {
                perc += rate;
                HUD.UpdateProgress(perc);
                lastTime = Time.time;
            }
            if(perc >= 100)
            {
                HUD.finishProgress();
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
            HUD.updatePhoneScore(phonePS);
            type = -1;
            deltaPS = 0;
        }else if(type == PHONE_DESIGN_TYPE)
        {
            phonePS += deltaPS;
            phoneDesignLevel++;
            HUD.updatePhoneScore(phonePS);
            type = -1;
            deltaPS = 0;
        }else if(type == PHONE_SALE_TYPE)
        {
            GameObject newAgent = (GameObject)Instantiate(saleAgent, spawnspot.position, Quaternion.identity);
            newAgent.GetComponent<SalesmanScript>().setSaleType(0, companyColor, phonePS);
            allSalesMans.Add(newAgent.GetComponent<SalesmanScript>());
            type = -1;
            HUD.addMessage("A phone sale agent is trained.");
        }else if (type == LAPTOP_TECH_TYPE)
        {
            laptopPS += deltaPS;
            laptopTechLevel++;
            laptopInterest += LAPTOP_INTEREST_CHANGE;
            HUD.updateLaptopScore(laptopPS);
            type = -1;
            deltaPS = 0;
        }
        else if (type == LAPTOP_DESIGN_TYPE)
        {
            laptopPS += deltaPS;
            laptopDesignLevel++;
            HUD.updateLaptopScore(laptopPS);
            type = -1;
            deltaPS = 0;
        }
        else if (type == LAPTOP_SALE_TYPE)
        {
            GameObject newAgent = (GameObject)Instantiate(saleAgent, spawnspot.position, Quaternion.identity);
            newAgent.GetComponent<SalesmanScript>().setSaleType(1, companyColor, laptopPS);
            allSalesMans.Add(newAgent.GetComponent<SalesmanScript>());
            type = -1;
            HUD.addMessage("A laptop sale agent is trained.");
        }
        else if (type == HEADPHONE_TECH_TYPE)
        {
            headphonePS += deltaPS;
            headphoneTechLevel++;
            headphoneInterest += HEADPHONE_INTEREST_CHANGE;
            HUD.updateHeadphoneScore(headphonePS);
            type = -1;
            deltaPS = 0;
        }
        else if (type == HEADPHONE_DESIGN_TYPE)
        {
            headphonePS += deltaPS;
            headphoneDesignLevel++;
            HUD.updateHeadphoneScore(headphonePS);
            type = -1;
            deltaPS = 0;
        }
        else if (type == HEADPHONE_SALE_TYPE)
        {
            GameObject newAgent = (GameObject)Instantiate(saleAgent, spawnspot.position, Quaternion.identity);
            newAgent.GetComponent<SalesmanScript>().setSaleType(2, companyColor, headphonePS);
            allSalesMans.Add(newAgent.GetComponent<SalesmanScript>());
            type = -1;
            HUD.addMessage("A headphone sale agent is trained.");
        }

        updateSalesmansProduct();
    }


    void updateResourceAndCosts()
    {
        if(type == PHONE_TECH_TYPE)
        {
            resource -= phoneTechCost;
            phoneTechCost += PHONE_TECH_COST_CHANGE;
        }else if(type == PHONE_DESIGN_TYPE)
        {
            resource -= phoneDesignCost;
        }else if(type == PHONE_SALE_TYPE)
        {
            resource -= SALE_TRAINING_COST;
        }else if (type == LAPTOP_TECH_TYPE)
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
        }else if (type == HEADPHONE_TECH_TYPE)
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
        else
        {
            return;
        }
        HUD.updateResource(resource);
    }


    public int getCompanyColor()
    {
        return companyColor;
    }

    public void addToResource(int saleType)
    {
        //0 FOR PHONE, 1 FOR LAPTOP, 2 FOR HEADPHONE
        if(saleType == 0)
        {
            resource += phoneInterest * 100;
            HUD.addMessage("A hundred phones are sold with interest of "+ (phoneInterest*100).ToString("0.0") + "K");
        }else if(saleType == 1)
        {
            resource += laptopInterest * 100;
            HUD.addMessage("A hundred laptop is sold with interest of " + (laptopInterest * 100).ToString("0.0") + "K");
        }
        else if(saleType == 2)
        {
            resource += headphoneInterest * 100;
            HUD.addMessage("A hundred headphone is sold with interest of " + (headphoneInterest * 100).ToString("0.0") + "K");
        }

        HUD.updateResource(resource);
    }

    void updateSalesmansProduct()
    {
        foreach (SalesmanScript seller in allSalesMans)
        {
            seller.setProductScoreIfIsType(phonePS, 0);
            seller.setProductScoreIfIsType(laptopPS, 1);
            seller.setProductScoreIfIsType(headphonePS, 2);
        }
    }

    public void paySalesmanMonthlySalary()
    {
        int totalSalary = allSalesMans.Count;
        HUD.addMessage("Monthly payment for employers are: " + totalSalary + "K");
        resource -= totalSalary;
        HUD.updateResource(resource);
    }

    //JUST A METHOD FOR SALESMAN TO ACCESS HUD
    public void updateCustomerCount(int a, int b, int c, int d, int e, int f, int g)
    {
        HUD.updatePopulationCount(a,b,c,d,e,f,g);
    }

    //FOR COMMON DATA MANAGER TO ACCESS
    public void updateTimeHelperMethod(int month, int day, int year)
    {
        HUD.updateTimeDisplay(month, day, year);
    }
}
