using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private bool _isDefault;

    [SerializeField]
    private Color _color;

    [SerializeField]
    private float _radius;

    public bool IsDefault
    {
        get { return _isDefault; }
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR

        Vector3 circleCenter = gameObject.transform.position;
        Vector3 upDirection = Vector3.up;

        Handles.color = _color;

        Handles.DrawWireDisc(circleCenter, upDirection, _radius);

        // Find the point on the circle in the forward direction
        Vector3 forwardDirection = gameObject.transform.forward;
        Vector3 pointOnCircle = circleCenter + forwardDirection.normalized * _radius;

        // Draw a small dot or sphere at the forward direction point
        Handles.DrawSolidDisc(pointOnCircle, upDirection, 0.05f); // 0.05f is the size of the dot

        // Optionally, draw a line from the center to the point
        Handles.DrawLine(circleCenter, pointOnCircle);
#endif
    }

}