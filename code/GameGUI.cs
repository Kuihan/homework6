using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGUI : MonoBehaviour {
	public static int state;
	// Use this for initialization
	public delegate void GameStart();
	public static event GameStart OnStart;

	float width, height;

	float castw(float scale)
	{
		return (Screen.width - width) / scale;
	}

	float casth(float scale)
	{
		return (Screen.height - height) / scale;
	}

	void Start () {
		state = 0;
		width = Screen.width / 12;
		height = Screen.height / 12;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI(){
		GUI.Box (new Rect (0, 0, width, height), "Score:" + ScoreRecorder.score);
		if (state == 0) {
			Texture2D img = Resources.Load ("Image/chan2") as Texture2D;
			string aa = "";
			GUIStyle bb = new GUIStyle ();
			bb.normal.background = img;
			GUI.Label (new Rect (0, 0, Screen.width, Screen.height), aa, bb);
			Texture2D image = Resources.Load ("Image/title") as Texture2D;
			width = 400;
			height = 167;
			GUI.Label (new Rect (castw (2f), casth (4f), width, height), image);
			width = Screen.width / 12;
			height = Screen.height / 12;
			if (GUI.Button (new Rect (castw (2f), casth (2f), width, height), "Start")) {
				state = 4;
			} else if (GUI.Button (new Rect (castw (2f), casth (2f) + casth (4f), width, height), "Exit")) {
				Application.Quit ();
			}
		} else if (state == 1) {
			if (GUI.Button (new Rect (castw (2f), 0, width, height), "Back")) {
				Application.LoadLevel (Application.loadedLevelName);
			} else if (GUI.Button (new Rect (castw (2f), height, width, height), "Pause")) {
				if (Time.timeScale == 1) {
					Time.timeScale = 0;
					GUIStyle bigfont = new GUIStyle ();
					bigfont.fontSize = 48;
					GUI.Label (new Rect (castw (2f), casth (2f), width, height), "Pause", bigfont);
				} else if (Time.timeScale == 0) {
					Time.timeScale = 1;
				}
			}
			GUI.Label (new Rect (castw (2f), height * 2, width, height), "Level " + Factory.level);
		} else if (state == 2) {
			GUIStyle bigfont = new GUIStyle ();
			bigfont.fontSize = 48;
			width += 140;
			GUI.Label (new Rect (castw (2f), casth (4f), width, height), "<color=#FFFF00>Gameover</color>", bigfont);
			width -= 140;
			if (GUI.Button (new Rect (castw (2f), casth (2f), width, height), "Menu")) {
				Application.LoadLevel (Application.loadedLevelName);
			}
		} else if (state == 3) {
			GUIStyle bigfont = new GUIStyle ();
			bigfont.fontSize = 48;
			width += 140;
			GUI.Label (new Rect (castw (2f), casth (4f), width, height), "<color=#FFFF00>You win!</color>", bigfont);
			width -= 140;
			if (GUI.Button (new Rect (castw (2f), casth (2f), width, height), "Menu")) {
				Application.LoadLevel (Application.loadedLevelName);
			}
		} else if (state == 4) {
			Texture2D img = Resources.Load ("Image/chan2") as Texture2D;
			string aa = "";
			GUIStyle bb = new GUIStyle ();
			bb.normal.background = img;
			GUI.Label (new Rect (0, 0, Screen.width, Screen.height), aa, bb);
			if (GUI.Button (new Rect (castw (2f), casth (4f), width, height), "Easy")) {
				Destroy (Factory.ca);
				if (OnStart != null)
					OnStart ();
				GuardController.maxdis = 0.1f;
				state = 1;
			} else if (GUI.Button (new Rect (castw (2f), casth (2f), width, height), "Normal")) {
				Destroy (Factory.ca);
				if (OnStart != null)
					OnStart ();
				GuardController.maxdis = 1;
				state = 1;
			} else if (GUI.Button (new Rect (castw (2f), casth (2f) + casth (4f), width, height), "Hard")) {
				Destroy (Factory.ca);
				if (OnStart != null)
					OnStart ();
				GuardController.maxdis = 2;
				state = 1;
			}
		}
	}
}
