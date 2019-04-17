using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

using System.Xml;

public class CsButtonControll2 : MonoBehaviour
{
    public GameObject GO_Button;

    public GameObject enemy;

    public GameObject player;

    public GameObject[] buttons;

    public GameObject pressButton;


    int ButtonMax = 6;


    public int correctAnswer = 0;


    public int click = 0;

    int Enemy_ID;

    int[] falseAnswer = new int[5];
    private void Start()
    {
        buttons = new GameObject[ButtonMax];
        for(int i=0;i<2;i++)
        {
            for(int j=0;j<3;j++)
            {
                buttons[i * 3 + j] = Instantiate(GO_Button, new Vector3(j * 1.8f, -i * 1.2f - 0.4f) + transform.position, Quaternion.identity);
                buttons[i * 3 + j].transform.parent = this.transform;

                buttons[i * 3 + j].GetComponent<RectTransform>().localScale = GO_Button.GetComponent<RectTransform>().localScale;
                buttons[i * 3 + j].name = "[x=" + j + ",y=" + i + "]";
            }
        }
        SetButton();

    }

    private void Update()
    {
        pressButton.GetComponentInChildren<Text>().text = "답:" + click + "/" + correctAnswer;

    }

    public void Attack()
    {
        int rightMax = 0;


        for (int i = 0; i < 6; i++)
        {

            if ((buttons[i].GetComponent<CsMojiButton2>().isRight == true) && (buttons[i].GetComponent<CsMojiButton2>().isClick == true))
            { // 답과 누른게 전부 맞을경우
                rightMax++;
                buttons[i].GetComponent<CsMojiButton2>().isAttack = true;




            }

            if ((buttons[i].GetComponent<CsMojiButton2>().isRight == true) && (buttons[i].GetComponent<CsMojiButton2>().isClick == false))
            { // 답은 맞고 누른것은 틀릴경오 답이 아직 안누른게 있다

                player.GetComponent<CsPlayer>().Shake();
                CsSoundManager.instance.PlayWrongSound();
                Debug.Log("Wrong is" + i);
            }
            if ((buttons[i].GetComponent<CsMojiButton2>().isRight == false) && (buttons[i].GetComponent<CsMojiButton2>().isClick == true))
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

        if (rightMax == correctAnswer) // 모든게 정답
        {
            Debug.Log("sex");


            enemy.GetComponent<CsEnemy>().Dead();

            CsUiControll.instance.ScoreUp();

            CsUiControll.instance.InitTime();

            player.GetComponent<CsPlayer>().Attack();


        }


    }

    public void SetButton()
    {
        click = 0;
        for (int i = 0; i < ButtonMax; i++)
        {
            buttons[i].GetComponent<CsMojiButton2>().isAttack = false;
            buttons[i].GetComponent<CsMojiButton2>().isClick = false;
        }



        TextAsset textAsset = (TextAsset)Resources.Load("XML/" + "Hanja");

        XmlDocument xmlDoc = new XmlDocument();

        xmlDoc.LoadXml(textAsset.text);

        XmlNodeList all_nodes = xmlDoc.SelectNodes("dataroot/Moji");

        Enemy_ID = enemy.GetComponent<CsEnemy>().id;


        XmlNodeList answerTables = all_nodes[Enemy_ID].SelectNodes("mean");


        correctAnswer = answerTables.Count;
        for(int i=0;i< correctAnswer; i++)
        {
            buttons[i].GetComponent<CsMojiButton2>().isRight = true;

            buttons[i].GetComponent<CsMojiButton2>().text = answerTables[i].InnerText;

        }

        int rand = Random.Range(1, 6);
        
        for(int i= correctAnswer; i<6; i++)
        {
            XmlNode falseAnswerTable;
            if (Enemy_ID > all_nodes.Count - 5 - rand)
            {
                falseAnswerTable = all_nodes[Enemy_ID - rand - i - correctAnswer].SelectSingleNode("mean");
            }
            else
            {
                falseAnswerTable = all_nodes[Enemy_ID + rand + i - correctAnswer].SelectSingleNode("mean");
            }
            buttons[i].GetComponent<CsMojiButton2>().isRight = false;

            buttons[i].GetComponent<CsMojiButton2>().text = falseAnswerTable.InnerText;



        }

        for (int i = 0; i < ButtonMax; i++)
        {
            
            Color c = buttons[i].GetComponent<Image>().color;
            c = Color.white;
            c.a = 0.5f;
            buttons[i].GetComponent<Image>().color = c;
        }








    }




}
