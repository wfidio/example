using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPopUp : MonoBehaviour {

	[SerializeField] private  Slider speedSlider;
    [SerializeField] private AudioClip sound;
   


	// Use this for initialization
	void Start () {
		speedSlider.value = PlayerPrefs.GetFloat ("speed", 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Open(){
		gameObject.SetActive (true);
	}

	public void Close(){
		gameObject.SetActive (false);
	}

	public void OnSubmitName(string name){
		Debug.Log (name);
	}

	public void OnSpeedValue(float speed){
		Messenger<float>.Broadcast (GameEvent.SPEED_CHANGED,speed);
//		Debug.Log ("Speed:" + speed);
	}

    public void OnSoundToggle()
    {
        Managers.Audio.SoundMute = !Managers.Audio.SoundMute;
        Managers.Audio.PlaySound(sound);
    }

    public void OnSoundValue(float volume)
    {
        Managers.Audio.SoundVolume = volume;
    }

    public void OnPlayMusic(int selector)
    {
        switch (selector)
        {
            case 1:
                Managers.Audio.PlayIntroMusic();
                break;
            case 2:
                Managers.Audio.PlayLevelMusic();
                break;
            default:
                Managers.Audio.StopMusic();
                break;
        }
    }

    public void OnMusicToggle()
    {
        Managers.Audio.MusicMute = !Managers.Audio.MusicMute;
    }

    public void OnMusicValue(float volume)
    {
        Managers.Audio.MusicVolume = volume;
    }
}
