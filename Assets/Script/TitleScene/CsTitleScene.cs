using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CsTitleScene : MonoBehaviour
{
    bool isChange = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoInGame1()
    {
        SceneManager.LoadScene("InGameScene1");
    }
    public void GoInGame2()
    {
        SceneManager.LoadScene("InGameScene2");
    }
    public void GoShop()
    {
        SceneManager.LoadScene("ShopScene");
    }
    public void GoHanja()
    {
        SceneManager.LoadScene("HanjaScene");
    }
}
