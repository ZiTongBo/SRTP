//------------------------------------------------------------------------------
// Copyright 2016 Baofeng Mojing Inc. All rights reserved.
//------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using MojingSample.CrossPlatformInput;
using MojingSample.CrossPlatformInput.MojingInput;
using System;
using UnityEngine.UI;
public class UISimulator : MonoBehaviour {
    public RectTransform[] UI;
    public Text info;
    public Text touchpad_x;
    public Text touchpad_y;
    public Material switchMat;

    Transform CenterPointer;
    Camera UICamera;
    IntPtr texID = IntPtr.Zero;
    RenderTexture UIScreen;
    void Start()
    {
        //MojingInputManager.DeviceAttachedCallback += onDeviceAttachedCallback;

        CenterPointer = GameObject.Find("MojingMain/MojingVrHead/GazePointer").transform;
        UICamera = GameObject.Find("UICamera").GetComponent<Camera>();
        int size = MojingSDK.Unity_GetTextureSize();
        UIScreen = new RenderTexture(size, size, 24, RenderTextureFormat.Default);
        UIScreen.anisoLevel = 0;
        UIScreen.antiAliasing = Mathf.Max(QualitySettings.antiAliasing, 1);
        UIScreen.Create();
        UICamera.targetTexture = UIScreen;
        texID = UIScreen.GetNativeTexturePtr();
    }

    //private void onDeviceAttachedCallback(bool obj)
    //{
    //    if (obj)
    //    {
    //        switchMat.SetColor("_Color", Color.green);
    //        info.text = MojingInputManager.Instance.device_name_attach + " connected";
    //    }
    //    else
    //    {
    //        switchMat.SetColor("_Color", Color.red);
    //        info.text = "Device disconnected";
    //    }
    //}

    void Update()
    {
        if (CrossPlatformInputManager.GetButton("LEFT"))
        {
            UI[0].sizeDelta = new Vector2(60, 60);
        }
        else if (CrossPlatformInputManager.GetButtonUp("LEFT"))
        {
            UI[0].sizeDelta = new Vector2(50, 50);
        }

        if (CrossPlatformInputManager.GetButton("RIGHT"))
        {
            UI[1].sizeDelta = new Vector2(60, 60);
        }
        else if (CrossPlatformInputManager.GetButtonUp("RIGHT"))
        {
            UI[1].sizeDelta = new Vector2(50, 50);
        }

        if (CrossPlatformInputManager.GetButton("UP"))
        {
            UI[2].sizeDelta = new Vector2(60, 60);
        }
        else if (CrossPlatformInputManager.GetButtonUp("UP"))
        {
            UI[2].sizeDelta = new Vector2(50, 50);
        }

        if (CrossPlatformInputManager.GetButton("DOWN"))
        {
            UI[3].sizeDelta = new Vector2(60, 60);
        }
        else if (CrossPlatformInputManager.GetButtonUp("DOWN"))
        {
            UI[3].sizeDelta = new Vector2(50, 50);
        }
        if (CrossPlatformInputManager.GetButton("VOLUME_UP"))
        {
            UI[4].sizeDelta = new Vector2(60, 60);
        }
        else if (CrossPlatformInputManager.GetButtonUp("VOLUME_UP"))
        {
            UI[4].sizeDelta = new Vector2(50, 50);
        }

        if (CrossPlatformInputManager.GetButton("VOLUME_DOWN"))
        {
            UI[5].sizeDelta = new Vector2(60, 60);
        }
        else if (CrossPlatformInputManager.GetButtonUp("VOLUME_DOWN"))
        {
            UI[5].sizeDelta = new Vector2(50, 50);
        }
        if (CrossPlatformInputManager.GetButton("C")|| CrossPlatformInputManager.GetButton("B"))
        {
            UI[6].sizeDelta = new Vector2(60, 60);
        }
        else if (CrossPlatformInputManager.GetButtonUp("C")|| CrossPlatformInputManager.GetButtonUp("B"))
        {
            UI[6].sizeDelta = new Vector2(50, 50);
        }
        if (CrossPlatformInputManager.GetButton("OK")|| CrossPlatformInputManager.GetButton("A"))
        {
            UI[7].sizeDelta = new Vector2(60, 60);
        }
        else if (CrossPlatformInputManager.GetButtonUp("OK")|| CrossPlatformInputManager.GetButtonUp("A"))
        {
            UI[7].sizeDelta = new Vector2(50, 50);
        }
        if (CrossPlatformInputManager.GetAxis("Horizontal") != 0)
        {
            touchpad_x.text = "x轴:  " + CrossPlatformInputManager.GetAxis("Horizontal");
        }
        if (CrossPlatformInputManager.GetAxis("Vertical") != 0)
        {
            touchpad_y.text = "y轴:  " + CrossPlatformInputManager.GetAxis("Vertical");
        }
        if (Mojing.SDK.NeedDistortion)
        {
            MojingSDK.Unity_SetOverlay3D(MojingSDK.TEXTURE_BOTH_EYE, texID, 1, 1, CenterPointer.position.magnitude);
        }
    }
}
