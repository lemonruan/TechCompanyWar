using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDScript : MonoBehaviour {
    public Slider progressBar;
    public Transform Buttons;
    

    Button[] buttonArray;
    Button ButtonInProgress;

    public Sprite LogoSprite;
    public Image Logo;


    
    public Text Timer;
    public Text ResourceText;
    public Text LoanText;
    public Text PhoneScore;
    public Text LaptopScore;
    public Text headphoneScore;

    //THESE ARE MESSAGES THAT CHANGE
    public Text text0;
    public Text text1;
    public Text text2;
    public Text text3;
    public Text text4;

    //THESE ARE PRODUCT NUMBERS
    public Text playerPhoneNumber;
    public Text enemyPhoneNumber;
    public Text playerLaptopNumber;
    public Text enemyLaptopNumber;
    public Text playerHeadphoneNumber;
    public Text enemyHeadphoneNumber;


    // Use this for initialization
    void Start () {
        buttonArray = Buttons.GetComponentsInChildren<Button>(true);

        progressBar.gameObject.SetActive(false);
        
	}

    public void StartAProgress(Button bInp)
    {
        print("Start Progress");
        ButtonInProgress = bInp;
        ButtonInProgress.interactable = false;
        progressBar.gameObject.SetActive(true);
        progressBar.value = 0;
        Logo.sprite = ButtonInProgress.image.sprite;
    }

    public void UpdateProgress(int perc)
    {
        progressBar.value = perc;
    }

    public void finishProgress()
    {
        ButtonInProgress.interactable = true;
        progressBar.gameObject.SetActive(false);
        Logo.sprite = LogoSprite;
    }
	
	// Update is called once per frame
	void Update () {
        

    }

    public void addMessage(string message)
    {
        text4.text = text3.text;
        text3.text = text2.text;
        text2.text = text1.text;
        text1.text = text0.text;
        text0.text = message;
    }

    public void updateTimeDisplay(int month, int day, int year)
    {
        Timer.text = "Month: " + month + " Day: " + day + " Year: " + year;
    }


    public void updatePhoneScore(int score)
    {
        PhoneScore.text = "Phone PS: " + score;
    }

    public void updateLaptopScore(int score)
    {
        LaptopScore.text = "Laptop PS: " + score;
    }

    public void updateHeadphoneScore(int score)
    {
        headphoneScore.text = "Headphone PS: " + score;
    }

    public void updateResource(float resource)
    {
        ResourceText.text = "Resource($): " + resource.ToString("0.0") + "K";
    }

    public void updateLoan(float loan)
    {
        LoanText.text = "Bank Loan Due($): " + loan.ToString("0.0") + "K";
    }


    public void updatePopulationCount(int playerPhone, int enemyPhone, 
        int playerLaptop, int enemyLaptop,
        int playerHeadphone, int enemyHeadphone,
        int totalPopulation)
    {
        playerPhoneNumber.text = playerPhone.ToString() + "/" + totalPopulation.ToString();
        enemyPhoneNumber.text = enemyPhone.ToString() + "/" + totalPopulation.ToString();
        playerLaptopNumber.text = playerLaptop.ToString() + "/" + totalPopulation.ToString();
        enemyLaptopNumber.text = enemyLaptop.ToString() + "/" + totalPopulation.ToString();
        playerHeadphoneNumber.text = playerHeadphone.ToString() + "/" + totalPopulation.ToString();
        enemyHeadphoneNumber.text = enemyHeadphone.ToString() + "/" + totalPopulation.ToString();
    }
}
