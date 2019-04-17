using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;

public class CsHanjaManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject GO_Hanja;

    GameObject[] HanjaList;


    void Start()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/Hanja");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNodeList all_nodes = xmlDoc.SelectNodes("dataroot/Moji");

        HanjaList = new GameObject[all_nodes.Count];
        for (int i = 0; i < all_nodes.Count; i++)
        {
            HanjaList[i] = Instantiate(GO_Hanja, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

            HanjaList[i].transform.parent = this.transform;

            HanjaList[i].name = "Hanja["+i+"]";
            HanjaList[i].GetComponent<RectTransform>().localScale = GO_Hanja.GetComponent<RectTransform>().localScale;

            HanjaList[i].GetComponent<RectTransform>().localPosition = new Vector3(6.5f, -20f + i * -240, 0);

            HanjaList[i].GetComponent<CsHanjaText>().setUp();

            
            HanjaList[i].GetComponent<CsHanjaText>().kanji.text = all_nodes[i].SelectSingleNode("kanji").InnerText;


            XmlNodeList meanTable = all_nodes[i].SelectNodes("mean");

            string meanStr = "";

            foreach (XmlNode means in meanTable)
            {
                meanStr += means.InnerText + "\n";

            }

            HanjaList[i].GetComponent<CsHanjaText>().mean.text = meanStr;

            XmlNodeList meanReadTable = all_nodes[i].SelectNodes("meanRead");

            string meanReadStr = "훈독 : ";

            foreach (XmlNode meanRead in meanReadTable)
            {
                meanReadStr += meanRead.InnerText + ",";

            }

            HanjaList[i].GetComponent<CsHanjaText>().meanRead.text = meanReadStr;

            XmlNodeList soundReadTable = all_nodes[i].SelectNodes("soundRead");

            string soundReadStr = "음독 : ";

            foreach (XmlNode soundRead in soundReadTable)
            {
                soundReadStr += soundRead.InnerText + ",";

            }

            HanjaList[i].GetComponent<CsHanjaText>().soundRead.text = soundReadStr;


        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
