using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;

public class ImageBuffer : MonoBehaviour {
    byte[] inoutBuffer;
    Texture2D tex;
    IntPtr texID = IntPtr.Zero;
    public static float HEADCTRL_WIDTH = 1;
    public static float HEADCTRL_HEIGHT = 1;
    Camera[] cams;
    Transform CenterPointer;
    MojingController controller;
    public void Start()
    {
        controller = FindObjectOfType<MojingController>();
        cams = FindObjectsOfType<Camera>();
        foreach (Camera cam in cams)
        {
            cam.cullingMask = 0;
        }
        CenterPointer = GameObject.Find("MojingMain/MojingVrHead/GazePointer").transform;
        tex = new Texture2D(2000, 201, TextureFormat.RGB24, false);
        inoutBuffer = CreateImageBuffer(2000, 201, TextureFormat.RGB24);
        texID = tex.GetNativeTexturePtr();
    }


    float[] X_List = new float[2000];
    float[] Y_List = new float[2000];
    float[] Z_List = new float[2000];
    void Update()
    {
        //Draw angular acceleration curve
        AppendImageBuffer(X_List, Y_List, Z_List, MojingController.AngularArray[0].x, MojingController.AngularArray[0].y, MojingController.AngularArray[0].z);
        //GetComponent<Renderer>().material.mainTexture = tex;
        MojingSDK.Unity_SetOverlay3D(MojingSDK.TEXTURE_BOTH_EYE, texID, HEADCTRL_WIDTH, HEADCTRL_HEIGHT, CenterPointer.position.magnitude);
    }

    int index = 0;
    public void AppendImageBuffer(float[] x_list, float[] y_list, float[] z_list, float x_value, float y_value, float z_value)
    {
        if (index < 2000)
        {
            x_list[index] = x_value;
            y_list[index] = y_value;
            z_list[index] = z_value;
            index++;
        }
        else
        {
            for (int i = 0; i < 1999; i++)
            {
                x_list[i] = x_list[i + 1];
                y_list[i] = y_list[i + 1];
                z_list[i] = z_list[i + 1];
            }
            x_list[1999] = x_value;
            y_list[1999] = y_value;
            z_list[1999] = z_value;
        }

        UpdateImageBuffer(inoutBuffer, 2000, 201, x_list, y_list, z_list);
        tex.LoadRawTextureData(inoutBuffer); // Load data into the texture and upload it to the GPU.
        tex.Apply();
    }

    byte[] CreateImageBuffer(int iWidth, int iHeight, TextureFormat RTF)
    {   int iIndex = 0;
        if (RTF != TextureFormat.RGB24)
            return null;

        byte[] Buffer = new byte[iWidth * iHeight * 3];
        int iSize = iWidth * iHeight * 3;
        while (iIndex < iSize)
        {
            Buffer[iIndex++] = 0x00; // R
            Buffer[iIndex++] = 0x00; // G
            Buffer[iIndex++] = 0xFF; // B
        }
        return Buffer;
    }

    void UpdateImageBuffer(byte[] inoutBuffer, int iWidth, int iHeight, float[] fX, float[] fY, float[] fZ)
    {
        float fZoom = 1;
        for (int iIndex = 0; iIndex < iWidth; iIndex++)
        {
            fZoom = Math.Max(fZoom, Math.Max(Math.Max(fX[iIndex], fY[iIndex]), fZ[iIndex]));
        }
        int iMaxValue = (int)(iHeight * 0.5f);
        int iCenter = (int)(iHeight * 0.5f + 0.5f);
        for (int iX = 0; iX < iWidth; iX++)
        {
            int iPtX = Math.Min(iHeight - 1, Math.Max((int)(fX[iX] / fZoom * iMaxValue + iCenter), 0));
            int iPtY = Math.Min(iHeight - 1, Math.Max((int)(fY[iX] / fZoom * iMaxValue + iCenter), 0));
            int iPtZ = Math.Min(iHeight - 1, Math.Max((int)(fZ[iX] / fZoom * iMaxValue + iCenter), 0));
            for (int iY = 0; iY < iHeight; iY++)
            {
                int iPos = 3 * (iX + iY * iWidth);
                if (iY == iPtX)
                {// Set to R
                    inoutBuffer[iPos + 0] = 0xFF;
                    inoutBuffer[iPos + 1] = 0x00;
                    inoutBuffer[iPos + 2] = 0x00;
                }
                else if (iY == iPtY)
                {// Set to G
                    inoutBuffer[iPos + 0] = 0x00;
                    inoutBuffer[iPos + 1] = 0xFF;
                    inoutBuffer[iPos + 2] = 0x00;
                }
                else if (iY == iPtZ)
                {// Set to B
                    inoutBuffer[iPos + 0] = 0x00;
                    inoutBuffer[iPos + 1] = 0x00;
                    inoutBuffer[iPos + 2] = 0xFF;
                }
                else if (iY == iCenter)
                {// set to W
                    inoutBuffer[iPos + 0] = 0xFF;
                    inoutBuffer[iPos + 1] = 0xFF;
                    inoutBuffer[iPos + 2] = 0xFF;
                }
                else
                {// set to dark
                    inoutBuffer[iPos + 0] = 0x00;
                    inoutBuffer[iPos + 1] = 0x00;
                    inoutBuffer[iPos + 2] = 0x00;
                }
            }
        }
    }

}
