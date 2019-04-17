using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CsEffectManager : MonoBehaviour
{
    
    GameObject attackEffect;
    GameObject damagedEffect;

    static public CsEffectManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (CsEffectManager.instance == null)
            CsEffectManager.instance = this;



    }
    void Start()
    {
        attackEffect = Resources.Load<GameObject>("Sprite/Effect/PEA_1") as GameObject;
        damagedEffect = Resources.Load<GameObject>("Sprite/Effect/EEA_2") as GameObject;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setAttackEffect(Vector3 position)
    {
        Instantiate(attackEffect, transform.position, Quaternion.identity);
    }
    public void setDamagedEffect(Vector3 position)
    {
        Instantiate(damagedEffect, new Vector3(-1.25f,0,0), Quaternion.identity);
    }
}
