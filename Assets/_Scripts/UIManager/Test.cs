using UnityEngine;

public class RotateCube : MonoBehaviour
{
    // Speed of rotation (degrees per second)
    public float rotationSpeed = 20f;

    void Update()
    {
        // Rotate the cube around the Y axis at the defined speed
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}