using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class ControllerDemo : MonoBehaviour {
    public Text text;
    // 魔镜控制设备
    public GameObject[] weaponList = new GameObject[2];
    public LineRenderer[] lineObj= new LineRenderer[2];
    Vector3 origin = new Vector3(0, 8.0f, 1);
    Vector3 target = new Vector3(0, 8.0f, 1);

    void Update () 
    {
        for (int id = 0; id < weaponList.Length; id++)
        {
            updateRay(weaponList[id], id);
        }

        StringBuilder _sb = new StringBuilder();
        _sb.Remove(0, _sb.Length);
        _sb.AppendFormat("({0:0.00000}, {1:0.00000}, {2:0.00000}, {3:0.00000})", MojingController.QuartArray[0].x, MojingController.QuartArray[0].y, MojingController.QuartArray[0].z, MojingController.QuartArray[0].w);

        text.text = "Quaternion:" + _sb.ToString()
        + "\n" + "LinearArray:" + MojingController.LinearArray[0]
        + "\n" + "AngularArray:"+ MojingController.AngularArray[0]
        +"\n" + "FixQuate:" + MojingController.FixQuate[0];
    }

    void updateRay(GameObject weapon, int id)
    {
        //Box
        weapon.transform.rotation = MojingController.QuartArray[id];
        //Ray
        target = weapon.transform.forward * 300;
        if (null != lineObj[id])
        {
            lineObj[id].SetPosition(0, origin);
            lineObj[id].SetPosition(1, target);
        }
    }
}
