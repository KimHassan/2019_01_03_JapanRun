using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using System.Xml;



public class CsXML : MonoBehaviour
{
    public GameObject ButtonControll;
        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


   public void LoadXML(string _fileName)
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/"+_fileName);
        Debug.Log(textAsset);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        int i = 0;
        //하나씩 가져오기 테스트 예제
        
        XmlNodeList kanji_table = xmlDoc.GetElementsByTagName("kanji");
        
        foreach (XmlNode kanji in kanji_table)
        {
            ButtonControll.GetComponent<CsButtonControll>().buttons[i].GetComponent<CsMojiButton>().text = kanji.InnerText;
            Debug.Log("[one bt one] kanji : " + kanji.InnerText);
        }
        //XmlNodeList cost_Table = xmlDoc.GetElementsByTagName("cost");
        //foreach(XmlNode cost in cost_Table)
        //{
        //    Debug.Log("[one bt one] cost : " + cost.InnerText);
        //}

        ////전체아이템 가져오기
        //XmlNodeList all_nodes = xmlDoc.SelectNodes("dataroot/TextItem");
        //foreach(XmlNode node in all_nodes)
        //{
        //    Debug.Log("[at once] id :" + node.SelectSingleNode("id").InnerText);
        //    Debug.Log("[at once] name :" + node.SelectSingleNode("name").InnerText);
        //    Debug.Log("[at once] cost :" + node.SelectSingleNode("cost").InnerText);
        //}


    }

}
