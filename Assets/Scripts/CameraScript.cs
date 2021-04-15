using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraScript : MonoBehaviour {
    //Attach this script to the camera//

    public GameObject player;
    Color oldColor;
    Renderer rend;
    public Material transparentMaterial;
    public Material originalMaterial;
    public Vector3 fwd;
    public float dist;
    public RaycastHit[] oldHits;


    void Start() {
        oldHits = new RaycastHit[0];
    }

    void Update() {
        dist = Vector3.Distance(transform.position, player.transform.position);
        fwd = player.transform.position - transform.position;

        RaycastHit[] hits = Physics.RaycastAll(transform.position, fwd, dist);
        DecideWhoToMakeTransparent(hits);
        //foreach (RaycastHit h in hits)
        //    MakeTransparent(h.transform.GetComponent<Renderer>());

    }

    public void DecideWhoToMakeTransparent(RaycastHit[] hits) {
        for (int i = 0; i < oldHits.Length; i++) {
            Debug.Log(oldHits[i].transform.name);
            bool stillThere = false;
            for (int j = 0; j < hits.Length; j++) {
                if (oldHits[i].transform.gameObject == hits[j].transform.gameObject) {
                    stillThere = true;
                }
                else
                    MakeTransparent(hits[j].transform.GetComponent<Renderer>());
            }
            if (!stillThere)
                ColorIt(oldHits[i].transform.GetComponent<Renderer>());
        }

        oldHits = hits;
    }

    public void MakeTransparent(Renderer rend) {
        Debug.Log("Made " + rend.transform.name + " Transparent");
        if(rend.transform.tag=="Furniture")
        rend.material = transparentMaterial;
    }

    void ColorIt(Renderer rend) {
        rend.material = originalMaterial;
    }
}