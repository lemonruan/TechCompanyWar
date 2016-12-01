using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SalesmanScript : MonoBehaviour {

    public Renderer ProductTypeLogoRenderer;
    public Texture phoneLogo;
    public Texture laptopLogo;
    public Texture headphoneLogo;

    //ATM, 0 for red , 1 for blue
    int CompanyColor;
    
    //0 for phone, 1 for laptop, 2 for headphone
    int saleType;
    int productScore;
    float convinceTime;
    float timeLeft;

    //FOR AI
    bool needATarget;

    selectable selectableSelf;
    CitizenScript targetCitizen;

    CommonDataManager commonData;
    
    //AI BASE WILL BE ARRAY FOR MORE THAN ONE AI, PLAYER BASE WILL BE ARRAY IF COMES TO MULTIPLAYER
    PlayerBaseScript playerBase;
    AIBaseScript aiBase;

    ParticleSystem ConvinceEffect;

    //FOR AUTO FIND, NOT GOING FOR THE SAME TARGETS
    List<CitizenScript> unwillingBuyers;

	// Use this for initialization
	void Start () {
        timeLeft = 0;
        convinceTime = 5;
        selectableSelf = GetComponent<selectable>();
        commonData = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CommonDataManager>();
        playerBase = GameObject.FindGameObjectWithTag("PlayerBaseTag").GetComponent<PlayerBaseScript>();
        aiBase = GameObject.FindGameObjectWithTag("AIBaseTag").GetComponent<AIBaseScript>();
        print("AICOLOR: "+aiBase.getCompanyColor());
        needATarget = true;
        ConvinceEffect = GetComponentsInChildren<ParticleSystem>()[0];
        ConvinceEffect.Stop();
        unwillingBuyers = new List<CitizenScript>();
    }

    public void setProductScoreIfIsType(int score, int type)
    {
        if(type == saleType)
        {
            productScore = score;
        }
    }

    public void setSaleType(int type, int color, int score)
    {
        saleType = type;
        CompanyColor = color;
        if (type == 0) {
            ProductTypeLogoRenderer.material.mainTexture = phoneLogo;
        }else if(type == 1)
        {
            ProductTypeLogoRenderer.material.mainTexture = laptopLogo;
        }else if(type == 2)
        {
            ProductTypeLogoRenderer.material.mainTexture = headphoneLogo;
        }

        productScore = score;
    }

    public void autoFindClosest()
    {
        GameObject autoTarget = commonData.getClosestCitizenWithDifferentColor(this.transform, saleType, CompanyColor, unwillingBuyers);
         if (autoTarget != null)
         {
            selectableSelf.Move(autoTarget.transform);
         }
    }

    public void convince(CitizenScript citizen)
    {
        //Called when touches the target
        if (citizen.getColorWithType(saleType) != CompanyColor)
        {
            timeLeft = convinceTime;
            targetCitizen = citizen;
            selectableSelf.setMovable(false);
            ConvinceEffect.Play();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (CompanyColor == commonData.AIcolor && needATarget)
        {
            print("Try find a target");
            //IF AI, INITIALIZE THE MOVEMENT AND TRIGGER AUTOFIND
            GameObject autoTarget = commonData.getClosestCitizenWithDifferentColor(this.transform, saleType, CompanyColor, unwillingBuyers);
            if (autoTarget != null)
            {
                print("Found target");
                selectableSelf.Move(autoTarget.transform);
            }
            needATarget = false;
        }

        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else
        {
            if(targetCitizen != null)
            {
                if (SellSuccess(targetCitizen, saleType))
                {
                    if (saleType == 0)
                    {
                        targetCitizen.changePhoneColor(CompanyColor);

                        if (playerBase.getCompanyColor() == CompanyColor)
                        {
                            //0 FOR PHONE, 1 FOR LAPTOP, 2 FOR HEADPHONE
                            playerBase.addToResource(0);
                        }
                        if (aiBase.getCompanyColor() == CompanyColor)
                        {
                            aiBase.addToResource(0);
                        }
                    }
                    else if (saleType == 1)
                    {
                        targetCitizen.changeLaptopColor(CompanyColor);

                        if (playerBase.getCompanyColor() == CompanyColor)
                        {
                            playerBase.addToResource(1);
                        }
                        if (aiBase.getCompanyColor() == CompanyColor)
                        {
                            aiBase.addToResource(1);
                        }
                    }
                    else if (saleType == 2)
                    {
                        targetCitizen.changeHeadphoneColor(CompanyColor);

                        if (playerBase.getCompanyColor() == CompanyColor)
                        {
                            playerBase.addToResource(2);
                        }
                        if (aiBase.getCompanyColor() == CompanyColor)
                        {
                            aiBase.addToResource(2);
                        }
                    }

                    targetCitizen.setProductScore(saleType, productScore);
                    //USING PLAYER BASE TO UPDATE THE CUSTOMER COUNT
                    playerBase.updateCustomerCount(commonData.getPlayerProductSellStat(0), commonData.getAIProductSellStat(0),
                        commonData.getPlayerProductSellStat(1), commonData.getAIProductSellStat(1),
                        commonData.getPlayerProductSellStat(2), commonData.getAIProductSellStat(2),
                        commonData.getTotalPopulation());
                }
                else
                {
                    unwillingBuyers.Add(targetCitizen);
                }
                selectableSelf.setMovable(true);
                targetCitizen = null;

                //after finishing convincing one, and autofind a target

                GameObject autoTarget = commonData.getClosestCitizenWithDifferentColor(this.transform, saleType, CompanyColor, unwillingBuyers);
                if (autoTarget != null)
                {
                    selectableSelf.Move(autoTarget.transform);
                }

                ConvinceEffect.Stop();
            }
                
        }
	}

    bool SellSuccess(CitizenScript targetCitizen, int type)
    {
        if (targetCitizen.getColorWithType(type) == -1)
        {
            return true;
        }
        if (type == 0)
        {
            if(targetCitizen.getColorWithType(0) == commonData.AIcolor)
            {
                return targetCitizen.willingToBuy(0, productScore);
            }
        }
        if (type == 1)
        {
            if (targetCitizen.getColorWithType(1) == commonData.AIcolor)
            {
                return targetCitizen.willingToBuy(1, productScore);
            }
        }
        if (type == 2)
        {
            if (targetCitizen.getColorWithType(2) == commonData.AIcolor)
            {
                return targetCitizen.willingToBuy(2, productScore);
            }
        }
        return false;
    }
}
