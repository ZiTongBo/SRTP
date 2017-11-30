using UnityEngine;
using System.Collections;

public class Changer : MonoBehaviour {
    private static bool newobj = true;
    private const int chairs = 2;
    private const int tables = 2;
    private static GameObject _change = null;
    private string type;
    private int num;
    private int curNum;
    private Vector3 pos;
    private Transform parent;
    GameObject FPS;
	// Use this for initialization
	void Start () {
        if (newobj)
            _change = Selector._select;
        string name = _change.name;
        int length = name.Length;
        type = name.Substring(0, length - 2);
        num = int.Parse(name[length - 2] == '0' ? name.Substring(length - 1, 1) : name.Substring(length - 2, 2));
        pos = _change.transform.localPosition;
        parent = _change.transform.parent;
        curNum = num;
        FPS = GameObject.Find("RigidBodyFPSController");
        Debug.Log("change");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("done");
            Selector.RestartSelector();
            newobj = true;
            Destroy(_change.GetComponent<Changer>());
            gameState.State = 0;
            FPS.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().enabled = true;
            Cursor.visible = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            newobj = false;
            Debug.Log("shift");
            string path = type + "\\" + type;
            switch (type)
            {
                case "chair":
                    curNum++;
                    curNum = curNum > chairs ? curNum - chairs : curNum;
                    break;
                case "table":
                    curNum++;
                    curNum = curNum > tables ? curNum - tables : curNum;
                    break;
            }
            path += (curNum < 10 ? "0" + curNum.ToString() : curNum.ToString());
            GameObject obj = Resources.Load<GameObject>(path);
            Destroy(_change);
            _change = GameObject.Instantiate(obj);
            _change.name = type + (curNum < 10 ? "0" + curNum.ToString() : curNum.ToString());
            _change.transform.parent = parent;
            _change.transform.localPosition = pos;
            _change.AddComponent<Changer>();
        }
    }
}
