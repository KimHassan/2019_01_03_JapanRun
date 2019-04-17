using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsTouchScreen : MonoBehaviour
{
    public GameObject enemy;
    CsEnemy e;

    // Start is called before the first frame update
    void Start()
    {
        enemy.GetComponent<CsEnemy>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
