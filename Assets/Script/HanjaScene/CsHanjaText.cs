using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CsHanjaText : MonoBehaviour
{
    public Text kanji;

    public Text mean;

    public Text meanRead;

    public Text soundRead;

    // Start is called before the first frame update
    void Start()
    {

       

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setUp()
    {
        kanji = transform.Find("HanjaBg").gameObject.GetComponentInChildren<Text>();

        mean = transform.Find("mean").gameObject.GetComponent<Text>();

        meanRead = transform.Find("meanRead").gameObject.GetComponent<Text>();


        soundRead = transform.Find("soundRead").gameObject.GetComponent<Text>();
    }
}
