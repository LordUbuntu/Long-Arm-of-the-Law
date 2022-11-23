using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleSpawner : MonoBehaviour
{
    enum Directions { Up, Down, Left, Right }

    [SerializeField] int peopleCount;

    [SerializeField] PeopleController personPrefab;

    [SerializeField] float viewportWidth = 20;
    [SerializeField] float viewportHeight = 10;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < peopleCount; i++) {
            SpawnPerson();
        }
    }

    void SpawnPerson() {
        Directions direction = (Directions)Random.Range(0, 3);

        Vector3 position = Vector3.zero;
        switch (direction) {
            case Directions.Up:
                position.x = Random.Range(-viewportWidth / 2, viewportHeight / 2);
                position.y = viewportHeight / 2;
                break;
            case Directions.Down:
                position.x = Random.Range(-viewportWidth / 2, viewportHeight / 2);
                position.y = -viewportHeight / 2;
                break;
            case Directions.Left:
                position.x = -viewportWidth / 2;
                position.y = Random.Range(-viewportHeight / 2, viewportHeight / 2);
                break;
            case Directions.Right:
                position.x = viewportWidth / 2;
                position.y = Random.Range(-viewportHeight / 2, viewportHeight / 2);
                break;
        }

        PeopleController person = Instantiate(personPrefab, position, Quaternion.identity);
    }
}
