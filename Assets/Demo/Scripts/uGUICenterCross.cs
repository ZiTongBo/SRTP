//------------------------------------------------------------------------------
// Copyright 2016 Baofeng Mojing Inc. All rights reserved.
//------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
public class uGUICenterCross : MonoBehaviour 
{
    private GameObject _mainCamera;
    //private UISprite _rotateSprite;
    //private UISprite _dotSprite;
    private Transform _rotateSprite;
    private Transform _dotSprite;
    private uGUICrossNodeBase _selectedNode;
    private float _enterTime;
    private Transform _head;
 	private bool _haveGrab;
    private Image image;
    void Awake()
    {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        _head = transform.Find("head");
        _rotateSprite = _head.Find("waitting");
        _rotateSprite.gameObject.SetActive(false);
        _dotSprite = _head.Find("click");
        _dotSprite.gameObject.SetActive(true);
        transform.SetParent(_mainCamera.transform.parent);
        transform.position = _mainCamera.transform.position + new Vector3(0,0,1.49f);
        EnableControl(true);
        image = _rotateSprite.GetComponent<Image>();
    }
	
	void Update ()
	{
        SetSelectedNode();
        if (_haveGrab)
            return;
        _rotateSprite.gameObject.SetActive(false);
        _dotSprite.gameObject.SetActive(true);
        //_rotateSprite.fillAmount = 0;
        if (_selectedNode != null && _selectedNode.Clickable)
	    {
            var selectTime = Time.time - _enterTime;
	        if (CheckTouch())
	        {
	            selectTime = float.MaxValue;
	        }
	        if (selectTime > _selectedNode.HoverTime)
	        {
	            if (selectTime - _selectedNode.HoverTime < _selectedNode.ClickTime)
	            {
					if(_selectedNode.ShowWaiting)
                    { 
	                	_rotateSprite.gameObject.SetActive(true);
                        _dotSprite.gameObject.SetActive(false);
                    }
	                var passedClickTime = selectTime - _selectedNode.HoverTime;
	                image.fillAmount = passedClickTime/_selectedNode.ClickTime;
	            }
	            else
	            {
	                _selectedNode.OnClick();
                    if (_selectedNode.Grabable)
                        _haveGrab = true;
                    else
	                    _selectedNode = null;
	            }
	        }
	    }
	}

    /// <summary>
    /// 是否启用头控
    /// </summary>
    /// <param name="enable"></param>
    public void EnableControl(bool enable) 
    {
        if(_head == null)
        {
            _head = transform.Find("head");
            if (_head == null)
                return;
        }
        _head.gameObject.SetActive(enable);
        if(!enable)
        {
            if (_selectedNode != null)          //disable select also
                _selectedNode.SetSelect(false);
        }
    }

    private void SetSelectedNode()  
    {
        uGUICrossNodeBase currentNode = null;
        RaycastHit hit;
        var forward = _mainCamera.transform.forward;
        if (Physics.Raycast(transform.position, forward, out hit, 1000))
        {
            currentNode = hit.collider.gameObject.GetComponent<uGUICrossNodeBase>();
        }

        if (currentNode != _selectedNode)
        {
            _haveGrab = false;
            if (_selectedNode != null)
            {
                _selectedNode.SetSelect(false);
            }
            _selectedNode = currentNode;
            _enterTime = Time.time;
			if (_selectedNode != null)
			{
				_selectedNode.SetSelect(true);
            }
        }
    }

    private bool CheckTouch()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
            return true;
#endif
        if (Input.touchSupported && Input.touchCount > 0)
        {
            var touch = Input.touches[0];
            Debug.Log(touch.phase);
            return touch.phase == TouchPhase.Began;
        }
        return false;
    }
}
