using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SFXManager : MonoBehaviour {
    AudioSource audioSource;
    bool isMuted = false;
    void Start() {
        audioSource = GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("SFXMuted") == 0)
            isMuted = false;
        else
            isMuted = true;
        AdjustVolume();

        Button toggleButton = GameObject.Find("SFXToggleButton").GetComponent<Button>();
        toggleButton.onClick.AddListener(ToggleIsMuted);
    }


    public void ToggleIsMuted() {
        isMuted = !isMuted;
        AdjustVolume();
    }

    public void AdjustVolume() {
        if (isMuted) {
            PlayerPrefs.SetInt("SFXMuted", 1);
            audioSource.volume = 0f;
        }
        else {
            PlayerPrefs.SetInt("SFXMuted", 0);
            audioSource.volume = 1f;
        }
    }

    public void PlaySFX(AudioClip ac) {
        audioSource.PlayOneShot(ac);
    }
}
