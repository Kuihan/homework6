using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : MonoBehaviour {
	private Vector3 speed;
	private bool canmove;
	private Vector3 p1, p2, p3, p4;
	private int steps;
	private Vector3 pos;
	public static float maxdis = 0;
	// Use this for initialization
	void Start () {
		canmove = true;
		setpoint ();
		steps = 0;
		this.transform.position = new Vector3 (0, 0.5f, 0);
		pos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (canmove && GameGUI.state == 1) {
			if (cancatch ())
				caught ();
			else
				lookaround ();
		}
	}

	//judge distance to player
	bool cancatch(){
		float x1 = this.transform.position.x, z1 = this.transform.position.z;
		float x2 = UnityChanControlScriptWithRgidBody.pos.x, z2 = UnityChanControlScriptWithRgidBody.pos.z;
		float dis = Mathf.Sqrt (Mathf.Pow(x1 - x2, 2) + Mathf.Pow(z1 - z2, 2));
		if (dis < maxdis)
			return true;
		return false;
	}

	void caught(){
		float hard = (float)Factory.level / 30;
		this.transform.LookAt (UnityChanControlScriptWithRgidBody.pos);
		speed = UnityChanControlScriptWithRgidBody.pos - this.transform.position;
		float mod = Mathf.Sqrt (Mathf.Pow(speed.x, 2) + Mathf.Pow(speed.y, 2) + Mathf.Pow(speed.z, 2));
		speed /= mod;
		this.transform.position += (3 + hard) * speed * Time.deltaTime;
	}

	void setpoint(){
		p1 = new Vector3 (0, 0, 3.2f) + this.transform.position;
		p2 = new Vector3 (0, 0, -3.2f) + this.transform.position;
		p3 = new Vector3 (3.2f, 0, 0) + this.transform.position;
		p4 = new Vector3 (-3.2f, 0, 0) + this.transform.position;
		p1.x += randomInt ();
		p2.x += randomInt ();
		p3.z += randomInt ();
		p4.z += randomInt ();
	}

	int randomInt(){
		if (this.name == "StoneMonster") {
			System.Random rd = new System.Random ();
			return rd.Next (-3, 3);
		} else if (this.name == "StoneMonster2") {
			System.Random rd = new System.Random ();
			return rd.Next (-2, 2);
		} else if (this.name == "StoneMonster3") {
			System.Random rd = new System.Random ();
			return rd.Next (-3, 3);
		}
		return 0;
	}

	void lookaround(){
		if (this.name == "StoneMonster") {
			if (judge ())
				steps++;
			if (steps == 0) {
				moveTo (p1);
			} else if (steps == 1) {
				moveTo (p3);
			} else if (steps == 2) {
				moveTo (p2);
			} else if (steps == 3) {
				moveTo (p4);
			} else {
				steps = 0;
			}
		} else if (this.name == "StoneMonster2") {
			if (judge ())
				steps++;
			if (steps == 0) {
				moveTo (p2);
			} else if (steps == 1) {
				moveTo (p3);
			} else if (steps == 2) {
				moveTo (p1);
			} else if (steps == 3) {
				moveTo (p4);
			} else {
				steps = 0;
			}
		} else if (this.name == "StoneMonster3") {
			if (judge ())
				steps++;
			if (steps == 0) {
				moveTo (p3);
			} else if (steps == 1) {
				moveTo (p2);
			} else if (steps == 2) {
				moveTo (p4);
			} else if (steps == 3) {
				moveTo (p1);
			} else {
				steps = 0;
			}
		}
	}

	void moveTo(Vector3 loc){
		float hard = (float)Factory.level / 30;
		this.transform.LookAt (loc);
		speed = loc - this.transform.position;
		float mod = Mathf.Sqrt (Mathf.Pow(speed.x, 2) + Mathf.Pow(speed.y, 2) + Mathf.Pow(speed.z, 2));
		speed /= mod;
		this.transform.position += (3 + hard) * speed * Time.deltaTime;
	}

	bool judge(){
		Vector3 loc = this.transform.position;
		bool outofbound = false;
		if (loc.x > pos.x + 3.2f) {
			loc.x = pos.x + 3.18f;
			outofbound = true;
		}
		if (loc.x < pos.x - 3.2f) {
			loc.x = -3.18f + pos.x;
			outofbound = true;
		}
		if (loc.z > 3.2f + pos.z) {
			loc.z = 3.18f + pos.z;
			outofbound = true;
		}
		if (loc.z < -3.2f + pos.z) {
			loc.z = -3.18f + pos.z;
			outofbound = true;
		}
		this.transform.position = loc;
		return outofbound;
	}

	//trigger
	void OnTriggerEnter(Collider c){
		if (c.gameObject.tag == "Player") {
			canmove = false;
			GameGUI.state = 2;
		}
	}
}
