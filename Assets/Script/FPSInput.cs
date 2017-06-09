using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Controll Script/FPSInput")]

public class FPSInput : MonoBehaviour {

	public float speed = 6.0f;

	private CharacterController _charController; // add the physic attributions

	private float gravity = -9.8f;  // the influence of gravity
	// Use this for initialization

	private const float baseSpeed = 6.0f;
	void Start () {
		_charController = GetComponent<CharacterController> ();
	}

	void Awake(){
		Messenger<float>.AddListener (GameEvent.SPEED_CHANGED,OnSpeedChanged);
	}

	void OnDestroy(){
		Messenger<float>.RemoveListener (GameEvent.SPEED_CHANGED,OnSpeedChanged);
	}
	// Update is called once per frame
	void Update () {
		float deltaX = Input.GetAxis ("Horizontal") * speed;
		float deltaZ = Input.GetAxis ("Vertical") * speed;

		Vector3 movement = new Vector3 (deltaX, 0, deltaZ);
		movement = Vector3.ClampMagnitude (movement, speed); 

		movement *= Time.deltaTime;
		movement = transform.TransformDirection (movement);  //change the global coordinate to local coordinate
		movement.y = gravity;  

		_charController.Move (movement);

//		transform.Translate (deltaX*Time.deltaTime, 0, deltaZ*Time.deltaTime);
	}

	private void OnSpeedChanged(float value){
		speed = baseSpeed * value;
	}
}
