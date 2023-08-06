using System.Collections;
using UnityEngine;

public class SharkBehaviour : MonoBehaviour
{
    public GameObject raft;
    public float speed = 10f; // Movement speed
    public float rotationSpeed = 2f; // Rotation speed
    public float detectRadius = 20.0f;  // Radius to detect food
    private Vector3 destination;

    void Start()
    {
        SetRandomDestination();
    }

    void Update()
    {
        // Move shark towards destination
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, destination, step);

        // If shark is close to the destination, set a new random point
        if (Vector3.Distance(transform.position, destination) <= 0.2f)
        {
            SetRandomDestination();
        }

        // If shark is close to the raft
        if (Vector3.Distance(transform.position, raft.transform.position) < 10f)
        {
            destination = new Vector3(transform.position.x, -5, transform.position.z); // Dive
        }

        // Check if there's any food around
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.tag == "food")
            {
                destination = hitCollider.transform.position; // Go towards the food
                break;
            }
        }

        // Rotate the shark to face the destination
        Vector3 direction = (destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    private void SetRandomDestination()
    {
        destination = new Vector3(Random.Range(-40, 40), Random.Range(-5, -0.5f), Random.Range(-40, 40));
    }
}