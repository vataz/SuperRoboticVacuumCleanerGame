using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DebugManager : MonoBehaviour {
    public bool opened;
    public GameObject menu;
    public VacuumMovement vm;

    // Start is called before the first frame update
    void Start() {
        opened = false;
        UpdateMenuDisplay();
    }

    // Update is called once per frame
    void Update() {

    }

    public void OpenMenu() {
        opened = true;
        UpdateMenuDisplay();
        vm.PauseMovement(-1);
    }

    public void CloseMenu() {
        opened = false;
        UpdateMenuDisplay();
        vm.PauseMovement(0.5f);
    }

    void UpdateMenuDisplay() {
        menu.SetActive(opened);
    }

}
