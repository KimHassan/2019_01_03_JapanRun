using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

using System.Xml;

public class CsButtonControll : MonoBehaviour
{
    public GameObject GO_Button;
    public GameObject enemy;
    public GameObject player;

    public GameObject[] buttons;

    public GameObject pressButton;



    int ButtonMax = 12;

    int meanMax = 0;

    int soundMax = 0;

    public int meanReadAnswer;
    public int soundReadAnswer;

    public int meanReadClickNum=0;
    public int soundReadClickNum=0;

    int isRight;

    int Enemy_ID;

    int buttonMaxCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        buttons = new GameObject[ButtonMax];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                buttons[i * 4 + j] = Instantiate(GO_Button, new Vector3(j * 1.2f, -i * 1) + transform.position, Quaternion.identity) as GameObject;
                buttons[i * 4 + j].transform.parent = this.transform;

                buttons[i * 4 + j].GetComponent<RectTransform>().localScale = GO_Button.GetComponent<RectTransform>().localScale;
                buttons[i * 4 + j].name = "[x=" + j + ",y=" + i + "]";
            }

        }
        SetButton();
    }

    // Update is called once per frame
    void Update()
    {
        pressButton.GetComponentInChildren<Text>().text = "훈독:" + meanReadClickNum + "/" + meanReadAnswer + "|" + "음독:"+soundReadClickNum + "/" + soundReadAnswer;
        

        
    }

   public void Attack()
    {
        int rightMax = 0;


        for (int i=0;i<buttonMaxCount-1;i++)
        {
            
            if((buttons[i].GetComponent<CsMojiButton>().isRight==true) && (buttons[i].GetComponent<CsMojiButton>().isClick == true))
            { // 답과 누른게 전부 맞을경우
                rightMax++;
                buttons[i].GetComponent<CsMojiButton>().isAttack = true;
               



            }

            if ((buttons[i].GetComponent<CsMojiButton>().isRight == true) && (buttons[i].GetComponent<CsMojiButton>().isClick == false))
            { // 답은 맞고 누른것은 틀릴경오 답이 아직 안누른게 있다

                player.GetComponent<CsPlayer>().Shake();
                CsSoundManager.instance.PlayWrongSound();
                Debug.Log("Wrong is" + i);
            }
            if ((buttons[i].GetComponent<CsMojiButton>().isRight == false) && (buttons[i].GetComponent<CsMojiButton>().isClick == true))
            { // 답이 틀리고 누른게 맞을 경우
                buttons[i].GetComponent<Image>().color = Color.red;
                Color c = buttons[i].GetComponent<Image>().color;
                c.a = 1.0f;
                buttons[i].GetComponent<Image>().color = c;

                CsUiControll.instance.Damaged();
                player.GetComponent<CsPlayer>().Shake();
                //CsSoundManager.instance.PlayWrongSound();


            }

        }

        if (rightMax == isRight) // 모든게 정답
        {
            Debug.Log("sex");
            
           
            enemy.GetComponent<CsEnemy>().Dead();
            CsUiControll.instance.ScoreUp();
            CsUiControll.instance.InitTime();

            player.GetComponent<CsPlayer>().Attack();
            

        }
        else
        {

            Debug.Log(rightMax + "/" + isRight);

        }


    }

    public void SetButton()
    {

        isRight = 0;

        for (int i=0 ; i < ButtonMax; i++)
        {
            buttons[i].GetComponent<CsMojiButton>().isAttack = false;
            buttons[i].GetComponent<CsMojiButton>().isClick = false;
            buttons[i].SetActive(true);
        }

        TextAsset textAsset = (TextAsset)Resources.Load("XML/" + "Hanja");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList all_nodes = xmlDoc.SelectNodes("dataroot/Moji");
        Enemy_ID = enemy.GetComponent<CsEnemy>().id;
        int Enemy_ID2;
        int Enemy_ID3;
        if (Enemy_ID <= 2)
        {
            Enemy_ID2 = Random.Range(Enemy_ID + 1, Enemy_ID + 2);
            Enemy_ID3 = Random.Range(Enemy_ID + 3, Enemy_ID + 5);
        }
        else if (Enemy_ID >= all_nodes.Count - 2)
        {
            Enemy_ID2 = Random.Range(all_nodes.Count - 4, all_nodes.Count - 3);
            Enemy_ID3 = Random.Range(all_nodes.Count - 6, all_nodes.Count - 5);
        }
        else
        {
            Enemy_ID2 = Random.Range(Enemy_ID - 3, Enemy_ID - 1);
            Enemy_ID3 = Random.Range(Enemy_ID + 1, Enemy_ID + 3);
        }
        //훈음
        XmlNodeList meanReadTable1 = all_nodes[Enemy_ID].SelectNodes("meanRead");
        XmlNodeList meanReadTable2 = all_nodes[Enemy_ID2].SelectNodes("meanRead");
        XmlNodeList meanReadTable3 = all_nodes[Enemy_ID3].SelectNodes("meanRead");

        XmlNodeList soundReadTable1 = all_nodes[Enemy_ID].SelectNodes("soundRead");
        XmlNodeList soundReadTable2 = all_nodes[Enemy_ID2].SelectNodes("soundRead");
        XmlNodeList soundReadTable3 = all_nodes[Enemy_ID3].SelectNodes("soundRead");


        meanMax = meanReadTable1.Count + meanReadTable2.Count + meanReadTable3.Count;

        soundMax = soundReadTable1.Count + soundReadTable2.Count + soundReadTable3.Count;

        buttonMaxCount = 0;

        if (meanReadTable1.Count == 0)
        {
        }
        else
        {

            foreach (XmlNode MRT in meanReadTable1)
            {
                buttons[buttonMaxCount].GetComponent<CsMojiButton>().isRight = true;
                buttons[buttonMaxCount].GetComponent<CsMojiButton>().text = MRT.InnerText;
                buttons[buttonMaxCount].GetComponent<CsMojiButton>().state = 1;

                buttons[buttonMaxCount].GetComponent<Image>().color = Color.green;

                buttonMaxCount++;
                isRight++;

            }

            foreach (XmlNode MRT in meanReadTable2)
            {
                buttons[buttonMaxCount].GetComponent<CsMojiButton>().isRight = false;
                buttons[buttonMaxCount].GetComponent<CsMojiButton>().text = MRT.InnerText;
                buttons[buttonMaxCount].GetComponent<CsMojiButton>().state = 1;

                buttons[buttonMaxCount].GetComponent<Image>().color = Color.green;
                buttonMaxCount++;

            }

            foreach (XmlNode MRT in meanReadTable3)
            {
                buttons[buttonMaxCount].GetComponent<CsMojiButton>().isRight = false;
                buttons[buttonMaxCount].GetComponent<CsMojiButton>().text = MRT.InnerText;
                buttons[buttonMaxCount].GetComponent<CsMojiButton>().state = 1;

                buttons[buttonMaxCount].GetComponent<Image>().color = Color.green;
                buttonMaxCount++;

            }
        }

        if(soundReadTable1.Count == 0)
        {

        }
        else
        {

        
        foreach (XmlNode SRT in soundReadTable1)
        {
            buttons[buttonMaxCount].GetComponent<CsMojiButton>().isRight = true;
            buttons[buttonMaxCount].GetComponent<CsMojiButton>().text = SRT.InnerText;
            buttons[buttonMaxCount].GetComponent<CsMojiButton>().state = 2;
            buttons[buttonMaxCount].GetComponent<Image>().color = Color.yellow;
            buttonMaxCount++;
            isRight++;

        }

        foreach (XmlNode SRT in soundReadTable2)
        {
            buttons[buttonMaxCount].GetComponent<CsMojiButton>().isRight = false;
            buttons[buttonMaxCount].GetComponent<CsMojiButton>().text = SRT.InnerText;
            buttons[buttonMaxCount].GetComponent<CsMojiButton>().state = 2;

            buttons[buttonMaxCount].GetComponent<Image>().color = Color.yellow;
            buttonMaxCount++;

        }

        foreach (XmlNode SRT in soundReadTable3)
        {
            if (buttonMaxCount >= ButtonMax)
                break;
            buttons[buttonMaxCount].GetComponent<CsMojiButton>().isRight = false;
            buttons[buttonMaxCount].GetComponent<CsMojiButton>().text = SRT.InnerText;
            buttons[buttonMaxCount].GetComponent<CsMojiButton>().state = 2;

            buttons[buttonMaxCount].GetComponent<Image>().color = Color.yellow;
            buttonMaxCount++;

        }
        }


        meanReadClickNum = 0;
        soundReadClickNum = 0;
        meanReadAnswer = meanReadTable1.Count;
        soundReadAnswer = soundReadTable1.Count;


        Debug.Log(buttonMaxCount);

        for (int i=0; i < buttonMaxCount; i++)
        {
            Color c = buttons[i].GetComponent<Image>().color;
            c.a = 0.5f;
            buttons[i].GetComponent<Image>().color = c;
        }

        if (buttonMaxCount >= ButtonMax)
            return;
        
        for(int num = buttonMaxCount; num < ButtonMax;num++)
        {
            buttons[num].SetActive(false);
            
        }


    }

}
