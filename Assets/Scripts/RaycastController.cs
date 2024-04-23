using UnityEngine;

public class RaycastController : MonoBehaviour
{
    // Reference to the UI Image
    public RectTransform dot;

    // Update is called once per frame
    void Update()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Get the camera position
            Vector3 cameraPosition = Camera.main.transform.position;

            // Direction of the ray (along the Z-axis)
            Vector3 rayDirection = Camera.main.transform.forward;

            // Create a ray with the camera position and direction
            Ray ray = new Ray(cameraPosition, rayDirection);
            RaycastHit hit;

            // Visualize the ray
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green, 1f);

            // Check if the ray hits any objects
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the object hit by the ray has a collider
                if (hit.collider != null)
                {
                    // Do something with the object hit
                    Debug.Log("Hit object: " + hit.collider.gameObject.name);
                }
            }
        }
    }
}
