using UnityEngine;
using System.Collections;

public class Rotater : MonoBehaviour {
    private GameObject _rotate = null;
    private float rotateSpeed = 5.0f;
    GameObject FPS;
	// Use this for initialization
	void Start () {
        FPS = GameObject.Find("RigidBodyFPSController");
        _rotate = Selector._select;
        Debug.Log("rotate");
	}
	
	// Update is called once per frame
	void Update () {
        float x = Input.GetAxis("Mouse X") * rotateSpeed;
        if (_rotate.GetComponent<MeshRenderer>().materials.Length > 1)
        {
            _rotate.transform.Rotate(new Vector3(0, 0, x));
        }
        else
        {
            _rotate.transform.Rotate(new Vector3(0, x, 0));
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("done");
            Selector.RestartSelector();
            Destroy(_rotate.GetComponent<Rotater>());
            gameState.State = 0;
            FPS.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().enabled = true;
            Cursor.visible = false;
        }
    }
}
