//------------------------------------------------------------------------------
// Copyright 2016 Baofeng Mojing Inc. All rights reserved.
//------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;
public class SetOverlayFPS : MonoBehaviour {

    private float fps = 60;
    public Text text1;
    private int count = 60;
    Transform CenterPointer;
    Camera LCamera;
    Camera RCamera;
    Camera UICamera;
    IntPtr texID = IntPtr.Zero;
	RenderTexture UIScreen;
    void Start()
    {
        LCamera = GameObject.Find("MojingMain/MojingVrHead/VR Camera Left").GetComponent<Camera>();
        RCamera = GameObject.Find("MojingMain/MojingVrHead/VR Camera Right").GetComponent<Camera>();
        CenterPointer = GameObject.Find("MojingMain/MojingVrHead/GazePointer").transform;
        UICamera = GameObject.Find("UICamera").GetComponent<Camera>();

        int size = MojingSDK.Unity_GetTextureSize();
        UIScreen = new RenderTexture(size, size, 24, RenderTextureFormat.Default);
        UIScreen.anisoLevel = 0;
		UIScreen.antiAliasing = Mathf.Max(QualitySettings.antiAliasing, 1);

        UICamera.targetTexture = UIScreen;
        
		if (SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Metal) //Metal Rendering
			texID = UIScreen.colorBuffer.GetNativeRenderBufferPtr();
		else
            texID = UIScreen.GetNativeTexturePtr();
    }

    void Update()
    {
        count++;
        float interp = Time.deltaTime / (0.5f + Time.deltaTime);
        float currentFPS = 1.0f / Time.deltaTime;
        if (count >= 5)
        {
            fps = Mathf.Lerp(fps, currentFPS, interp);
            text1.text = "Fps:" + Mathf.RoundToInt(fps)
                + ", " + "Time:" + Mathf.RoundToInt(Time.time)
#if !UNITY_EDITOR && UNITY_ANDROID
                + "\n" + "Cap:" + GetBatteryCapacity()
                + ", " + "Current：" + GetElectricCurrent()
                + "\n" + "GPUF：" + GetGPUFreq()
                + ", " + "GPUT：" + GetGPUTemperature()
                + "\n" + "CPUF：" + GetCPUFreq()
                + "\n" + "CPUT：" + GetCPUTemperature()
#endif              
                ;

            //MojingLog.LogTrace("帧率：" + Mathf.RoundToInt(fps)
            //    + ",时长：" + Mathf.RoundToInt(Time.time)
            //    + ",电量：" + battery_capacity
            //    + ",电流：" + current_electric
            //    + ",GPU频率：" + gpu_frequency
            //    + ",GPU温度：" + gpu_temperature
            //    + ",CPU频率：" + cpu_frequency
            //    + ",CPU温度：" + cpu_temperature);  
            count = 0;
        }
#if !UNITY_EDITOR && UNITY_IOS
        if (SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Metal && !UIScreen.IsCreated())
        {
            UIScreen.Create();
        }
#endif
        if (Mojing.SDK.NeedDistortion && UIScreen.IsCreated())
        {
			if (SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Metal) 
			{
                texID = UIScreen.colorBuffer.GetNativeRenderBufferPtr ();
                MojingSDK.Unity_SetOverlay3D(MojingSDK.TEXTURE_BOTH_EYE | MojingSDK.TEXTURE_IS_UNITY_METAL_RENDER_TEXTURE, texID, 1, 1, CenterPointer.position.magnitude);
			}
            else
            {
                MojingSDK.Unity_SetOverlay3D(MojingSDK.TEXTURE_BOTH_EYE, texID, 1, 1, CenterPointer.position.magnitude);
            }
        }
    }

    int GetBatteryCapacity()
    {
        try
        {
            string ValueString = File.ReadAllText("/sys/class/power_supply/battery/capacity");
            return int.Parse(ValueString);
        }
        catch (Exception e)
        {
            Debug.Log("Failed to read battery power; " + e.Message);
        }
        return -1;
    }

    string GetElectricCurrent()
    {
        try
        {
            string ValueString = File.ReadAllText("/sys/class/power_supply/battery/current_now");
            return ValueString.Trim();
        }
        catch (Exception e)
        {
            Debug.Log("Failed to read battery temperature; " + e.Message);
        }
        return "Unknwon";
    }
    int GetBatteryTemperature()
    {
        try
        {
            string ValueString = File.ReadAllText("/sys/class/power_supply/battery/temp");
            return int.Parse(ValueString);
        }
        catch (Exception e)
        {
            Debug.Log("Failed to read battery temperature; " + e.Message);
        }
        return -1;
    }

    string GetBatteryStatus()
    {
        try
        {
            return File.ReadAllText("/sys/class/power_supply/battery/status").Trim();
        }
        catch (Exception e)
        {
            Debug.Log("Failed to read battery status; " + e.Message);
        }
        return "";
    }

    string GetCPUPresent()
    {
        try
        {
            return File.ReadAllText("/sys/devices/system/cpu/present").Trim();
        }
        catch (Exception e)
        {
            Debug.Log("Failed to read cpu present; " + e.Message);
        }
        return "";
    }

    string GetGPUFreq()
    {
        try
        {
            string strRet = ("");
            string ValueString = File.ReadAllText("/sys/class/kgsl/kgsl-3d0/gpuclk");
            double dFreq = Math.Round(int.Parse(ValueString) / 1e6f, 2);
            strRet += dFreq;
            return strRet;
        }
        catch (Exception e)
        {
            Debug.Log("Failed to read GPU freq; " + e.Message);
        }
        return "Unknown";
    }
    string GetGPUTemperature()
    {
        try
        {
            //string ValueString0 = System.IO.File.ReadAllText("/sys/class/thermal/thermal_zone0/temp");
            string ValueString0 = File.ReadAllText("/sys/devices/virtual/thermal/thermal_zone16/temp");
            return ValueString0.Trim();
        }
        catch (Exception e)
        {
            Debug.Log("Failed to read cpu temperature; " + e.Message);
        }
        return "Unknown";
    }

    string GetCPUFreq()
    {
        try
        {
            string strRet = ("");
            for (int n = 0; n < 4; n++)
            {
                string ValueString = File.ReadAllText("/sys/devices/system/cpu/cpu" + n + "/cpufreq/scaling_cur_freq");
                double dFreq = Math.Round(int.Parse(ValueString) / 1e6f, 2);

                if (strRet.Length > 0)
                    strRet += " | ";

                strRet += dFreq;
            }

            return strRet;
        }
        catch (Exception e)
        {
            Debug.Log("Failed to read cpu freq; " + e.Message);
        }
        return "Unknown";
    }

    string GetCPUTemperature()
    {
        try
        {
            //string ValueString0 = System.IO.File.ReadAllText("/sys/class/thermal/thermal_zone0/temp");
            string ValueString0 = File.ReadAllText("/sys/devices/virtual/thermal/thermal_zone3/temp");
            string ValueString1 = File.ReadAllText("/sys/devices/virtual/thermal/thermal_zone9/temp");
            return ValueString0.Trim() + " | " + ValueString1.Trim();
        }
        catch (Exception e)
        {
            Debug.Log("Failed to read cpu temperature; " + e.Message);
        }
        return "Unknown";
    }

    void OnDestroy()
    {
        UICamera.targetTexture = null;
        UIScreen.Release();
        MojingSDK.Unity_SetOverlay3D(MojingSDK.TEXTURE_BOTH_EYE, IntPtr.Zero, 1, 1, 1);
    }
}
