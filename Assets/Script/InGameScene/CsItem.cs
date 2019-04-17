using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CsItem : MonoBehaviour
{
    public int ItemType = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void setItem(int _itemType)
    {
        ItemType = _itemType;
        switch (ItemType)
        {
            case 0:
                {
                    break;
                }

            case 1:
                {

                    break;
                }
        }
    }
    void ItemUse()
    {
        switch (ItemType)
        {
            case 0:
                {
                    break;
                }

            case 1:
                {
                    break;
                }
        }

    }
}
