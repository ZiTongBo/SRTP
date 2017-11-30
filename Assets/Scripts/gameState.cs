using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class gameState : MonoBehaviour {
    public static int State = 0;
    public const int MOVE = 1;
    public const int ROTATE = 2;
    public const int CHANGE = 3;
    public const int DELETE = 4;
    public const int MENU = 6;
    public const int SAVE = 7;
    public const int ADD = 8;
    public const int ADD_CHAIR = 11;
    public const int ADD_TABLE = 12;
    public const int ADD_SPEAKER = 13;
    public const int NEW = 21;
    public GameObject chair1;
    public GameObject chair2;
    public GameObject table1;
    public GameObject table2;
    GameObject FPS;
    int sHeight;
    int sWidth;
    int i;
    GameObject newObject;
    public Texture2D bg1;
    public Texture2D bg2;
    public Texture2D t1;
    public Texture2D t2;
    public Texture2D c1;
    public Texture2D c2;

    // Use this for initialization
    void Start()
    {
        i = 1;
        sHeight = Screen.height;
        sWidth = Screen.width;
        FPS = GameObject.Find("RigidBodyFPSController");
    }

    // Update is called once per frame
    void Update () {
        switch (State)
        {
            case 0:
                Selector.RestartSelector();
                break;
            case NEW:
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Physics.Raycast(ray, out hit);
                Vector3 pos = new Vector3();
                pos.x = hit.point.x;
                pos.z = hit.point.z;
                pos.y = 0;
                newObject.transform.position = pos;
                if (Input.GetMouseButtonDown(0))
                {
                    FPS.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().enabled = true;
                    Cursor.visible = false;
                    State = 0;
                }
                break;
            
            default:
                break;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            Selector.LockSelector();
            State = MENU;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Selector.LockSelector();
            State = ADD;
        }
    }
    void OnGUI()
    {
        switch (State)
        {
            case 0:
                GUI.TextField(new Rect(sWidth / 2 - 10, 10, 90, 30), "室内装修设计");
                break;
            case MOVE:
                GUI.TextField(new Rect(sWidth / 2 - 10, 10, 100, 30), "选择移动的位置");
                break;
            case ROTATE:
                GUI.TextField(new Rect(sWidth / 2 - 10, 10, 100, 30), "选择旋转的方位");
                break;
            case CHANGE:
                GUI.TextField(new Rect(sWidth / 2 - 10, 10, 120, 30), "左键确定，右键更换");
                break;
            case DELETE:
                GUI.TextField(new Rect(sWidth / 2 - 10, 10, 120, 30), "左键删除，右键取消");
                break;
            case MENU:
                FPS.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                GUI.Window(0, new Rect(sWidth / 2 - 250, 100, 500, 500), menu, "");
                break;
            case SAVE:
                GUI.TextField(new Rect(sWidth / 2 - 10, 10, 110, 30), "保存成功");
                break;
            case ADD:
                FPS.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                GUI.Window(0, new Rect(sWidth / 2 - 250, 100, 500, 500), add, "请选择要添加的家具");
                break;
            case ADD_CHAIR:
                GUI.Window(0, new Rect(sWidth / 2 - 250, 100, 500, 500), add, "");
                break;
            case ADD_TABLE:
                GUI.Window(0, new Rect(sWidth / 2 - 250, 100, 500, 500), add, "");
                break;
            case ADD_SPEAKER:
                GUI.Window(0, new Rect(sWidth / 2 - 250, 100, 500, 500), add, "");
                break;
            default:
                break;
        }
    }

    void add(int WindowID)
    {
        GUI.Box(new Rect(0, 0, 500, 500), bg2);
        GUI.TextField(new Rect(190, 50, 120, 30), "请选择要添加的家具");
        
        switch (State)
        {
            case ADD:
                if (GUI.Button(new Rect(200, 150, 80, 50), "桌子"))
                {
                    State = ADD_TABLE;
                }
                if (GUI.Button(new Rect(200, 250, 80, 50), "椅子"))
                {
                    State = ADD_CHAIR;
                }
                if (GUI.Button(new Rect(200, 350, 80, 50), "返回"))
                {
                    State = 0;
                    FPS.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().enabled = true;
                    Cursor.visible = false;
                }
                break;
            case ADD_CHAIR:
                if (GUI.Button(new Rect(100, 150, 100, 100), c1))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    Physics.Raycast(ray, out hit);
                    Vector3 pos = new Vector3();
                    pos.x = hit.point.x;
                    pos.z = hit.point.z;
                    pos.y = 0;
                    newObject = Instantiate(chair1, pos, new Quaternion(0, 0, 0, 0));
                    newObject.AddComponent<MeshRenderer>();
                    newObject.AddComponent<MeshFilter>();
                    newObject.AddComponent<BoxCollider>();
                    newObject.GetComponent<BoxCollider>().size = new Vector3(1, 1, 1);
                    newObject.GetComponent<BoxCollider>().center = new Vector3(0, 0, 0);
                    State = NEW;
                }
                if (GUI.Button(new Rect(300, 150, 100, 100), c2))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    Physics.Raycast(ray, out hit);
                    Vector3 pos = new Vector3();
                    pos.x = hit.point.x;
                    pos.z = hit.point.z;
                    pos.y = 0;
                    newObject = Instantiate(chair2, pos, new Quaternion(0, 0, 0, 0));
                    newObject.AddComponent<MeshRenderer>();
                    newObject.AddComponent<MeshFilter>();
                    newObject.AddComponent<BoxCollider>();
                    newObject.GetComponent<BoxCollider>().size = new Vector3(1, 1, 1);
                    newObject.GetComponent<BoxCollider>().center = new Vector3(0, 0, 0);
                    State = NEW;
                   
                }
                if (GUI.Button(new Rect(200, 300, 100, 50), "返回"))
                {
                    State = ADD;
                }
                break;
            case ADD_TABLE:
                if (GUI.Button(new Rect(100, 150, 100, 100), t1))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    Physics.Raycast(ray, out hit);
                    Vector3 pos = new Vector3();
                    pos.x = hit.point.x;
                    pos.z = hit.point.z;
                    pos.y = 0;
                    newObject = Instantiate(table1, pos, new Quaternion(0, 0, 0, 0));
                    newObject.AddComponent<MeshRenderer>();
                    newObject.AddComponent<MeshFilter>();
                    newObject.AddComponent<BoxCollider>();
                    newObject.GetComponent<BoxCollider>().size = new Vector3(1, 1, 1);
                    newObject.GetComponent<BoxCollider>().center = new Vector3(0, 0, 0);
                    State = NEW;
                }
                if (GUI.Button(new Rect(300, 150, 100, 100), t2))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    Physics.Raycast(ray, out hit);
                    Vector3 pos = new Vector3();
                    pos.x = hit.point.x;
                    pos.z = hit.point.z;
                    pos.y = 0;
                    newObject = Instantiate(table2, pos, new Quaternion(0, 0, 0, 0));
                    newObject.AddComponent<MeshRenderer>();
                    newObject.AddComponent<MeshFilter>();
                    newObject.AddComponent<BoxCollider>();
                    newObject.GetComponent<BoxCollider>().size = new Vector3(1, 1, 1);
                    newObject.GetComponent<BoxCollider>().center = new Vector3(0, 0, 0);
                    State = NEW;

                }
                if (GUI.Button(new Rect(200, 300, 100, 50), "返回"))
                {
                    State = ADD;
                }
                break;
            case ADD_SPEAKER:
                break;
            default:
                break;
        }

    }
    void menu(int WindowID)
    {
        GUI.TextField(new Rect(200, 50, 100, 50), "主菜单");
        GUI.Box(new Rect(0, 0, 500, 500), bg1);
        if (GUI.Button(new Rect(200, 150, 100, 50), "保存设计"))
        {
            Application.CaptureScreenshot("Screenshot/name" + i);
            i++;
            State = SAVE;
            FPS.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().enabled = true;
            Cursor.visible = false;
        }
        if (GUI.Button(new Rect(200, 250, 100, 50), "返回户型选择"))
        {
            State = 0;
            SceneManager.LoadScene("Main");
        }
        if (GUI.Button(new Rect(200, 350, 100, 50), "返回"))
        {
            State = 0;
            FPS.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().enabled = true;
            Cursor.visible = false;
        }
    }
}
