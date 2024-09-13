using UnityEngine;

public class RotateCamera : MonoBehaviour
{

    [SerializeReference]
    public float rotationSpeed = 100f;
    private Quaternion targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        targetRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = 0f;

        if (Input.GetKey(KeyCode.Q))
        {
            horizontalInput = -1f;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            horizontalInput = 1f;
        }

        if (horizontalInput != 0f)
        {
            // Get the current Euler angles
            Vector3 currentRotation = transform.rotation.eulerAngles;

            // Calculate the new Y rotation
            float newYRotation = currentRotation.y + (horizontalInput * rotationSpeed * Time.deltaTime);

            // Create a new Quaternion with the current X and Z rotation, and the updated Y rotation
            targetRotation = Quaternion.Euler(currentRotation.x, newYRotation, currentRotation.z);
        }

        transform.rotation = targetRotation;

    }

}
