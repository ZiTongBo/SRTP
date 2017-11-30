using UnityEngine;
using System.Collections;

public class Blinker : MonoBehaviour {
    private bool _flag = true;
    private float _time = 0f;
    private GameObject _blink = null;

// Use this for initialization
void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Selector._select != _blink)
        {
            if (_blink)
            {
                _blink.GetComponent<Renderer>().enabled = true;
                Renderer[] rendererComponents = _blink.GetComponentsInChildren<Renderer>(true);
                
                // Enable rendering:  
                foreach (Renderer component in rendererComponents)
                {
                    component.enabled = true;
                }
                
            }
            _time = 0;
            _flag = true;
        }

        _blink = Selector._select;

        if (_blink)
        {
            if (!Timer.getTimer())
            {
                if (_time < 0.5f)
                    _time += Time.deltaTime;
                else
                {
                    _time = 0;
                    _flag = !_flag;

                    _blink.GetComponent<Renderer>().enabled = _flag;
                    Renderer[] rendererComponents = _blink.GetComponentsInChildren<Renderer>(true);
                    
                    // Enable rendering:  
                    foreach (Renderer component in rendererComponents)
                    {
                        component.enabled = _flag;
                    }
                    
                    Debug.Log("blink");
                }
            }
            else
            {
                _blink.GetComponent<Renderer>().enabled = true;
                Renderer[] rendererComponents = _blink.GetComponentsInChildren<Renderer>(true);
                
                // Enable rendering:  
                foreach (Renderer component in rendererComponents)
                {
                    component.enabled = true;
                }

                _time = 0;
                _flag = true;
            }
        }
	}
}
