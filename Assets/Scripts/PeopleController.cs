using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PeopleController : MonoBehaviour
{
    [Header("Speed Settings")]
    [SerializeField] float maxSpeed = 1f;
    [SerializeField] float rotationSpeed = 30f;
    [SerializeField] float acceleration = 1f;

    [Header("Movement Modifiers")]
    [SerializeField] float sqrSlowDistance = 2f;
    [SerializeField] float sqrStoppingDistance = 0.3f;
    
    [Range(0.75f, 2f)]
    [SerializeField] float sideMovementReduction = 1.5f;

    // This should be moved elsewhere eventually
    [Header("Viewport Options")]
    [SerializeField] float viewportWidth = 15f;
    [SerializeField] float viewportHeight = 7f;

    Vector3 targetPosition;
    float currentSpeed;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChooseNewTarget();
    }

    void ChooseNewTarget() {
        // Choose a new point anywhere inside the viewport
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

        // If we have arrived at the target find a new one
        if (sqrDistance < sqrStoppingDistance) {
            ChooseNewTarget();

            // Recalculate the target values
            toTarget = targetPosition - transform.position;
            sqrDistance = toTarget.sqrMagnitude;
        }


        float dot = Vector3.Dot(transform.up, toTarget.normalized);
        float angle = Vector2.SignedAngle(transform.up, toTarget.normalized);
        transform.Rotate(Vector3.forward, angle * rotationSpeed * Time.deltaTime);


        // Update velocity
        float slow = Mathf.Min(sqrDistance, sqrSlowDistance) / sqrSlowDistance;
        float velocityChange = Mathf.Max(acceleration * dot * Time.deltaTime, 0);

        // Limit sideways velocity
        rb.velocity *= 
            Mathf.Clamp01(sideMovementReduction * Vector2.Dot(rb.velocity.normalized, transform.up));

        rb.velocity += (Vector2)(transform.up * velocityChange); // Apply the velocity change
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, slow * maxSpeed); // Clamp to the max velocity
    }
}
