using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PeopleController : MonoBehaviour
{
    [Header("Speed Settings")]
    [SerializeField] float maxSpeed = 1f;
    [SerializeField] float acceleration = 1f;

    [SerializeField] float rotationSpeed = 5f;

    [Header("Movement Modifiers")]
    [SerializeField] float slowDistance = 2f;
    [SerializeField] float stoppingDistance = 0.3f;
    
    [Range(0.75f, 2f)]
    [SerializeField] float sideMovementReduction = 1.5f;

    [Header("Collission Avoidance")]
    [SerializeField] float raycastDistance = 3f;
    [SerializeField] float avoidanceTurn = 50;

    // This should be moved elsewhere eventually
    [Header("Viewport Options")]
    [SerializeField] float viewportWidth = 15f;
    [SerializeField] float viewportHeight = 7f;

    Vector3 targetPosition;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChooseNewTarget();
    }

    // Update is called once per frame
    void Update() {
        Vector3 toTarget = targetPosition - transform.position;
        float distance = toTarget.magnitude;

        // If we have arrived at the target find a new one
        if (distance < stoppingDistance) {
            ChooseNewTarget();

            // Recalculate the target values
            toTarget = targetPosition - transform.position;
            distance = toTarget.magnitude;
        }


        float dot = Vector3.Dot(transform.up, toTarget.normalized);
        float angle = Vector2.SignedAngle(transform.up, toTarget.normalized); // Get the angle to the target

        float obstacleDirection = CheckObstacles(distance);
        float turn = angle * rotationSpeed;

        // Turn away from obstacles
        if (obstacleDirection != 0) {
            turn = obstacleDirection * avoidanceTurn;
        }

        transform.Rotate(Vector3.forward, turn * Time.deltaTime);


        // Update velocity
        float slow = Mathf.Min(distance, slowDistance) / slowDistance;
        float directionLimit = obstacleDirection == 0 ? dot : 1;
        float velocityChange = Mathf.Max(acceleration * directionLimit * Time.deltaTime, 0);

        // Limit sideways velocity
        rb.velocity *=
            Mathf.Clamp01(sideMovementReduction * Vector2.Dot(rb.velocity.normalized, transform.up));

        rb.velocity += (Vector2)(transform.up * velocityChange); // Apply the velocity change
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, slow * maxSpeed); // Clamp to the max velocity
    }

    void ChooseNewTarget() {
        // Choose a new point anywhere inside the viewport
        targetPosition = new Vector3(
            Random.Range(-viewportWidth, viewportWidth),
            Random.Range(-viewportHeight, viewportHeight)
        );
    }

    // Get the direction to turn to turn away from obstacles
    float CheckObstacles(float targetDistance) {
        // Don't want to avoid the obstacle if the target is this side of the obstacle
        float distanceCheck = Mathf.Min(raycastDistance, targetDistance);

        RaycastHit2D hitMain = Physics2D.Raycast(transform.position, transform.up, distanceCheck);

        // Perform the right and left raycasts
        RaycastHit2D hitRight = Physics2D.Raycast(
            transform.position, transform.TransformDirection(new Vector3(3, 1).normalized), distanceCheck);
        RaycastHit2D hitLeft = Physics2D.Raycast(
            transform.position, transform.TransformDirection(new Vector3(-3, 1).normalized), distanceCheck);

        if (!hitMain && !hitRight && !hitLeft) return 0;

        // Return the direction that has more room
        if (!hitRight) return -1;
        if (!hitLeft) return 1;
        return hitRight.distance > hitLeft.distance ? -1 : 1;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(targetPosition, 0.5f);
    }


}
