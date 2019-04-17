using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Xml;

public class CsShop : MonoBehaviour
{

    public GameObject moneyText;

    public GameObject OBJ_Char;

    static public GameObject[] items;

    // Start is called before the first frame update
    void Start()
    {
        CsPlayerInformation.instance.JobLoad();

        items = new GameObject[CsPlayerInformation.instance.itemList.Count];

        for (int i=0;i< items.Length; i++)
        {
            items[i] = Instantiate(OBJ_Char, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

            items[i].transform.parent = this.transform;

            items[i].GetComponent<RectTransform>().localPosition = new Vector3(0, -300 * i - 100, 0);

            items[i].GetComponent<RectTransform>().localScale = OBJ_Char.GetComponent<RectTransform>().localScale;

            items[i].GetComponent<CsShopItem>().id = CsPlayerInformation.instance.itemList[i].id;

            items[i].GetComponent<CsShopItem>().name = CsPlayerInformation.instance.itemList[i].name;

            items[i].GetComponent<CsShopItem>().price = CsPlayerInformation.instance.itemList[i].price;

            items[i].GetComponent<CsShopItem>().isBuy = CsPlayerInformation.instance.itemList[i].isBuy;

            items[i].GetComponent<CsShopItem>().setItem();
        }

    }

    static public void SetItems()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].GetComponent<CsShopItem>().EditIsBuy();
        }
    }
    // Update is called once per frame
    void Update()
    {
        moneyText.GetComponentInChildren<Text>().text = "금액:" + CsPlayerInformation.instance.money;
    }
    
}
