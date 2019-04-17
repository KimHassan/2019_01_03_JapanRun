using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class CsPlayer : MonoBehaviour
{
    
    public int maxHp = 10;
    public int Hp = 10;


 
    Vector3 originPos;

    RectTransform myTransform;

    public GameObject text;

    public GameObject Character;


    //흔들기
    bool isShake = false;

    Vector3[] shakePostion = new Vector3[2];

    Vector3 shakeDestination;

    float shakeSpeed = 150;

    int shakeCount = 0;

    int shakeLastCount = 2;


    //공격관련
    bool isAttack = false;

    int attackStep = 0;

    Vector3 attackPosition = new Vector3(-300, 110, 0);

    float attackSpeed = 200;



    // Start is called before the first frame update
    void Start()
    {
       
        Hp = maxHp;
        myTransform = Character.GetComponent<RectTransform>();
        originPos = myTransform.localPosition;

        shakePostion[0] = myTransform.localPosition;
        shakePostion[0].x = myTransform.localPosition.x - 20;

        shakePostion[1] = myTransform.localPosition;
        shakePostion[1].x = myTransform.localPosition.x + 20;

        shakeDestination = shakePostion[0];

        setPlayerJob();
    }

    void setPlayerJob()
    {
        string jobName = CsPlayerInformation.instance.name;
        Character.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/player/" + jobName);

    }

    // Update is called once per frame
    void Update()
    {
        text.GetComponent<Text>().text = Hp + "/" + maxHp;
        ShakeUpdate();
        AttackUpdate();
    }
    public void Attack()
    {
        if (isShake == true)
            isShake = false;
          isAttack = true;
          myTransform.localPosition = originPos;
          attackStep = 0;
        CsEffectManager.instance.setAttackEffect(this.transform.position);
        CsSoundManager.instance.PlayAttackSound();

    }

    void AttackUpdate()
    {

        if (isAttack == true)
        {
            if (attackStep == 0)
            {
                myTransform.localPosition = Vector3.MoveTowards(myTransform.localPosition, attackPosition, attackSpeed * Time.deltaTime);
                if (myTransform.localPosition == attackPosition)
                {
                    attackStep = 1;
                }
            }
            else if (attackStep == 1)
            {
                myTransform.localPosition = Vector3.MoveTowards(myTransform.localPosition, originPos, attackSpeed * Time.deltaTime);
                if (myTransform.localPosition == originPos)
                {
                    isAttack = false;
                }
            }
        }
    }

   public void Shake()
    {
        if (isAttack == true)
            return;
        if (isShake == true)
            return;
        isShake = true;
        shakeCount = 0;
        shakeDestination = shakeDestination = shakePostion[0];
    }
    void ShakeUpdate()
    {
        if(isShake == true)
        {

            myTransform.localPosition = Vector3.MoveTowards(myTransform.localPosition, shakeDestination, shakeSpeed * Time.deltaTime);
            if(myTransform.localPosition == shakeDestination)
            {
                if(shakeCount == shakeLastCount+1)
                {
                    isShake = false;
                    
                }

                if(shakeCount%2==0) // 짝수 
                {
                    shakeDestination = shakePostion[1];
                }
                else//홀수
                {
                    shakeDestination = shakePostion[0];
                }
                if (shakeCount == shakeLastCount)
                {
                    shakeDestination = originPos;
                    
                }

                 shakeCount++;

            }
        }
    }
}
