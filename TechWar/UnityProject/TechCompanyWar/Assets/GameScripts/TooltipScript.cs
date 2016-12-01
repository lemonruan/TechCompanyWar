using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TooltipScript : MonoBehaviour {

    public PlayerBaseScript playerBase;
    Text toolTipText;

    void Start()
    {
        toolTipText = GetComponent<Text>();
    }

	public void displayPhoneTechTooltip()
    {
        
        toolTipText.text = "Phone Tech Level Upgrade\n"
            + "Cost: " + playerBase.getPhoneTechCost() + "K";
    }

    public void displayPhoneDesignTooltip()
    {
        toolTipText.text = "Phone Design Level Upgrade\n"
            + "Cost: " + playerBase.getPhoneDesignCost()+ "K";
    }

    public void displayPhoneSaleToolTip()
    {
        toolTipText.text = "Phone Sale Agent taining\n"
            + "Cost: " + playerBase.getSaleTrainingCost() + "K";
    }

    public void displayLaptopTechTooltip()
    {

        toolTipText.text = "Laptop Tech Level Upgrade\n"
            + "Cost: " + playerBase.getLaptopTechCost() + "K";
    }

    public void displayLaptopDesignTooltip()
    {
        toolTipText.text = "Laptop Design Level Upgrade\n"
            + "Cost: " + playerBase.getLaptopDesignCost() + "K";
    }

    public void displayLaptopSaleToolTip()
    {
        toolTipText.text = "Laptop Sale Agent taining\n"
            + "Cost: " + playerBase.getSaleTrainingCost() + "K";
    }

    public void displayHeadphoneTechTooltip()
    {

        toolTipText.text = "Headphone Tech Level Upgrade\n"
            + "Cost: " + playerBase.getHeadphoneTechCost() + "K";
    }

    public void displayHeadphoneDesignTooltip()
    {
        toolTipText.text = "Headphone Design Level Upgrade\n"
            + "Cost: " + playerBase.getHeadphoneDesignCost() + "K";
    }

    public void displayHeadphoneSaleToolTip()
    {
        toolTipText.text = "Headphone Sale Agent taining\n"
            + "Cost: " + playerBase.getSaleTrainingCost() + "K";
    }

    public void clearTooltip()
    {
        toolTipText.text = "";
    }
}
