using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour {

    GameObject FPS;
    GameObject plane;
    float mapWidth;
    float mapHeight;
    float widthCheck;
    float heightCheck;
    float miniMap_x;
    float miniMap_y;
    public Texture map;
    public Texture map_cube;
	// Use this for initialization
	void Start () {
        FPS = GameObject.Find("RigidBodyFPSController");
        plane = GameObject.Find("plane");
        float size_x = plane.GetComponent<MeshFilter>().mesh.bounds.size.x;
        float scal_x = plane.transform.localScale.x;
        float size_z = plane.GetComponent<MeshFilter>().mesh.bounds.size.z;
        float scal_z = plane.transform.localScale.z;

        mapWidth = size_x * scal_x;
        mapHeight = size_z * scal_z;
        check();
    }
	
    void check()
    {
        float x = FPS.transform.position.x;
        float z = FPS.transform.position.z;
        
        FPS.transform.position = new Vector3(x, FPS.transform.position.y, z);
        miniMap_x = (map.width / mapWidth * x) + ((map.width / 2) - (map_cube.width / 2)) + (Screen.width - map.width);
        miniMap_y = map.height - ((map.height / mapHeight * z) + (map.height / 2));
    }
	// Update is called once per frame
	void FixedUpdate () {
        check();
	}

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(Screen.width - map.width, 0, map.width, map.height), map);
        GUI.DrawTexture(new Rect(miniMap_x + 100, miniMap_y, map_cube.width/10, map_cube.height/10), map_cube);
        GUI.Box(new Rect(Screen.width - map.width/2 - 30, map.height, 60, 30), "小地图");
    }
}
