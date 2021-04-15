using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    private static AudioManager _instance;
    AudioSource audioSource;
    bool isMuted = false;
    void Awake() {
        if (!_instance)
            _instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        audioSource = GetComponent<AudioSource>();

        if (PlayerPrefs.GetInt("MusicMuted") == 0)
            isMuted = false;
        else
            isMuted = true;
        AdjustVolume();
        SetUpToggleButton();

    }

    public void SetUpToggleButton() {
        Button toggleButton = GameObject.Find("MusicToggleButton").GetComponent<Button>();
        toggleButton.onClick.AddListener(ToggleIsMuted);
    }

    public void ToggleIsMuted() {
        isMuted = !isMuted;
        AdjustVolume();
    }
    public void AdjustVolume() {
        if (isMuted) {
            PlayerPrefs.SetInt("MusicMuted", 1);
            audioSource.volume = 0f;
        }
        else {
            PlayerPrefs.SetInt("MusicMuted", 0);
            audioSource.volume = 0.5f;
        }
    }
}
