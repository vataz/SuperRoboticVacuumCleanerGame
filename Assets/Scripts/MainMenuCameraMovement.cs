using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraMovement : MonoBehaviour {
    public LevelGenerator lg;
    public float cameraSpeed;

    // Start is called before the first frame update
    void Start() {
        if(lg)
            lg.GenerateLevel();
    }

    // Update is called once per frame
    void FixedUpdate() {
        transform.Rotate(0f, cameraSpeed, 0f);
    }
}
