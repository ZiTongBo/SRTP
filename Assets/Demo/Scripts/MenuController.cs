//------------------------------------------------------------------------------
// Copyright 2016 Baofeng Mojing Inc. All rights reserved.
//------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;//引用事件命名空间
using UnityEngine.UI;//引用UI命名空间

public class MenuController : MonoBehaviour
{

    public GameObject[] Button_Object;
    public int buttonCurIndex = -1;
    public Demo demo;

    public Text TimeWarpText;

    // Use this for initialization
    void Start()
    {
        if (ConfigItem.TW_STATE)
        {
            TimeWarpText.text = "关闭 TimeWarp";
        }
        else
        {
            TimeWarpText.text = "打开 TimeWarp";
        }
        HoverNext();
    }

    public void HoverNext()
    {    
        buttonCurIndex++;
        buttonCurIndex = buttonCurIndex % Button_Object.Length;

        for (int i = 0; i < Button_Object.Length; i++)
        {
            if (i != buttonCurIndex)
            {
                Button_Object[i].GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f);
                Button_Object[i].GetComponent<RectTransform>().sizeDelta = new Vector2(150, 40);
                //ExecuteEvents.Execute(Button_Object[i], null, ExecuteEvents.pointerClickHandler);
            }
            else
            {
                Button_Object[i].GetComponent<Image>().color = new Color(0.5f, 1.0f, 0.5f);
                Button_Object[i].GetComponent<RectTransform>().sizeDelta = new Vector2(160, 50);
            }

        }
    }

    public void HoverPrev()
    {
        buttonCurIndex--;
        if (buttonCurIndex < 0)
            buttonCurIndex = Button_Object.Length - 1;

        for (int i = 0; i < Button_Object.Length; i++)
        {
            if (i != buttonCurIndex)
            {
                Button_Object[i].GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f);
                Button_Object[i].GetComponent<RectTransform>().sizeDelta = new Vector2(150, 40);
                //ExecuteEvents.Execute(Button_Object[i], null, ExecuteEvents.pointerClickHandler);
            }
            else
            {
                Button_Object[i].GetComponent<Image>().color = new Color(0.5f, 1.0f, 0.5f);
                Button_Object[i].GetComponent<RectTransform>().sizeDelta = new Vector2(160, 50);
            }

        }
    }

    public void HoverRight()
    {
        if (buttonCurIndex < Button_Object.Length / 2)
        {
            buttonCurIndex = buttonCurIndex + Button_Object.Length / 2;
        }

        for (int i = 0; i < Button_Object.Length; i++)
        {
            if (i != buttonCurIndex)
            {
                Button_Object[i].GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f);
                Button_Object[i].GetComponent<RectTransform>().sizeDelta = new Vector2(150, 40);
            }
            else
            {
                Button_Object[i].GetComponent<Image>().color = new Color(0.5f, 1.0f, 0.5f);
                Button_Object[i].GetComponent<RectTransform>().sizeDelta = new Vector2(160, 50);
            }

        }
    }

    public void HoverLeft()
    {
        if (buttonCurIndex >= Button_Object.Length / 2)
        {
            buttonCurIndex = buttonCurIndex - Button_Object.Length / 2;
        }

        for (int i = 0; i < Button_Object.Length; i++)
        {
            if (i != buttonCurIndex)
            {
                Button_Object[i].GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f);
                Button_Object[i].GetComponent<RectTransform>().sizeDelta = new Vector2(150, 40);
            }
            else
            {
                Button_Object[i].GetComponent<Image>().color = new Color(0.5f, 1.0f, 0.5f);
                Button_Object[i].GetComponent<RectTransform>().sizeDelta = new Vector2(160, 50);
            }

        }
    }

    public void PressCurrent()
    {
        switch (buttonCurIndex)
        {
            case 0:
                demo.ShowScene360Photo();
                break;
            case 1:
                demo.ShowStereoImage();
                break;
            case 2:
                demo.Mojing1stC();
                break;
            case 3:
                demo.Mojing3rdC();
                break;
            case 4:
                demo.EnableTimeWarp();
                break;
            case 5:
                demo.EyeDemo();
                
                break;
            case 6:
                demo.OpenGlassMenu();
                
                break;
            case 7:
                demo.ReportDemo();
                
                break;
            case 8:
                demo.LoginPayDemo();
                break;
            case 9:
                demo.DepthOfField();
                break;
        }
    }
}
