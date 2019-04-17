using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CsUiControll : MonoBehaviour
{
    static public CsUiControll instance;

    public GameObject stopButtonPanel;
    


    bool isClickStopButton = false;
    bool isStop = false;

    public float time;
     float maxTime = 5;
    

    public Slider timeBar;

    public GameObject player;
    public GameObject enemy;

     int score;

    public GameObject ItemInv;

    public GameObject winPannel;

    // Start is called before the first frame update
    private void Awake()
    {
        if (CsUiControll.instance == null)
            CsUiControll.instance = this;
    }
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStop == false)
        {


            time += Time.deltaTime;
            timeBar.value = (float)time / maxTime;
        }

        if(time >= maxTime)
        {
            //Debug.Log("fuck");


            player.GetComponent<CsPlayer>().Hp -= 1;
            player.GetComponent<CsPlayer>().Shake();
            CsSoundManager.instance.PlayDamagedSound();
            CsEffectManager.instance.setDamagedEffect(this.transform.position);

            if (player.GetComponent<CsPlayer>().Hp <= 0)
            {
                winPannel.SetActive(true);
                winPannel.GetComponent<CsWinPannel>().isWin = false;
                winPannel.GetComponent<CsWinPannel>().setUp();
                TimeStop();
            }

            time = 0;

        }

        ScoreTextUI();
        StepTextUI();
        Finish();
    }

    public void TimeStart()
    {

        isStop = false;

    }
    public void TimeStop()
    {
        isStop = true;
    }
    public void Damaged()
    {
        time += maxTime/5;

        
        timeBar.value = (float)time / maxTime;


    }
    public void InitTime()
    {
        time = 0;


       


    }

    public void PressStopButton()
    {
        if(isClickStopButton == false)
        {
            TimeStop();
            isClickStopButton = true;
            stopButtonPanel.SetActive(true);
        }
        else
        {
            TimeStart();
            isClickStopButton = false;
            stopButtonPanel.SetActive(false);
        }
        
        
    }
    public void PressResumeButton()
    {
        TimeStart();
        isClickStopButton = false;
        stopButtonPanel.SetActive(false);
    }

    public void PressTitleButton()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void ScoreTextUI()
    {
        
        transform.Find("stepText").gameObject.GetComponentInChildren<Text>().text =
            enemy.GetComponent<CsEnemy>().kanjiStepNum +"/"+ enemy.GetComponent<CsEnemy>().kanjiMax;

    }
    public void StepTextUI()
    {

        transform.Find("scoreText").gameObject.GetComponentInChildren<Text>().text = score.ToString();

    }
    public void ScoreUp()
    {
        int num = (int)((maxTime - time) * 100) * 10;
        score += num;
    }
    public void Finish()
    {
        if(enemy.GetComponent<CsEnemy>().kanjiStepNum > enemy.GetComponent<CsEnemy>().kanjiMax)
        {

            winPannel.SetActive(true);
            winPannel.GetComponent<CsWinPannel>().isWin = true;
            winPannel.GetComponent<CsWinPannel>().setUp();
            TimeStop();
        }
    }
}
