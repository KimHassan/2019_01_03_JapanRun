using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class CsMojiButton : MonoBehaviour
{
    enum STATE
    {
        mean = 0,
        meanRead = 1,
        soundRead = 2
    }

    public string text;
    public bool isRight;
    public bool isClick = false;
    public bool isAttack = false;
    //public int state;

    public int state;



    // Start is called before the first frame update
    void Start()
    {
        this.GetComponentInChildren<Text>().text = text;
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponentInChildren<Text>().text = text;


    }

    public void Click()
    {
        

        ColorBlock cb = this.GetComponent<Button>().colors;

        if (isClick == true) // 활성화에서 비활성화로
        {

            if (isAttack == true)
                return;

            Color c = this.GetComponent<Image>().color;
            c.a = 0.5f;
            this.GetComponent<Image>().color = c;
            if(state == 1)
            {
                this.gameObject.transform.parent.GetComponent<CsButtonControll>().meanReadClickNum -= 1;
            }
            else if(state == 2)
            {
                this.gameObject.transform.parent.GetComponent<CsButtonControll>().soundReadClickNum -=1;
            }
            
           isClick = false;
        }
        else//비활성화에서 활성화로
        {
           
            if(state == 1)
            {
                if (this.gameObject.transform.parent.GetComponent<CsButtonControll>().meanReadClickNum
                    >= this.gameObject.transform.parent.GetComponent<CsButtonControll>().meanReadAnswer)
                {
                    return;
                }

                this.gameObject.transform.parent.GetComponent<CsButtonControll>().meanReadClickNum++;
            }

            else if(state == 2)
            {
                if (this.gameObject.transform.parent.GetComponent<CsButtonControll>().soundReadClickNum
                    >= this.gameObject.transform.parent.GetComponent<CsButtonControll>().soundReadAnswer)
                {
                    return;
                }

                this.gameObject.transform.parent.GetComponent<CsButtonControll>().soundReadClickNum++;
            }


            Color c = this.GetComponent<Image>().color;
            c.a = 1.0f;
            this.GetComponent<Image>().color = c;
            isClick = true;
        }
        

        this.GetComponent<Button>().colors = cb;
    }
}
