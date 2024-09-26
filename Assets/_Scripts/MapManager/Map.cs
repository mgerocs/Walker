using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField]
    private BoundMarkers _bounds;

    [SerializeField]
    private RectTransform _imageTransform;

    [SerializeField]
    private RectTransform _markerTransform;

    private Transform _playerTransform;

    private RectTransform MapTransform => transform as RectTransform;

    private void OnEnable()
    {
        EventManager.OnPlayerSpawn += HandlePlayerSpawn;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerSpawn -= HandlePlayerSpawn;
    }

    private void Update()
    {
        Vector2 normalizedPosition = _bounds.FindNormalizedPosition(_playerTransform.position);

        SetMarkerPosition(normalizedPosition);
    }

    private void HandlePlayerSpawn(GameObject player)
    {
        _playerTransform = player.transform;
    }

    void SetMarkerPosition(Vector2 normalizedPosition)
    {
        // Calculate the marker's anchored position
        Vector2 markerPosition = new(
            normalizedPosition.x * _imageTransform.rect.width,
            normalizedPosition.y * _imageTransform.rect.height
        );

        // Offset for the pivot point being in the center
        markerPosition -= new Vector2(_imageTransform.rect.width / 2, _imageTransform.rect.height / 2);

        // Set the marker's position relative to the map
        _markerTransform.anchoredPosition = markerPosition;
    }
}