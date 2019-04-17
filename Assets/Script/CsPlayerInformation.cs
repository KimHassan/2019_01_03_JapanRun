using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using System;

public class Item
{
    public int id;
    public string name;
    public int price;
    public bool isBuy;

    public Item(int _id,string _name,int _price,bool _isBuy)
    {
        id = _id;
        name = _name;
        price = _price;
        isBuy = _isBuy;
    }
}

public class PlayerInfo
{
    public int id;
    public string name;
    public int money;

    public PlayerInfo(int _id, string _name,int _money)
    {
        id = _id;
        name = _name;
        money = _money;
    }
}

public class CsPlayerInformation : MonoBehaviour
{
    public static CsPlayerInformation instance;

    public PlayerInfo playerInfo;

    public int id;

    public string name;

    public int money;

    string path;

    public List<Item> itemList = new List<Item>();

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
            instance = this;

        path = Application.dataPath + "/Resources/Json/";
        PlayerLoad();


    }

    void Start()
    {
     
}

    // Update is called once per frame
    void Update()
    {
        
    }

     
    public void PlayerSave(int nowId,string nowName,int nowMoney)
    {
        playerInfo = new PlayerInfo(nowId, nowName, nowMoney);

        JsonData playerjob = JsonMapper.ToJson(playerInfo);

        File.WriteAllText(path + "PlayerInfomation.json", playerjob.ToString());
    }

    public void PlayerSave()
    {
        playerInfo = new PlayerInfo(id,name,money);

        JsonData playerjob = JsonMapper.ToJson(playerInfo);

        File.WriteAllText(path + "PlayerInfomation.json", playerjob.ToString());
    }


    public void PlayerLoad()
    {

        string Jsonstring = File.ReadAllText(path + "PlayerInfomation.json");

        JsonData playerjob = JsonMapper.ToObject(Jsonstring);

        id = Convert.ToInt32(playerjob["id"].ToString());

        name = playerjob["name"].ToString();

        money = Convert.ToInt32(playerjob["money"].ToString());


    }

    public void JobEdit()
    {

        JsonData itemJson = JsonMapper.ToJson(itemList);

        File.WriteAllText(path + "ItemData.json", itemJson.ToString());

    }

    public void JobSave()
    {

        JsonData itemJson = JsonMapper.ToJson(itemList);

        File.WriteAllText(path + "ItemData.json", itemJson.ToString());

    }

    public void JobReset()
    {
        itemList.Add(new Item(0, "knight", 20000, true));

        itemList.Add(new Item(1, "ninja", 30000, false));

        itemList.Add(new Item(2, "wizard", 50000, false));

        JsonData itemJson = JsonMapper.ToJson(itemList);

        File.WriteAllText(path + "ItemData.json", itemJson.ToString());

    }

    public void JobLoad()
    {

        string Jsonstring = File.ReadAllText(path + "ItemData.json");

        JsonData itemJson = JsonMapper.ToObject(Jsonstring);

        for (int i=0;i< itemJson.Count;i++)
        {
            itemList.Add(
                        new Item(
                                    Convert.ToInt32(itemJson[i]["id"].ToString()),
                                    itemJson[i]["name"].ToString(),
                                    Convert.ToInt32(itemJson[i]["price"].ToString()),
                                    Convert.ToBoolean(itemJson[i]["isBuy"].ToString())
                                )
                        );
        }

    }

    public void JobBuy(int _num)
    {
        itemList[_num].isBuy = true;
    }
}
