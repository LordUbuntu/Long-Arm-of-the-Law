using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {
    public float speed;
    private Rigidbody2D body;


    void Start() {
        body = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate() {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        body.velocity = new Vector2(x * speed, y * speed);
    }
}
