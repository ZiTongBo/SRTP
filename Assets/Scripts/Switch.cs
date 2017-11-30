using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Switch : MonoBehaviour {
    public Texture2D t1;
    public Texture2D t2;
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
        GUI.Window(0, new Rect(sWidth/3-100, 50, 200, 300), window0, "户型1");
        GUI.Window(1, new Rect(sWidth*2/3 - 100, 50, 200, 300), window1, "户型2");
        if (GUI.Button(new Rect(sWidth/2-50, 500, 100, 40), "个人主页"))
        {
            Application.OpenURL("http://www.bozt.top");
        }
    }

    void window0(int windowID)
    {
        GUI.Box(new Rect(0, 40, 200, 200), t1);
        if(GUI.Button(new Rect(0,240,200,40),"选择"))
        {
            SceneManager.LoadScene("ViewScene");
        }
    }

    void window1(int windowID)
    {
        GUI.Box(new Rect(0, 40, 200, 200), t2);
        if (GUI.Button(new Rect(0, 240, 200, 40), "选择"))
        {
            SceneManager.LoadScene("ViewScene");
        }
    }
}
