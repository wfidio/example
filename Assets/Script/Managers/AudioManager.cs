using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour,IGameManager
{

    public ManagerStatus status { get; private set; }

    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioSource music1Source;
    [SerializeField] private string introBGMusic;
    [SerializeField] private string levelBGMusic;

    [SerializeField] private AudioSource music2Source;

    private AudioSource _activeSource;
    private AudioSource _inactiveSource;

    public float crossFadeRate = 1.5f;
    private bool _crossFading; // a flag shows the music is fading


    public float SoundVolume
    {
        get { return AudioListener.volume; }
        set
        {
            AudioListener.volume = value;           
        }
    }

    public bool SoundMute
    {
        get { return AudioListener.pause; }
        set { AudioListener.pause = value; }
    }

    private float _musicVolume;

    public float MusicVolume
    {
        get { return _musicVolume; }
        set
        {
            _musicVolume = value;
            if (music1Source != null && !_crossFading)
            {
                music1Source.volume = _musicVolume;
                music2Source.volume = _musicVolume;
            }
        }
    }

    public bool MusicMute
    {
        get
        {
            if (music1Source != null)
            {
                return music1Source.mute;
            }
            return false;
        }

        set
        {
            if (music1Source != null)
            {
                music1Source.mute = value;
                music2Source.mute = value;
            }
        }
    }
    public void startUp()
    {
        Debug.Log("Audio Manager Starting");

        music1Source.ignoreListenerVolume = true;
        music1Source.ignoreListenerPause = true;
        music2Source.ignoreListenerVolume = true;
        music2Source.ignoreListenerPause = true;


        SoundVolume = 1f;
        MusicVolume = 1f;

        _activeSource = music1Source;
        _inactiveSource = music2Source;

        status = ManagerStatus.started;
    }

    private IEnumerator CrossFadeMusic(AudioClip clip)
    {
        _crossFading = true;

        _inactiveSource.clip = clip;
        _inactiveSource.volume = 0;
        _inactiveSource.Play();

        float scaledRate = crossFadeRate * _musicVolume;
        while (_activeSource.volume > 0)
        {
            _activeSource.volume -= scaledRate * Time.deltaTime;
            _inactiveSource.volume += scaledRate * Time.deltaTime;

            yield return null;
        }

        AudioSource temp = _activeSource;
        _activeSource = _inactiveSource;
        _activeSource.volume = MusicVolume;

        _inactiveSource = temp;
        _inactiveSource.Stop();

        _crossFading = false;
    }


    public void PlaySound(AudioClip clip)
    {
        soundSource.PlayOneShot(clip);
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayMusic(AudioClip clip)
    {
        //music1Source.clip = clip;
        //music1Source.Play();

        if (_crossFading) { return; }
        StartCoroutine(CrossFadeMusic(clip));
    }


    public void PlayIntroMusic()
    {
        PlayMusic(Resources.Load<AudioClip>("Music/" + introBGMusic));
    }

    public void PlayLevelMusic()
    {
        PlayMusic(Resources.Load<AudioClip>("Music/" + levelBGMusic));
    }

    public void StopMusic()
    {
        //music1Source.Stop();
        _activeSource.Stop();
        _inactiveSource.Stop();
    }
}
