﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {


	public float speed = 3.0f;
	// Use this for initialization
	void Start () {
		
	}

	void Update() {
		transform.Rotate(0, speed, 0,Space.World);
	}
}
