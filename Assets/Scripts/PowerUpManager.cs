using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {

    public float powerUpDuration;

    float timerSpeed;
    float timerRotation;
    float timerSize;

    bool isSpeedPowerUpActive;
    bool isRotationPowerUpActive;
    bool isSizePowerUpActive;

    public Text notification;

    VacuumMovement vm;

    // Start is called before the first frame update
    void Start() {
        notification.text = "";
        vm = GetComponent<VacuumMovement>();
    }

    // Update is called once per frame
    void Update() {
        if (timerSpeed > 0) {
            timerSpeed -= Time.deltaTime;
        }
        else if (isSpeedPowerUpActive) {
            vm.NormalizeSpeed();
            notification.text = "";
            isSpeedPowerUpActive = false;
        }

        if (timerRotation > 0) {
            timerRotation -= Time.deltaTime;
        }
        else if (isRotationPowerUpActive) {
            vm.NormalizeRotationSpeed();
            notification.text = "";
            isRotationPowerUpActive = false;
        }

        if (timerSize > 0) {
            timerSize -= Time.deltaTime;
        }
        else if (isSizePowerUpActive) {
            vm.NormalizeSize();
            notification.text = "";
            isSizePowerUpActive = false;
        }
    }

    public void ModifySpeed() {
        int i = Random.Range(0, 2);
        if (i == 0) {
            vm.DoubleSpeed();
            notification.text = "Double Speed!";
        }
        else {
            vm.HalfSpeed();
            notification.text = "Half Speed!";
        }
        timerSpeed = powerUpDuration;
        isSpeedPowerUpActive = true;
    }

    public void ModifyRotationSpeed() {
        int i = Random.Range(0, 2);
        if (i == 0) {
            vm.DoubleRotationSpeed();
            notification.text = "Double Rotation Speed!";
        }
        else {
            vm.HalfRotationSpeed();
            notification.text = "Half Rotation Speed!";
        }
        timerRotation = powerUpDuration;
        isRotationPowerUpActive = true;
    }

    public void ModifySize() {
        int i = Random.Range(0, 2);
        if (i == 0) {
            vm.DoubleSize();
            notification.text = "Double Size!";
        }
        else {
            vm.HalfSize();
            notification.text = "Half Size!";
        }
        timerSize = powerUpDuration;
        isSpeedPowerUpActive = true;
    }

    public void TriggerRandomFunction() {
        int i = Random.Range(0, 2);
        if (i == 0) {
            ModifySpeed();
        }
        else if (i == 1) {
            ModifyRotationSpeed();
        }
        else {
            //ModifySize();
        }
    }

}
