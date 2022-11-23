using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleController : MonoBehaviour
{
    public float personSpeed = 1;
    public float rotationSpeed = 30;

    public float viewportWidth = 5;
    public float viewportHeight = 3;

    Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        ChooseNewTarget();
    }

    void ChooseNewTarget() {
        targetPosition = new Vector3(
            Random.Range(-viewportWidth, viewportWidth),
            Random.Range(-viewportHeight, viewportHeight)
        );
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toTarget = targetPosition - transform.position;
        float sqrDistance = toTarget.sqrMagnitude;

        if (sqrDistance < 0.1f) {
            ChooseNewTarget();
            toTarget = targetPosition - transform.position;
        }

        float dot = Vector3.Dot(transform.up, toTarget.normalized);
        float angle = Vector2.SignedAngle(transform.up, toTarget.normalized);

        transform.Rotate(Vector3.forward, angle * rotationSpeed * Time.deltaTime);

        // Move towards target
        float maxMovement = Mathf.Max(personSpeed * dot * Time.deltaTime, 0);
        float movement = Mathf.Min(maxMovement, toTarget.magnitude);
        transform.position += transform.up * movement;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(targetPosition, 0.3f);
    }
}
