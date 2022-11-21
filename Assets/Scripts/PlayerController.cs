using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {
    public float speed;
    private Rigidbody2D body;
    // TODO add inertia/mass


    void Start() {
        body = GetComponent<Rigidbody2D>();
    }


    void Update() {

    }


    void FixedUpdate() {
        // TODO add intertia/mass
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        body.velocity = new Vector2(x * speed, y * speed);
    }
}
