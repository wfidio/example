using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController: MonoBehaviour {


	[SerializeField] private GameObject enermyPrefab; // Serialized variable for linking to the prefab object
	private GameObject _enermy;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (_enermy == null) {
			_enermy = Instantiate<GameObject> (enermyPrefab);
			_enermy.transform.position = new Vector3 (0, 2.5f, 0);
			float angle = Random.Range (0, 360);
			_enermy.transform.Rotate (0, angle, 0);
		}
	}
}
