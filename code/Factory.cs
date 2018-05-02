using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour {
	public static bool isexist;
	public static int level;
	public static GameObject go;
	public static GameObject ca;
	// Use this for initialization
	void Start () {
		isexist = false;
		level = 0;
		ca = Instantiate(Resources.Load("Prefabs/Camera"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameGUI.state == 1 && level < 24 && isexist == false) {
			//random room
			System.Random rd = new System.Random ();
			go = Instantiate (Resources.Load ("Prefabs/Room" + rd.Next(1, 10)), new Vector3 (0, 0.5f, 0), Quaternion.identity) as GameObject;
			level++;
			isexist = true;
		} else if (GameGUI.state == 1 && level == 24 && isexist == false) {
			go = Instantiate (Resources.Load ("Prefabs/Room10"), new Vector3 (0, 0.5f, 0), Quaternion.identity) as GameObject;
			level++;
			isexist = true;
		}
	}

	void OnEnable(){
		GameGUI.OnStart += Teleport;
	}

	void OnDisable(){
		GameGUI.OnStart -= Teleport;
	}

	void Teleport(){
		go = Instantiate(Resources.Load("Prefabs/Room1"), new Vector3(0, 0.5f, 0), Quaternion.identity) as GameObject;
		level++;
		isexist = true;
	}
}
