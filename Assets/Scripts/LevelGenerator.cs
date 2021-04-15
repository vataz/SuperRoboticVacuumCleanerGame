using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    public int roomSize;
    int[,] roomLayout;

    public int numberOfTrash;
    public int numberOfObstacles;
    public int numberOfPowerUps;

    public Score score;

    public float xPos;
    public float zPos;
    public float xPosOffset;
    public float zPosOffset;

    public GameObject trashGO;
    public GameObject[] furnitureGO;
    public GameObject bigFurnitureGO;
    public GameObject powerUpGO;

    public LevelManager lm;
    public VacuumMovement player;
    public float startDelay;
    // Start is called before the first frame update
    void Start() {
    }

    public void GenerateLevel() {
        GenerateRoomLayout();
        InstantiateRoomLayout();
    }

    public void GenerateRoomLayout() {
        roomLayout = new int[roomSize, roomSize];
        if (score != null)
            score.SetMaxScore(numberOfTrash);
        //-1 - Player Location
        //1 - Trash
        //2 - Furniture
        //3 - Big Furniture
        //4 - Big Furniture with Trash
        //5 - PowerUp

        GeneratePlayerLocation();

        for (int i = 0; i < numberOfTrash; i++) {
            int n = Random.Range(0, roomSize);
            int m = Random.Range(0, roomSize);
            if (roomLayout[n, m] == 0) {
                roomLayout[n, m] = 1;
                if (Random.Range(0, 100) > 75)
                    roomLayout[n, m] = 4;
            }
            else
                i--;
        }

        for (int j = 0; j < numberOfObstacles; j++) {
            int n = Random.Range(0, roomSize);
            int m = Random.Range(0, roomSize);
            if (roomLayout[n, m] == 0) {
                roomLayout[n, m] = 2;
                if (Random.Range(0, 100) > 75)
                    roomLayout[n, m] = 3;
            }
            else
                j--;
        }

        for (int k = 0; k < numberOfPowerUps; k++) {
            int n = Random.Range(0, roomSize);
            int m = Random.Range(0, roomSize);
            if (roomLayout[n, m] == 0)
                roomLayout[n, m] = 5;
            else
                k--;
        }
    }

    void GeneratePlayerLocation() {
        player.PauseMovement(startDelay);
        if (lm != null)
            lm.SetTImerDelay(startDelay);
        int n = Random.Range(0, roomSize);
        int m = 0;
        roomLayout[n, m] = -1;
        if (n > 0)
            roomLayout[n - 1, m] = -2;
        if (n < roomSize - 1)
            roomLayout[n + 1, m] = -2;
        if (m < roomSize - 1)
            roomLayout[n, m + 1] = -2;
    }

    public void InstantiateRoomLayout() {
        for (int i = 0; i < roomSize; i++) {
            for (int j = 0; j < roomSize; j++) {
                GameObject objToInstantiate = null;
                if (roomLayout[i, j] > 0) {
                    if (roomLayout[i, j] == 1)
                        objToInstantiate = trashGO;
                    else if (roomLayout[i, j] == 2)
                        objToInstantiate = furnitureGO[Random.Range(0, furnitureGO.Length)];
                    else if (roomLayout[i, j] == 3)
                        objToInstantiate = bigFurnitureGO;
                    else if (roomLayout[i, j] == 4) {
                        Instantiate(trashGO, new Vector3(xPos, 0, zPos), transform.rotation);
                        objToInstantiate = bigFurnitureGO;
                    }
                    else if (roomLayout[i, j] == 5)
                        objToInstantiate = powerUpGO;
                    Instantiate(objToInstantiate, new Vector3(xPos, 0, zPos), Quaternion.Euler(0, Random.Range(0, 180), 0));
                }
                else if (roomLayout[i, j] == -1)
                    player.transform.position = new Vector3(xPos, player.transform.position.y, zPos);
                zPos += zPosOffset;
            }
            zPos = 0f;
            xPos += xPosOffset;
        }
    }
}
