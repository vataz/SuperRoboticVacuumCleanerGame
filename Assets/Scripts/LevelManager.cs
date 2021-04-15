using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    bool levelActive;
    public float timer = 0f;
    public float personalBest;

    public Text timerUI;
    public Text congratulationsText;
    public GameObject endGameWindow;

    public AudioManager am;
    public VacuumMovement vm;
    public LevelGenerator lg;

    // Start is called before the first frame update
    void Start() {
        lg.GenerateLevel();
        endGameWindow.SetActive(false);
        personalBest = PlayerPrefs.GetFloat("PersonalBest");
        if (personalBest <= 0)
            personalBest = 6000;
        levelActive = true;
        GameObject amGO = GameObject.Find("AudioManager");
        if (amGO != null) {
            am = amGO.GetComponent<AudioManager>();
            am.SetUpToggleButton();
        }
    }

    // Update is called once per frame
    void Update() {
        if (levelActive) {
            timer += Time.deltaTime;
            if (timer > 0)
                UpdateUI();
            else
                HideUI();
        }
    }

    public void EndLevel() {
        levelActive = false;
        vm.PauseMovement(-1);
        if (timer < personalBest) {
            PlayerPrefs.SetFloat("PersonalBest", timer);
            congratulationsText.text = "Congratulations, you have beaten your personal best!\nNew Perosnal Best Time: " + timer.ToString("0.00");
        }
        else {
            congratulationsText.text = "You were slower than your personal best\nPerosnal Best Time: " + personalBest.ToString("0.00");
        }
        endGameWindow.SetActive(true);
    }

    void UpdateUI() {
        timerUI.text = timer.ToString("0.00");
    }

    void HideUI() {
        timerUI.text = "";
    }

    public void SetTImerDelay(float delay) {
        timer -=delay;
    }

    
}
