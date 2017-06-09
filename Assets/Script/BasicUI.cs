using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI(){
		if (GUI.Button (new Rect (10, 10, 40, 20), "Test")) {
			Debug.Log ("Test");
		}
	}
}
