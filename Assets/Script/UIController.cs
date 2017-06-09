using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour {

	[SerializeField] private Text scoreLabel;
	[SerializeField] private SettingsPopUp SettingsPopup;

	private int _score;
	void Awake(){
		Messenger.AddListener (GameEvent.ENEMY_HIT,OnEnermyHit);
	}

	void OnDestroy(){
		Messenger.RemoveListener (GameEvent.ENEMY_HIT,OnEnermyHit);
	}

	// Use this for initialization
	void Start () {
		_score = 0;
		scoreLabel.text = _score.ToString ();
		SettingsPopup.Close ();
	}

	// Update is called once per frame
	void Update () {
//		scoreLabel.text = Time.realtimeSinceStartup.ToString ();
	}

	public void OnOpenSettings(){
		SettingsPopup.Open ();
//		Debug.Log ("open setting");
	}

	public void OnCloseSettings(){
		SettingsPopup.Close ();
	}

	private void OnEnermyHit(){
		_score += 1;
		scoreLabel.text = _score.ToString ();
	}
}
