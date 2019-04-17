using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CsWinPannel : MonoBehaviour
{

    Text text;
    public bool isWin = false;

    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        text = transform.Find("clear").gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
    public void GoNext()
    {
        if(isWin)
        {
            SceneManager.LoadScene("InGameScene1");
        }
        else
        {
            SceneManager.LoadScene("HanjaScene");
        }
    }

    public void setUp()
    {
        text = transform.Find("clear").gameObject.GetComponent<Text>();
        if (isWin)
        {
            text.text = "Clear"; 
        }
        else
        {
            text.text = "Fail";
            button.GetComponentInChildren<Text>().text = "Learn";
        }
    }

}
