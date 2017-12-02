using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class Selector : MonoBehaviour {
    public static GameObject _select = null;
    public static bool _work = true;

    public static void LockSelector()
    {
        _work = false;
    }

    public static void RestartSelector()
    {
        _work = true;
    }

    private static bool canSelect(GameObject o)
    {
        if (o.name == "room" || o.name == "plane" || o.name == "Plane")
            return false;
        if (Regex.IsMatch(o.name, "wall*"))
            return false;

        return true;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!_work)
        {
            if(gameState.State!=1 && gameState.State != 2 && gameState.State != 3 && gameState.State != 4 && gameState.State != 5)
            {
                _select = null;
            }
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {            
            if (_select == null && canSelect(hit.collider.gameObject))
            {
                _select = hit.collider.gameObject;
                Debug.Log("name: " + _select.name);                
            }
            else
            {
                if (hit.collider.gameObject != _select && canSelect(hit.collider.gameObject))
                {
                    _select = hit.collider.gameObject;
                    Debug.Log("name: " + _select.name);
                }else if(hit.collider.gameObject != _select)
                {
                    _select = null;
                }
            }
        }
	}
}
