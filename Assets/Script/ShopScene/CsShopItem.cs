using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class CsShopItem : MonoBehaviour
{

    public int id;

    public string name;

    public int price;

    public bool isBuy;

  

    Image image;
    Text text_name;
    
    public GameObject button;

    Text text_price;
    
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }
    
    public void Click()
    {
        Debug.Log("sex");

        Debug.Log(isBuy);

        if (CsPlayerInformation.instance.money < price)
            return;

        if (isBuy == false)
        {
            Debug.Log("sex");
            CsPlayerInformation.instance.money -= price;

            button.GetComponentInChildren<Text>().text = "장착";

            isBuy = true;

            CsPlayerInformation.instance.JobBuy(id);
        }
        if (isBuy == true)
        {
            CsPlayerInformation.instance.id = id;

            CsPlayerInformation.instance.name = name;



        }

        CsPlayerInformation.instance.PlayerSave();

        CsPlayerInformation.instance.JobSave();

        CsShop.SetItems();
    }

    public void EditIsBuy()
    {

        if (isBuy == true)
        {
            button.GetComponentInChildren<Text>().text = "장착";

            if (id == CsPlayerInformation.instance.id)
            {
                button.GetComponent<Image>().color = Color.red;
            }
            else
            {
                button.GetComponent<Image>().color = Color.white;
            }


        }
    }

    public void setItem()
    {
        image = transform.Find("icon").gameObject.GetComponent<Image>();

        text_name = transform.Find("name").gameObject.GetComponent<Text>();

        text_price = transform.Find("price").gameObject.GetComponent<Text>();


        
        switch (name)
        {
            case "knight":
                {
                    //image.color = Color.red;
                    image.sprite = Resources.Load<Sprite>("Sprite/Shop/knight") as Sprite;
                    text_name.text = "기사";
                    
                    
                    
                    break;
                }
            case "ninja":
                {
                    image.sprite = Resources.Load<Sprite>("Sprite/Shop/ninja") as Sprite;
                    text_name.text = "닌자";
                   
                    break;
                }
            case "wizard":
                {
                    image.sprite = Resources.Load<Sprite>("Sprite/Shop/wizard") as Sprite;
                    text_name.text = "마법사";
                   
                    break;
                }
        }

        text_price.text = price.ToString();

        if (isBuy == false)
        {
            button.GetComponentInChildren<Text>().text = "구매";
        }
        if (isBuy == true)
        {
            button.GetComponentInChildren<Text>().text = "장착";
            if (id == CsPlayerInformation.instance.id)
            {
                button.GetComponent<Image>().color = Color.red;
            }


        }
    }
}
