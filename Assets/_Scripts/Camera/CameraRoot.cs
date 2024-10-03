using UnityEngine;

public class CameraRoot : MonoBehaviour {
    public Color gizmoColor = Color.green;
    public float arrowLength = 2.0f;
    public float arrowHeadSize = 0.5f;

    private void OnDrawGizmos()
    {
        // Set the gizmo color
        Gizmos.color = gizmoColor;

        // Draw a line representing the forward direction (from the object's position)
        Vector3 arrowTail = transform.position;
        Vector3 arrowHead = transform.position + transform.forward * arrowLength;
        Gizmos.DrawLine(arrowTail, arrowHead);

        // Draw the arrowhead
        Vector3 right = Quaternion.LookRotation(transform.forward) * Quaternion.Euler(0, 150, 0) * Vector3.forward;
        Vector3 left = Quaternion.LookRotation(transform.forward) * Quaternion.Euler(0, -150, 0) * Vector3.forward;
        
        Gizmos.DrawLine(arrowHead, arrowHead + right * arrowHeadSize);
        Gizmos.DrawLine(arrowHead, arrowHead + left * arrowHeadSize);
    }
}