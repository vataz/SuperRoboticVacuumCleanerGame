using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HideStuff : MonoBehaviour {
    public GameObject target;
    public GameObject cam;
    Color oldColor;
    Renderer rend;
    public Material transparentMaterial;
    public Material originalMaterial;
    public Vector3 fwd;
    public float dist;
    public List<RaycastHit> oldHits;


    void Start() {
        oldHits = new List<RaycastHit>();
    }

    void Update() {
        dist = Vector3.Distance(transform.position, target.transform.position);
        fwd = target.transform.position - transform.position;

        RaycastHit[] hitsFromTarget = Physics.RaycastAll(transform.position, fwd, dist);

        dist = Vector3.Distance(cam.transform.position, transform.position);
        fwd = transform.position - cam.transform.position;
        RaycastHit[] hitsFromCamera = Physics.RaycastAll(cam.transform.position, fwd, dist);

        DecideWhoToMakeTransparent(CreateNewList(hitsFromTarget, hitsFromCamera));
    }

    public List<RaycastHit> CreateNewList(RaycastHit[] targetR, RaycastHit[] cameraR) {
        List<RaycastHit> rh = new List<RaycastHit>();
        foreach (RaycastHit r in cameraR) {
            if (r.transform.tag == "Furniture")
                rh.Add(r);
        }

        foreach (RaycastHit h in targetR) {
            bool isDuplicated = false;
            if (h.transform.tag == "Furniture") {
                foreach (RaycastHit old in rh) {
                    if (old.transform.gameObject == h.transform.gameObject)
                        isDuplicated = true;
                }
                if (!isDuplicated)
                    rh.Add(h);
            }
        }

        return rh;
    }

    public void DecideWhoToMakeTransparent(List<RaycastHit> hits) {
        foreach (RaycastHit r in oldHits) {
            bool stillHere = false;
            foreach (RaycastHit h in hits) {
                MakeTransparent(h.transform.GetComponent<Renderer>());
                if (h.transform.gameObject == r.transform.gameObject) {
                    stillHere = true;
                }
            }
            if (!stillHere)
                ColorIt(r.transform.GetComponent<Renderer>());
        }
        oldHits.Clear();
        foreach (RaycastHit c in hits) {
            oldHits.Add(c);
        }
    }

    public void MakeTransparent(Renderer rend) {
        rend.material = transparentMaterial;
    }

    void ColorIt(Renderer rend) {
        rend.material = originalMaterial;
    }
}