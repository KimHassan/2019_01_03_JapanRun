using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

using System.Xml;
using UnityEngine.SceneManagement;

public class CsEnemy : MonoBehaviour
{

    public int id;
    public string kanji;

    public string[] mean;
    public string[] meanRead;
    public string[] soundRead;

    public bool isDead = false;
    public GameObject Obj_Character;

    bool isRespawn = false;
    Image Character;
    
    public GameObject ButtonControll;

    int[] kanjiStep;
    public int kanjiStepNum = 0;
    public int kanjiMax;
    Text text;

    Vector3 originPos;



    // Start is called before the first frame update

    private void Awake()
    {
        Character = Obj_Character.GetComponent<Image>();
        setStep();
        setEnemy();
        originPos = Obj_Character.GetComponent<RectTransform>().localPosition;
    }
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        DeadUpdate();
        RespawnUpdate();



    }

    void setStep()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/" + "Hanja");

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);


        XmlNodeList all_nodes = xmlDoc.SelectNodes("dataroot/Moji");
        kanjiMax = all_nodes.Count;
        kanjiStep = new int[kanjiMax];
        
        for(int i=0;i< kanjiMax; i++)
        {
            kanjiStep[i] = i;
        }
        
        for (int i=0;i< kanjiMax / 2; i++)
        {
            int ranIdx = Random.Range(i, kanjiMax);



            int tmp = kanjiStep[ranIdx];

            kanjiStep[ranIdx] = kanjiStep[i];

            kanjiStep[i] = tmp;
        }

    }
    void setEnemy()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/" + "Hanja");
       
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        

        XmlNodeList all_nodes = xmlDoc.SelectNodes("dataroot/Moji");

        id = kanjiStep[kanjiStepNum];
        kanjiStepNum++;

        if (kanjiStepNum > kanjiMax)
            return;
        
        kanji = all_nodes[id].SelectSingleNode("kanji").InnerText;
        text = this.GetComponentInChildren<Text>();

        text.text = kanji;

        Debug.Log(id);

        XmlNodeList meanTable = all_nodes[id].SelectNodes("mean");
        mean = new string[meanTable.Count];
        int i = 0;

        foreach (XmlNode means in meanTable)
        {
            mean[i] = means.InnerText;
            Debug.Log(means.InnerText);
            i++;
        }


        XmlNodeList meanReadTable = all_nodes[id].SelectNodes("meanRead");
        meanRead = new string[meanReadTable.Count];
        i = 0;
        foreach (XmlNode meanReads in meanReadTable)
        {
            meanRead[i] = meanReads.InnerText;
            Debug.Log(meanReads.InnerText);
            i++;
        }

        XmlNodeList soundReadTable = all_nodes[id].SelectNodes("soundRead");
        soundRead = new string[soundReadTable.Count];
        i = 0;
        foreach(XmlNode soundReads in soundReadTable)
        {
            soundRead[i] = soundReads.InnerText;
            Debug.Log(soundReads.InnerText);
            i++;
        }
       
    }

    void RespawnUpdate()
    {
        if (isRespawn == true)
        {
            Vector3 destination = originPos;
            Obj_Character.GetComponent<RectTransform>().localPosition =
                Vector3.MoveTowards(Obj_Character.GetComponent<RectTransform>().localPosition,
                destination, 500.0f * Time.deltaTime);

            if (Obj_Character.GetComponent<RectTransform>().localPosition == destination)
            {
                isRespawn = false;
            }
        }
    }
    public void Dead()
    {
        isDead = true;
        Color cb = Color.black;
        Character.color = cb;

    }
    
    void DeadUpdate()
    {
        if (isDead == true)
        {
            Color cb = Character.color;
            cb.a -= (float)Time.deltaTime * 1f;

            Character.color = cb;
            if (cb.a < 0)
            {
                isDead = false;

                Color ck = Character.color;
                ck = Color.white;
                Character.color = ck;

                Obj_Character.GetComponent<RectTransform>().localPosition = new Vector3(876, 137, 0);

                int rand = Random.Range(1, 5);
                Character.sprite = Resources.Load<Sprite>("Sprite/enemy/Enemy_" + rand.ToString()) as Sprite;
                isRespawn = true;
                setEnemy();
                

                if (SceneManager.GetActiveScene().name == "InGameScene1")
                    ButtonControll.GetComponent<CsButtonControll>().SetButton();

                if (SceneManager.GetActiveScene().name == "InGameScene2")
                    ButtonControll.GetComponent<CsButtonControll2>().SetButton();

            }
        }
    }

    string getMoji() { return kanji; }
    string[] getMean() { return mean; }
    string[] getMeanRead() { return meanRead; }
    string[] getSoundRead() { return soundRead; }
}
