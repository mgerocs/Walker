using UnityEngine;

public class Gateway : MonoBehaviour
{
    [SerializeField]
    private string _name;

    [SerializeField]
    private SpawnPoint _spawnPoint;

    [SerializeField]
    private string playerTag = "Player";

    [SerializeField]
    private Color _color = Color.green;

    [SerializeField]
    private bool _isActive = true;

    [SerializeField]
    private SceneField _destination;

    public string Name
    {
        get { return _name; }
    }

    public SpawnPoint SpawnPoint
    {
        get { return _spawnPoint; }
    }

    public bool IsActive
    {
        get { return _isActive; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isActive) return;

        if (!other.CompareTag(playerTag)) return;

        EventManager.OnChangeScene?.Invoke(_destination, _name);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        if (!_isActive)
        {
            _isActive = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _color;

        BoxCollider boxCollider = GetComponent<BoxCollider>();
        MeshCollider meshCollider = GetComponent<MeshCollider>();

        if (boxCollider != null)
        {
            Gizmos.DrawWireCube(transform.position, GetComponent<Collider>().bounds.size);
        }

        if (meshCollider != null && meshCollider.sharedMesh != null)
        {
            // Get the mesh from the MeshCollider
            Mesh mesh = meshCollider.sharedMesh;

            // Draw the mesh vertices
            Gizmos.color = _color;
            foreach (Vector3 vertex in mesh.vertices)
            {
                // Convert vertex from local space to world space
                Vector3 worldPosition = transform.TransformPoint(vertex);
                Gizmos.DrawSphere(worldPosition, 0.05f); // Draw a small sphere at each vertex
            }

            // Optionally, draw the mesh triangles
            Gizmos.color = _color; // Set a different color for the edges
            for (int i = 0; i < mesh.triangles.Length; i += 3)
            {
                Vector3 vertex1 = transform.TransformPoint(mesh.vertices[mesh.triangles[i]]);
                Vector3 vertex2 = transform.TransformPoint(mesh.vertices[mesh.triangles[i + 1]]);
                Vector3 vertex3 = transform.TransformPoint(mesh.vertices[mesh.triangles[i + 2]]);

                // Draw lines between the vertices of the triangle
                Gizmos.DrawLine(vertex1, vertex2);
                Gizmos.DrawLine(vertex2, vertex3);
                Gizmos.DrawLine(vertex3, vertex1);
            }
        }
    }

    public void Activate()
    {
        _isActive = true;
    }

    public void Deactivate()
    {
        _isActive = false;
    }


}