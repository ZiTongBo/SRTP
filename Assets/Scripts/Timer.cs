using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {
    private float _time = 0;
    static private bool _flag = false;
    private GameObject _select = null;
    GameObject FPS;
    int sHeight;
    int sWidth;
    public static bool getTimer()
    {
        return _flag;
    }

    // Use this for initialization
    void Start()
    {
        sHeight = Screen.height;
        sWidth = Screen.width;
        FPS = GameObject.Find("RigidBodyFPSController");
    }

    // Update is called once per frame
    void Update() {
        
        if (Selector._select == null)
        {
            _time = 0;
            return;
        }
        if (Selector._select != _select)
        {
            _select = Selector._select;
            _time = 0;
            _flag = false;
        }
        if (_time < 3.0f)
        {
            _time += Time.deltaTime;
        }
        else
        {
            if (_flag == false)
            {
                Debug.Log("lock");
                _flag = true;
                Selector.LockSelector();
                gameState.State = 1;
            }
        }
    }

    private void OnGUI()
    {
        switch (gameState.State)
        {
            case 1:
                FPS.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Selector.LockSelector();
                if (GUI.Button(new Rect(sWidth * 2 / 4, sHeight * 2 / 5, 80, 50), "移动"))
                {
                    _select.AddComponent<Mover>();
                    gameState.State = 2;
                }
                if (GUI.Button(new Rect(sWidth * 3 / 5, sHeight * 2 / 4, 80, 50), "旋转"))
                {
                    _select.AddComponent<Rotater>();
                    gameState.State = 3;
                }
                if (GUI.Button(new Rect(sWidth * 2 / 5, sHeight * 2 / 4, 80, 50), "更换"))
                {
                    _select.AddComponent<Changer>();
                    gameState.State = 4;
                }
                if (GUI.Button(new Rect(sWidth * 2 / 4, sHeight * 3 / 5, 80, 50), "删除"))
                {
                    _select.AddComponent<Deleter>();
                    gameState.State = 5;
                }
                break;
            default:
                break;
        }
    }
}
