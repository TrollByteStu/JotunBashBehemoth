using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attelery : MonoBehaviour
{
    public float force = 10f;
    public Transform target;
    public float increment = 0.01f;

    public float g = 9.81f; // Gravity
    public float distance;
    public float GR;
    public float RotGR;

    //void Update()
    //{
    //
    //        float angle = CalculateFiringAngle(target.position, force);
    //        Debug.Log("Firing angle: " + angle);
    //        // Use this angle to launch your projectile
    //    
    //}
    //
    //float CalculateFiringAngle(Vector3 targetPosition, float force)
    //{
    //    float distance = Vector3.Distance(new Vector3(targetPosition.x, 0, targetPosition.z),
    //                                       new Vector3(transform.position.x, 0, transform.position.z));
    //    float y = targetPosition.y - transform.position.y;
    //
    //    for (float theta = 0.0f; theta <= Mathf.PI / 2; theta += increment) // Searching from 0 to 90 degrees
    //    {
    //        float R = (force * force * Mathf.Sin(2 * theta)) / g;
    //        float yOffset = (force * force * Mathf.Sin(theta) * Mathf.Sin(theta)) / (2 * g);
    //        if (Mathf.Approximately(distance, R) && Mathf.Approximately(y, yOffset))
    //        {
    //            return theta * Mathf.Rad2Deg; // Convert radian to degree
    //        }
    //    }
    //    return -1f; // Indicate no valid angle found
    //}

    // atempt 2
    //https://phys.libretexts.org/Bookshelves/University_Physics/Book%3A_Physics_(Boundless)/3%3A_Two-Dimensional_Kinematics/3.3%3A_Projectile_Motion

    // atempt 3 works?
    private void Start()
    {
        distance = Vector3.Distance(transform.position, target.position);
        // Given values
        float v0 = force; // initial velocity in m/s
          // acceleration due to gravity in m/s^2
        float x = distance;  // horizontal distance in meters

        // Calculate the values inside the square root
        float insideSqrt = Mathf.Pow(v0, 4) - g * (Mathf.Pow(x, 2) * (2 * 0 + 2 * 0 * Mathf.Pow(v0, 2)));

        // Calculate the square root of the result
        float sqrtResult = Mathf.Sqrt(insideSqrt);

        // Calculate the launch angles using both positive and negative square roots
        float theta1 = Mathf.Atan((Mathf.Pow(v0, 2) + sqrtResult) / (g * x));
        //float theta2 = Mathf.Atan((Mathf.Pow(v0, 2) - sqrtResult) / (g * x));

        // Convert angles from radians to degrees
        float degrees1 = theta1 * (180.0f / Mathf.PI);
        //float degrees2 = theta2 * (180.0f / Mathf.PI);

        // Output the results
        Debug.Log("Possible launch angles:");
        Debug.Log("Theta1: " + degrees1 + " degrees");
        //Debug.Log("Theta2: " + degrees2 + " degrees");
    }


}
