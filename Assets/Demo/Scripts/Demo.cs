//------------------------------------------------------------------------------
// Copyright 2016 Baofeng Mojing Inc. All rights reserved.
// 
// Author: Xu Xiang
//------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Demo : MonoBehaviour {

    public Text TimeWarpText;
    public GameObject Tips;

    public void ShowScene360Photo()
    {
        SceneManager.LoadScene("360PhotoDemo");
        MojingSDK.SetCenterLine(10, new Color(1.0f, 0.0f, 0.0f, 1.0f));
    }

    public void ShowStereoImage()
    {
        SceneManager.LoadScene("StereoImage");
        MojingSDK.SetCenterLine(4, new Color(0.0f, 0.0f, 1.0f, 1.0f));
    }

	public void Mojing1stC()
	{
        SceneManager.LoadScene("Mojing1stC");
    }
	
	public void Mojing3rdC()
	{
        SceneManager.LoadScene("Mojing3rdC");
    }
    public void ShowMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ReportDemo()
    {
        SceneManager.LoadScene("MojingReport");
    }
    public void LoginPayDemo()
    {
#if UNITY_IOS || UNITY_EDITOR
        if(Tips!=null)
            Tips.SetActive(true);
        return;
#endif
        SceneManager.LoadScene("MojingLoginPay");
    }

    public void EnableTimeWarp()
    {
        if (!MojingVRHead.Instance.VRModeEnabled)
            return;

        ConfigItem.TW_STATE = !ConfigItem.TW_STATE;
        Debug.Log("TimeWarp " + ConfigItem.TW_STATE.ToString());

        MojingSDK.Unity_SetEnableTimeWarp(ConfigItem.TW_STATE);
        Mojing.SDK.bWaitForMojingWord = true;
        if (ConfigItem.TW_STATE && TimeWarpText!=null)
        {
            TimeWarpText.text = "关闭 TimeWarp";
        }
        else if(!ConfigItem.TW_STATE && TimeWarpText != null)
        {
            TimeWarpText.text = "打开 TimeWarp";
        }
    }

    public void DepthOfField()
    {
//#if UNITY_IOS 
//        Tips.SetActive(true);
//        return;
//#endif
        SceneManager.LoadScene("ViewLock");
    }

    //展开||关闭 镜片选择二级菜单
    public void OpenGlassMenu()
    {
        //UIListController.show_flag = !UIListController.show_flag;
        //UIListController.show_change = true;
        GlassesList.List_Show = !GlassesList.List_Show;
    }

    //通过glassName获取glasskey
    static string GetGlassKeyByName(string glassName)
    {
        for (int i = 0; i < Mojing.glassesNameList.Count; i++)
        {
            if (Mojing.glassesNameList[i] == glassName)
                return Mojing.glassesKeyList[i];
        }
        return "";
    }

    //展开二级菜单后，选择更换glasses
    public void ChangeGlass(string glassName)
    {
        OpenGlassMenu();
        string sNewKey = GetGlassKeyByName(glassName);

        if (MojingSDK.cur_GlassKey != sNewKey)
        {
            MojingSDK.cur_GlassKey = sNewKey;
            Mojing.SDK.GlassesKey = MojingSDK.cur_GlassKey;
        }
        MojingLog.LogTrace("new Glasses Name:" + glassName + ", new Glasses Key:" + MojingSDK.cur_GlassKey);
    }

    //public void ToggleVRMode() {
    //      MojingVRHead.Instance.VRModeEnabled = !MojingVRHead.Instance.VRModeEnabled;
    //      if (MojingVRHead.Instance.VRModeEnabled && Mojing.SDK.NeedDistortion)
    //      {
    //          Mojing.SDK.bWaitForMojingWord = true;
    //      }
    //  }

    public void EyeDemo()
    {
        SceneManager.LoadScene("EyeDemo");
    }

  public void Start()
  {
      getParameter.EvnOnGetParameter += getParameter_EvnOnGetParameter;
  }

  void OnDestroy()
  {
      getParameter.EvnOnGetParameter -= getParameter_EvnOnGetParameter;
  }

  void getParameter_EvnOnGetParameter(string strName)
  {
      ChangeGlass(strName);
  }
}
