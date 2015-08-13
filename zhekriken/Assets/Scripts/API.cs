using UnityEngine;

public class API : MonoBehaviour {
    private static API instance;
    public AudioClip BackgroundSound;
    public AudioClip BubbleCrashSound;
    public AudioClip BubbleShootSound;
    public AudioClip ExplosionSound;

    private AudioSource _backgroundSound;
    private AudioSource _sound;
    private float _volumeBackground = 100;
    private float _volumeSounds = 100;

    public static API Instance {
        get {
            if (instance == null) {
                var go = new GameObject("API", typeof (API));
                instance = go.GetComponent<API>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    private void Awake() {
        instance = this;
        PlayBackground(BackgroundSound);
    }

    public void PlayBackground(AudioClip audio) {
        if (_backgroundSound == null) {
            _backgroundSound = gameObject.AddComponent<AudioSource>();
        }
        _backgroundSound.volume = _volumeBackground;
        _backgroundSound.loop = true;
        _backgroundSound.clip = audio;
        _backgroundSound.Play();
    }

    public void PlaySound(AudioClip audio) {
        if (audio != null) {
            _sound = gameObject.AddComponent<AudioSource>();
            _sound.loop = false;
            _sound.clip = audio;
            _sound.volume = _volumeSounds;
            _sound.Play();
        }
    }
}