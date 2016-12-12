using UnityEngine;
using System.Collections;

public class CitizenScript : MonoBehaviour {
    public Renderer phoneRenderer;
    public Texture phoneRedTexture;
    public Texture phoneBlueTexture;
    int phoneColor;
    int phonePS;

    public Renderer laptopRenderer;
    public Texture laptopRedTexture;
    public Texture laptopBlueTexture;
    int laptopColor;
    int laptopPS;

    public Renderer headphoneRenderer;
    public Texture headphoneRedTexture;
    public Texture headphoneBlueTexture;
    int headphoneColor;
    int headphonePS;

    bool isInProcessPhone;
    bool isInProcessLaptop;
    bool isInProcessHeadphone;
    // Use this for initialization
    void Start () {
        //-1 for neutral color, in this case, this citizen does not own a type of this product
        phoneColor = -1;
        laptopColor = -1;
        headphoneColor = -1;

        phonePS = 0;
        laptopPS = 0;
        headphonePS = 0;
        isInProcessPhone = false;
        isInProcessLaptop = false;
        isInProcessHeadphone = false;
    }

    public void setProductScore(int type, int score)
    {
        if(type == 0)
        {
            phonePS = score;
        }else if(type == 1)
        {
            laptopPS = score;
        }else if(type == 2)
        {
            headphonePS = score;
        }
    }

    public int getProductScore(int type)
    {
        if(type == 0)
        {
            return phonePS;
        }
        if (type == 1)
        {
            return laptopPS;
        }
        if(type == 2)
        {
            return headphonePS;
        }
        return -1;
    }

    public int getColorWithType(int type)
    {
        if(type == 0)
        {
            return phoneColor;
        }
        else if(type == 1)
        {
            return laptopColor;
        }
        else if(type == 2)
        {
            return headphoneColor;
        }else
        {
            //-2 for error, should never happen
            return -2;
        }
    }

    public void setInSession(int type, bool val)
    {
        if(type == 0)
        {
            isInProcessPhone = val;
        }else if(type == 1)
        {
            isInProcessLaptop = val;
        }else if(type == 2)
        {
            isInProcessHeadphone = val;
        }
    }

    public bool isTypeInSession(int type)
    {
        if (type == 0)
        {
            return isInProcessPhone;
        }
        else if (type == 1)
        {
            return isInProcessLaptop;
        }
        else if (type == 2)
        {
            return isInProcessHeadphone;
        }
        else
        {
            return false;
        }
    }

    public void changePhoneColor(int color)
    {
        //print("Change color to " + color);
        if (color == 0)
        {
            phoneRenderer.material.mainTexture = phoneRedTexture;
            phoneColor = 0;
        }else if(color == 1)
        {
            phoneRenderer.material.mainTexture = phoneBlueTexture;
            phoneColor = 1;
        }
    }

    public void changeLaptopColor(int color)
    {
        if (color == 0)
        {
            laptopRenderer.material.mainTexture = laptopRedTexture;
            laptopColor = 0;
        }
        else if (color == 1)
        {
            laptopRenderer.material.mainTexture = laptopBlueTexture;
            laptopColor = 1;
        }
    }

    public void changeHeadphoneColor(int color)
    {
        if (color == 0)
        {
            headphoneRenderer.material.mainTexture = headphoneRedTexture;
            laptopColor = 0;
        }
        else if (color == 1)
        {
            headphoneRenderer.material.mainTexture = headphoneBlueTexture;
            headphoneColor = 1;
        }
    }

    public bool willingToBuy(int type, int productScore)
    {
        if(type == 0)
        {
            if(productScore > phonePS)
            {
                int randomNumber = Random.Range(50, 100);
                if((productScore - phonePS) > randomNumber)
                {
                    return true;
                }
            }
        }
        if (type == 1)
        {
            if (productScore > laptopPS)
            {
                int randomNumber = Random.Range(50, 100);
                if ((productScore - laptopPS) > randomNumber)
                {
                    return true;
                }
            }
        }
        if (type == 2)
        {
            if (productScore > headphonePS)
            {
                int randomNumber = Random.Range(50, 100);
                if ((productScore - headphonePS) > randomNumber)
                {
                    return true;
                }
            }
        }
        return false;

    }

    // Update is called once per frame
    void Update () {
	
	}
}
