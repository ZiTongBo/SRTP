using UnityEngine;
using System.Collections;

public class DelayDisable : MonoBehaviour {

	// Use this for initialization
    float showTime;
	void Start () {
	
	}

    void OnEnable()
    {
        showTime = Time.realtimeSinceStartup;
    }

	
    void DisableSelf()
    {
        gameObject.SetActive(false);
    }

	// Update is called once per frame
	void Update () {

        if (Time.realtimeSinceStartup - showTime > 5)
            DisableSelf();
	
	}
}
