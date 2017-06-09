using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour {

	private bool _alive;
	public float speed = 3.0f;
	public float obstacleRange = 3.0f;
	[SerializeField] private GameObject fireBallPrefab;
	private GameObject _fireball;
	public const float baseSpeed = 3.0f;

	// Use this for initialization
	void Start () {
		_alive = true;
	}

	void Awake(){
		Messenger<float>.AddListener (GameEvent.SPEED_CHANGED,onSpeedChanged);
	}

	void OnDestroy(){
		Messenger<float>.RemoveListener (GameEvent.SPEED_CHANGED,onSpeedChanged);
	}

	private void onSpeedChanged(float value){
		speed = baseSpeed * value;
	}

	// Update is called once per frame
	void Update () {
		if (_alive) {
			transform.Translate (0, 0, speed * Time.deltaTime);

			Ray ray = new Ray (transform.position, transform.forward);
			RaycastHit hit;

			if (Physics.SphereCast (ray, 0.75f, out hit)) {
				if (_fireball == null) {
					_fireball = Instantiate<GameObject> (fireBallPrefab);
					_fireball.transform.position = transform.TransformPoint (Vector3.forward * 1.5f);
					_fireball.transform.rotation = transform.rotation;
				}
			else 
//
//				Debug.Log((float)hit.distance);
//				Debug.Log(hit.distance-obstacleRange);
				if ((hit.distance-obstacleRange)<0) {
					float angle = Random.Range (-110, 110);
					transform.Rotate (0, angle, 0);
//					Debug.Log (hit.distance);
				}
			}
		}
	}

	public void setAlive(bool alive){
		_alive = alive;
	}
}
