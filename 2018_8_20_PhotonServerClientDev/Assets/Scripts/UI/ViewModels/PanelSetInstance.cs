using System;
using System.Collections;
using System.Collections.Generic;
using MyType;
using UnityEngine;

public class PanelSetInstance : MonoBehaviour
{
    public List<GameObject> panels;

    private static PanelSetInstance instance;
    public static PanelSetInstance Instance
    {
        get
        {
            if(!instance)
            {
                instance = FindObjectOfType(typeof(PanelSetInstance)) as PanelSetInstance;

                if(!instance)
                {
                    Debug.Log("Cant find PanelSetInstance.");
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void OnDisable()
    {
        instance = null;
    }

    public void OpenPanel(string panelName)
    {
        if(panelName==null)
        {
            Debug.LogError("panelName is Null!");
        }

        foreach(var item in panels)
        {
            if(item.name==panelName)
            {
                item.gameObject.SetActive(true);
            }else
            {
                item.gameObject.SetActive(false);
            }
        }
    }

}
