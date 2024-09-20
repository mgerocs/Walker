using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private Color _color;

    [SerializeField]
    private float _radius;

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Handles.color = _color;

        Handles.DrawWireDisc(transform.position, Vector3.up, _radius);
#endif
    }

}