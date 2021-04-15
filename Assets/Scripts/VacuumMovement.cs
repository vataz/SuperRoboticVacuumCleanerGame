using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class VacuumMovement : MonoBehaviour {
    public float speed; //default is 5
    public float normalizedSpeed;
    public float rotationSpeed; //default is 2
    public float normalizedRotationSpeed;
    public float normalizedSize;
    public bool run;
    public float timer;
    public float delay;
    public Score score;

    public AudioClip suck;
    public SFXManager sfxManager;

    public Text displayCurrentSpeed;
    public Text displayCurrentRotationSpeed;

    Animator anim;
    Rigidbody rb;
    PowerUpManager pm;

    void Start() {
        normalizedSpeed = speed;
        normalizedRotationSpeed = rotationSpeed;
        normalizedSize = transform.localScale.x;
        rb = GetComponent<Rigidbody>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        pm = GetComponent<PowerUpManager>();
        UpdateCurrentSpeedUI();
        UpdateCurrentRotationSpeedUI();
    }

    void FixedUpdate() {
        if (run)
            rb.velocity = transform.forward * speed;

        if (delay > 0)
            delay -= Time.deltaTime;
        else if (!run && delay > -1)
            run = true;
        if (Input.GetMouseButton(0)) {
            if (Input.mousePosition.x <= Screen.width / 2) {
                transform.Rotate(0f, -rotationSpeed, 0f);
            }
            else {
                transform.Rotate(0f, rotationSpeed, 0f);
            }
        }

    }

    void Update() {

    }

    void OnCollisionEnter(Collision col) {
        if (col.transform.tag == "Furniture" || col.transform.tag == "Wall") {
            PauseMovement(timer);
        }
    }

    void OnTriggerEnter(Collider col) {
        if (col.transform.tag == "Trash") {
            Destroy(col.gameObject);
            if (score != null)
                score.AddPoints();
            anim.Play("EatTrashAnim", -1, 0f);
            if (sfxManager)
                sfxManager.PlaySFX(suck);
        }
        else if (col.transform.tag == "PowerUp") {
            pm.TriggerRandomFunction();
            Destroy(col.gameObject);
        }
    }
    public void PauseMovement(float pauseTime) {
        run = false;
        delay = pauseTime;
    }

    public void SpeedUp() {
        speed += 1f; 
        UpdateCurrentSpeedUI();
    }
    public void SpeedDown() {
        speed -= 1f; 
        UpdateCurrentSpeedUI();
    }

    public void DoubleSpeed() {
        speed *= 2;
    }

    public void HalfSpeed() {
        speed *= 0.5f;
    }

    public void NormalizeSpeed() {
        speed = normalizedSpeed;
    }

    public void IncreaseRotationSpeed() {
        rotationSpeed += 1f;
        UpdateCurrentRotationSpeedUI();
    }
    public void DecreaseRotationSpeed() {
        rotationSpeed -= 1f;
        UpdateCurrentRotationSpeedUI();
    }

    public void DoubleRotationSpeed() {
        normalizedRotationSpeed = rotationSpeed;
        rotationSpeed *= 2;
    }

    public void HalfRotationSpeed() {
        normalizedRotationSpeed = rotationSpeed;
        rotationSpeed *= 0.5f;
    }

    public void NormalizeRotationSpeed() {
        rotationSpeed = normalizedRotationSpeed;
    }

    public void UpdateCurrentSpeedUI() {
        if(displayCurrentSpeed!=null)
            displayCurrentSpeed.text = speed.ToString("0.00");
    }
    public void UpdateCurrentRotationSpeedUI() {
        if (displayCurrentRotationSpeed != null)
                displayCurrentRotationSpeed.text = rotationSpeed.ToString("0.00");
    }

    public void DoubleSize() {
        transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
    }

    public void HalfSize() {
        transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
    }

    public void NormalizeSize() {
        transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
    }
}
