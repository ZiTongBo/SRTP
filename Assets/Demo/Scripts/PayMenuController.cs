//------------------------------------------------------------------------------
// Copyright 2016 Baofeng Mojing Inc. All rights reserved.
//------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;//引用UI命名空间

public class PayMenuController : MonoBehaviour {
	
	public GameObject[] Button_Object;
    MojingLoginPay loginpay;
	private int buttonCurIndex = -1;

    // Use this for initialization
    void Start () {
        HoverNext ();
        loginpay = FindObjectOfType<MojingLoginPay>();
    }
	
	public void HoverNext() {
		buttonCurIndex++;
		buttonCurIndex = buttonCurIndex % Button_Object.Length;
		
		for (int i = 0; i < Button_Object.Length; i++) {
			if(i != buttonCurIndex) {
				Button_Object[i].GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f);
				Button_Object[i].GetComponent<RectTransform>().sizeDelta = new Vector2 (400,60);
			}
			else {
				Button_Object[i].GetComponent<Image>().color = new Color(0.5f, 1.0f, 0.5f);
				Button_Object[i].GetComponent<RectTransform>().sizeDelta = new Vector2 (410,63);
			}
		}
	}
	
	public void HoverPrev() {
		buttonCurIndex--;
		if (buttonCurIndex < 0)
			buttonCurIndex = Button_Object.Length - 1;
		
		for (int i = 0; i < Button_Object.Length; i++) {
			if(i != buttonCurIndex) {
				Button_Object[i].GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f);
				Button_Object[i].GetComponent<RectTransform>().sizeDelta = new Vector2 (400, 60);
			}
			else {
				Button_Object[i].GetComponent<Image>().color = new Color(0.5f, 1.0f, 0.5f);
				Button_Object[i].GetComponent<RectTransform>().sizeDelta = new Vector2 (410, 63);
			}
			
		}
	}

	public void PressCurrent() {
		switch (buttonCurIndex) {
		    case 0:
                loginpay.SingleLogin();
			break;
            case 1:
                loginpay.DoubleLogin();
            break;
            case 2:
                loginpay.syncMjAppLoginState();
            break;
            case 3:
                loginpay.Register();
            break;
            case 4:
                loginpay.SinglePay();
            break;
            case 5:
                loginpay.DoublePay1();
            break;
            case 6:
                loginpay.DoublePay2();
                break;
            case 7:
                loginpay.GetBalance();
            break;
        }
	}
}
