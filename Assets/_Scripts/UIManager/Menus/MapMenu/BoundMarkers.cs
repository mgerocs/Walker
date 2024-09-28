using UnityEngine;

public class BoundMarkers : MonoBehaviour
{
    [SerializeField]
    private Transform _lower;
    [SerializeField]
    private Transform _upper;

    public Vector2 FindNormalizedPosition(Vector3 position)
    {
        float xPosition = Mathf.InverseLerp(_lower.position.x, _upper.position.x, position.x);
        float zPosition = Mathf.InverseLerp(_lower.position.z, _upper.position.z, position.z);
        return new Vector2(xPosition, zPosition);
    }
}
