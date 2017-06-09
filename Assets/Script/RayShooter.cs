using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayShooter : MonoBehaviour {


	private Camera _camera;
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioClip hitWallSound;
    [SerializeField] private AudioClip hitEnermySound;


	// Use this for initialization
	void Start () {
		_camera = GetComponent<Camera> ();
//		Cursor.lockState = CursorLockMode.Locked;
//		Cursor.visible = false;
	}


	// Update is called once per frame
	void Update () {
		
		if (Input.GetMouseButtonDown (0)&&!EventSystem.current.IsPointerOverGameObject()) {
			Vector3 shootPoint = new Vector3 (_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
//			Vector3 shootPoint = new Vector3(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"),0);
			Ray ray = _camera.ScreenPointToRay (shootPoint); //from the Screen Coordinate to the global Coordinate
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
//				Debug.Log ("Hit " + hit.point);

				//Coroutines are a Unity-specific way of handling tasks that execute incrementally over time,
				//as opposed to how most functions make the program wait until they finish
				GameObject hitObject = hit.transform.gameObject;
				ReactiveTarget reactiveTarget = hitObject.GetComponent<ReactiveTarget> ();
				if (reactiveTarget != null) {
//					Debug.Log ("hit target");
					reactiveTarget.ReactToHit();
					Messenger.Broadcast (GameEvent.ENEMY_HIT);
                    soundSource.PlayOneShot(hitEnermySound);
                    //soundSource.clip = hitEnermySound;
                    //soundSource.Play();
                } else {
					StartCoroutine (SphereIndicator (hit.point));
                    soundSource.PlayOneShot(hitWallSound);
				}
			}
		}
	}

	private IEnumerator SphereIndicator(Vector3 vector){
		GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		sphere.transform.position = vector;
		yield return new WaitForSeconds(1); //The yield keyword tells coroutines where to pause.

		Destroy(sphere);
	}

	void OnGUI(){
		int size = 12;
		float posX = _camera.pixelWidth / 2;
		float posY = _camera.pixelHeight / 2;
		GUI.Label (new Rect (posX, posY, size, size), "*");
	}
}

