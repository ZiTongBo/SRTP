//------------------------------------------------------------------------------
// Copyright 2016 Baofeng Mojing Inc. All rights reserved.
// 
// Author: Xu Xiang
//------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using System;

public class ViewLock : MonoBehaviour
{
    private float distance = 1.5f;
    Transform vrHead;
    Material OldMateral;
    GameObject SelObj;
    Camera LCamera;
    Camera MainCamera;
    Vector2 mFilteredVelocity;
    Vector2 mFilteredPosition;
    float mJitterReduction = 1;
    float mLagReduction = 1;

    public Transform EventCamera;
    void Start()
    {
        LCamera = GameObject.Find("MojingMain/MojingVrHead/VR Camera Left").GetComponent<Camera>();
        MainCamera = Mojing.SDK.getMainCamera();
        vrHead = transform.parent;
    }

    private float distance_default = 1.5f;

    void Update()
    {
        if (this == null)
        {
            return;
        }
        if (gameObject.activeInHierarchy)
        {
            if (MainCamera != null)
            {
                float dist = distance;
                var rot = Mojing.SDK.headPose.Orientation;
                EventCamera.localRotation = rot;
                //Vector3 targetPos = new Vector3(Mathf.Tan(angle.y * Mathf.Deg2Rad) * distance, Mathf.Tan(angle.x * Mathf.Deg2Rad) * distance, distance);
                Vector3 lCameraPos = LCamera.transform.position;
                Quaternion rotation = LCamera.transform.localRotation;
                LCamera.transform.position = Vector3.zero;
                LCamera.transform.localRotation = rot;

                Vector3 screenPos = LCamera.WorldToScreenPoint(new Vector3(0, 0, dist));

                
                LCamera.transform.localRotation = rotation;
                Vector3 targetPos = LCamera.ScreenToWorldPoint(screenPos);

                LCamera.transform.position = lCameraPos;
                Vector3 dir = targetPos;
                dir.y *= -1;
                dir.x *= -1;
                Debug.DrawRay(MainCamera.transform.position, dir * 1000, Color.red);

                RaycastHit hit;

                if (Physics.Raycast(MainCamera.transform.position, dir, out hit, 1000))
                {
                    GameObject colliderObj = hit.collider.gameObject;

                    if (SelObj != colliderObj && colliderObj != null)
                    {
                        if (OldMateral != null)
                        {
                            OldMateral.SetColor("_Color", Color.red);
                        }

                        SelObj = colliderObj;
                        OldMateral = SelObj.GetComponent<MeshRenderer>().material;
                        OldMateral.SetColor("_Color", Color.green);
                    }


                }
                else if (OldMateral)
                {

                    OldMateral = SelObj.GetComponent<MeshRenderer>().material;
                    OldMateral.SetColor("_Color", Color.red);
                    SelObj = null;
                    OldMateral = null;


                }

                Vector3 angle = rot.eulerAngles;
                dist = dist / Mathf.Abs(Mathf.Cos(Mathf.RoundToInt(angle.y) * Mathf.Deg2Rad));
                distance_default = dist;
                transform.position = MainCamera.transform.position + targetPos.normalized * dist;
                SetOverlay.HeadCtrlOffset = new Vector2(Mathf.Round(screenPos.x), Mathf.Round(screenPos.y));
            }
        }
    }
}
