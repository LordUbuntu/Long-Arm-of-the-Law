using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleController : MonoBehaviour
{
    public float personSpeed = 1;

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
        }

        // Move towards target
        transform.position = Vector3.MoveTowards(
            transform.position, targetPosition, personSpeed * Time.deltaTime);
    }
}
