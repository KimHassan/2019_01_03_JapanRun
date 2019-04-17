using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CsMojiButton2 : MonoBehaviour
{
    public string text;
    public bool isRight;
    public bool isClick = false;
    public bool isAttack = false;

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

    public void setButton()
    {

    }

    public void Click()
    {

       
        if (isClick == true) // 활성화에서 비활성화로
        {

            if (isAttack == true)
                return;

            Color c = this.GetComponent<Image>().color;

            c.a = 0.5f;

            this.GetComponent<Image>().color = c;

                this.gameObject.transform.parent.GetComponent<CsButtonControll2>().click -= 1;
            

            isClick = false;
        }
        else//비활성화에서 활성화로
        {

            if (this.gameObject.transform.parent.GetComponent<CsButtonControll2>().correctAnswer
                <= this.gameObject.transform.parent.GetComponent<CsButtonControll2>().click)
                return;

                this.gameObject.transform.parent.GetComponent<CsButtonControll2>().click++;

            Color c = this.GetComponent<Image>().color;

            c.a = 1.0f;

            this.GetComponent<Image>().color = c;

            isClick = true;

        }

    }

}
