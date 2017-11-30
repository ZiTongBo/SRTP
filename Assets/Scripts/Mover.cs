using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {
    private GameObject _move = null;
    GameObject FPS;
    // Use this for initialization
    void Start () {
        FPS = GameObject.Find("RigidBodyFPSController");
        _move = Selector._select;
        Debug.Log("move");
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        Vector3 pos = _move.transform.position;
        pos.x = hit.point.x;
        pos.z = hit.point.z;
        _move.transform.position = pos;
        
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("done");
            Selector.RestartSelector();
            Destroy(_move.GetComponent<Mover>());
            gameState.State = 0;
            Selector._select = null;
            FPS.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().enabled = true;
            Cursor.visible = false;
        }
	}
   
}
