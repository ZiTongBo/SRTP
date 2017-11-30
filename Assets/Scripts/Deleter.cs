using UnityEngine;
using System.Collections;

public class Deleter : MonoBehaviour {
    private GameObject _delete = null;
    GameObject FPS;
	// Use this for initialization
	void Start () {
        FPS = GameObject.Find("RigidBodyFPSController");
        _delete = Selector._select;
        Debug.Log("delete");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("done");
            Selector.RestartSelector();
            Destroy(_delete);
            gameState.State = 0;
            FPS.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().enabled = true;
            Cursor.visible = false;
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("done");
            Selector.RestartSelector();
            Destroy(_delete.GetComponent<Deleter>());
            gameState.State = 0;
            FPS.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().enabled = true;
            Cursor.visible = false;
        }
    }
}
