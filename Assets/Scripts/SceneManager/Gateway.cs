using UnityEngine;

public class Gateway : MonoBehaviour
{
    [SerializeField]
    private SceneTracker _sceneTracker;

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
    private SceneName _destination;

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

        _sceneTracker.ChangeScene(_sceneTracker.NextScene, _destination, _name);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        if (!_isActive)
        {
            _isActive = true;
        }

        Debug.Log("Player left");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _color;

        Gizmos.DrawWireCube(transform.position, GetComponent<Collider>().bounds.size);
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