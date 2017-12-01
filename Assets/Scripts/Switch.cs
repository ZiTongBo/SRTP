using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Switch : MonoBehaviour {
    public Texture2D t1;
    public Texture2D t2;
    public Texture2D t3;
    int sWidth;
    int sHeight;
	// Use this for initialization
	void Start () {
        sWidth = Screen.width;
        sHeight = Screen.height;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI()
    {
        GUI.Window(0, new Rect(sWidth/4 - sWidth / 10, sHeight/8, sWidth/5, sWidth / 5 +80), window0, "户型1");
        GUI.Window(1, new Rect(sWidth/2 - sWidth / 10, sHeight / 8, sWidth/5, sWidth / 5 + 80), window1, "户型2");
        GUI.Window(2, new Rect(sWidth*3/ 4 - sWidth / 10, sHeight / 8, sWidth / 5, sWidth / 5 + 80), window2, "户型3");
        GUI.Button(new Rect(sWidth / 2 - sWidth / 20, sHeight *5/8 , sWidth / 10, 50), "导入户型");
        if (GUI.Button(new Rect(sWidth/2 - sWidth / 20, sHeight * 5/8 + 100, sWidth / 10, 50), "联系作者"))
        {
            Application.OpenURL("http://www.bozt.top");
        }
        if (GUI.Button(new Rect(sWidth / 2 - sWidth / 20, sHeight * 5/8 + 200, sWidth / 10, 50), "退出"))
        {
            Application.Quit();
        }
    }

    void window0(int windowID)
    {
        GUI.Box(new Rect(0, 20, sWidth / 5, sWidth / 5), t1);
        if(GUI.Button(new Rect(0, sWidth / 5 + 20, sWidth / 5, 60),"选择"))
        {
            SceneManager.LoadScene("ViewScene");
        }
    }

    void window1(int windowID)
    {
        GUI.Box(new Rect(0, 20, sWidth / 5, sWidth / 5), t2);
        if (GUI.Button(new Rect(0, sWidth / 5 + 20, sWidth / 5, 60), "选择"))
        {
            SceneManager.LoadScene("ViewScene");
        }
    }

    void window2(int windowID)
    {
        GUI.Box(new Rect(0, 20, sWidth / 5, sWidth / 5), t3);
        if (GUI.Button(new Rect(0, sWidth / 5 + 20, sWidth / 5, 60), "选择"))
        {
            SceneManager.LoadScene("ViewScene");
        }
    }
}
