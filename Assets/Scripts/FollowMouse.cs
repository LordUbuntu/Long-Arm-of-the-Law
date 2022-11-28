using UnityEngine;

public class FollowMouse : MonoBehaviour {
    public float speed = 0.1f;
    Vector2 mousePosition;
    Rigidbody2D body;
    Vector2 position = new Vector2(0f, 0f);

    void Start() {
        body = GetComponent<Rigidbody2D>();
    }

    void Update() {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        position = Vector2.Lerp(transform.position, mousePosition, speed)
    }

    void FixedUpdate() {
        body.MovePosition(position);
    }
}
