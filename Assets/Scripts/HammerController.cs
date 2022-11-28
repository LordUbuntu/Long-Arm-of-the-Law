using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : MonoBehaviour {
    // TODO
    // - have an arm connecting it to it's "body" (PlayerController)
    // - detect overlapping entities on lmb and score them

    // placeholder
    public int score = 0;


    void Update() {
        Collider[] overlap = Physics.OverlapSphere(transform.position, 0f);
        
        // for each object in collision area
        foreach (Collider hit in overlap) {
            // score hits
            if (hit.gameObject.tag == "enemy") {
                score = score - 1
            } else
            if (hit.gameObject.tag == "civilian") {
                score = score + 1
            }
        }
    }
}
