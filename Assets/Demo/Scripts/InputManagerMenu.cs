//------------------------------------------------------------------------------
// Copyright 2016 Baofeng Mojing Inc. All rights reserved.
//------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MojingSample.CrossPlatformInput;
using MojingSample.CrossPlatformInput.MojingInput;
public class InputManagerMenu : MonoBehaviour
{

    public MenuController menu_controller;
    public GlassesList glass_controller;
    MojingInputManager input;

    Vector3 origin = new Vector3(0, 0, 0);
    Vector3 target = new Vector3(-15, 0, 15);
    public RectTransform[] button = new RectTransform[10];
    public Camera mainCam;
    public Demo demo;
    private bool connect_flag = true;//Make sure the first button high-lighted when disconnect switch to connect
    void Awake()
    {
        input = FindObjectOfType<MojingInputManager>();
    }
    void Update()
    {
        //Debug.Log("input.attachstate: " + input.attachstate);
        //Debug.Log("input.attachstate:device_name_attach: " + input.device_name_attach);
        //Debug.Log("input.attachstate:device_name_detach: " + input.device_name_detach);
        if (input.attachstate == MojingInputManager.AttachState.Connected )
        {
            if (input.device_name_attach.Contains("mojing-motion"))
            {
                connect_flag = true;
                ColliderRay(mainCam);
            }
            else
            {
                if (connect_flag)
                {
                    //menu_controller.HoverNext();
                    ResetButton();
                    menu_controller.buttonCurIndex = 0;
                    connect_flag = false;
                }
                JoystickRespond();
            }
        }

        else
        {
            if (input.device_name_detach.Contains("mojing-motion"))
            {
                if (connect_flag)
                {
                    //menu_controller.HoverNext();
                    ResetButton();
                    menu_controller.buttonCurIndex = 0;
                    connect_flag = false;
                }
                JoystickRespond();
            }
            else
            {
                connect_flag = true;
                ColliderRay(mainCam);
            }

        }

        if (CrossPlatformInputManager.GetButtonUp("C") || CrossPlatformInputManager.GetButtonUp("B"))
        {
#if !UNITY_EDITOR && UNITY_ANDROID
            Application.Quit();
#endif
        }
    }


    void JoystickRespond()
    {
        if (CrossPlatformInputManager.GetButtonLongPressed("OK") || CrossPlatformInputManager.GetButtonLongPressed("A"))
        {
            Debug.Log("OK-----Long GetButtonDown");
        }

        if (CrossPlatformInputManager.GetButtonDown("OK"))
        {
            Debug.Log("OK-----GetButtonDown");
        }
        if (CrossPlatformInputManager.GetButton("OK"))
        {
            Debug.Log("OK-----GetButton");
        }
        else if (CrossPlatformInputManager.GetButtonUp("OK") || CrossPlatformInputManager.GetButtonUp("A"))
        {
            Debug.Log("OK-----GetButtonUp");
            if (menu_controller != null && glass_controller != null)
            {
                if (GlassesList.List_Show)//镜片选择的二级菜单
                    glass_controller.PressCurrent();
                else//场景选择的一级菜单
                    menu_controller.PressCurrent();
            }
        }
        if (CrossPlatformInputManager.GetButtonDown("C"))
        {
            Debug.Log("C-----GetButtonDown");
        }
        if (CrossPlatformInputManager.GetButton("C"))
        {
            Debug.Log("C-----GetButton");
        }
        else if (CrossPlatformInputManager.GetButtonUp("C"))
        {
            Debug.Log("C-----GetButtonUp");
        }

        if (CrossPlatformInputManager.GetButtonDown("MENU"))
        {
            Debug.Log("MENU-----GetButtonDown");
        }
        if (CrossPlatformInputManager.GetButton("MENU"))
        {
            Debug.Log("MENU-----GetButton");
        }
        else if (CrossPlatformInputManager.GetButtonUp("MENU") || CrossPlatformInputManager.GetButtonUp("X"))
        {
            Debug.Log("MENU-----GetButtonUp");
            MojingSDK.Unity_ResetSensorOrientation();
        }

        if (CrossPlatformInputManager.GetButton("UP"))
        {
            Debug.Log("UP-----GetButton");
        }
        else if (CrossPlatformInputManager.GetButtonUp("UP"))
        {
            if (menu_controller != null && glass_controller != null)
            {
                if (GlassesList.List_Show)
                    glass_controller.HoverPrev();
                else
                    menu_controller.HoverPrev();
            }
        }

        if (CrossPlatformInputManager.GetButton("DOWN"))
        {
            Debug.Log("DOWN-----GetButton");
        }
        else if (CrossPlatformInputManager.GetButtonUp("DOWN"))
        {
            Debug.Log("DOWN-----GetButtonUp");
            if (menu_controller != null && glass_controller != null)
            {
                if (GlassesList.List_Show)
                    glass_controller.HoverNext();
                else
                    menu_controller.HoverNext();
            }
        }

        if (CrossPlatformInputManager.GetButton("LEFT"))
        {
            Debug.Log("LEFT-----GetButton");
        }
        else if (CrossPlatformInputManager.GetButtonUp("LEFT"))
        {
            if (menu_controller != null && glass_controller != null)
            {
                if (!GlassesList.List_Show)
                    menu_controller.HoverLeft();
            }
        }

        if (CrossPlatformInputManager.GetButton("RIGHT"))
        {
            Debug.Log("RIGHT-----GetButton");
        }
        else if (CrossPlatformInputManager.GetButtonUp("RIGHT"))
        {
            if (menu_controller != null && glass_controller != null)
            {
                if (!GlassesList.List_Show)
                    menu_controller.HoverRight();
            }
        }

        if (CrossPlatformInputManager.GetButton("CENTER"))
        {
        }
        else if (CrossPlatformInputManager.GetButtonUp("CENTER"))
        {
        }
    }


    void ColliderRay(Camera camera)
    {
        target = camera.transform.forward * 100;
        Ray ray = new Ray(origin, target);
        Debug.DrawLine(origin, camera.transform.forward * 100, Color.red);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            //Debug.Log("hitInfo:" + hitInfo.collider.name);
            Image rect = hitInfo.collider.GetComponent<Image>();
            if (rect != null)
            {
                hitInfo.collider.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 50);
                rect.color = new Color(0.5f, 1.0f, 0.5f);
            }
            if (CrossPlatformInputManager.GetButtonUp("A"))
            {
                switch (hitInfo.collider.name)
                {
                    case "360Photo Demo":
                        demo.ShowScene360Photo();
                        break;
                    case "StereoPhoto":
                        demo.ShowStereoImage();
                        break;
                    case "Mojing1stC":
                        demo.Mojing1stC();
                        break;
                    case "Mojing3rdC":
                        demo.Mojing3rdC();
                        break;
                    case "TWEnable":
                        demo.EnableTimeWarp();
                        break;
                    case "EyeDemo":
                        demo.EyeDemo();
                        break;
                    case "GlassesList":
                        demo.OpenGlassMenu();
                        break;
                    case "MojingReport":
                        demo.ReportDemo();
                        break;
                    case "MojingLoginPay":
                        demo.LoginPayDemo();
                        break;
                    case "DepthOfField":
                        demo.DepthOfField();
                        break;
                }
            }
        }
        else
        {
            for (int j = 0; j < button.Length; j++)
            {
                button[j].sizeDelta = new Vector2(150, 40);
                button[j].gameObject.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f);
            }
        }
    }


    void ResetButton()
    {
        button[0].GetComponent<Image>().color = new Color(0.5f, 1.0f, 0.5f);
        button[0].GetComponent<RectTransform>().sizeDelta = new Vector2(160, 50);
        for (int i = 1; i < button.Length; i++)
        {
            button[i].GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f);
            button[i].GetComponent<RectTransform>().sizeDelta = new Vector2(150, 40);
        }
    }
}