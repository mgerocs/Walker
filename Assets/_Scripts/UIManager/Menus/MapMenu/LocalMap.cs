using UnityEngine;

public class LocalMap : MenuBase
{
    [SerializeField]
    private RectTransform _imageTransform;

    [SerializeField]
    private RectTransform _markerTransform;

    private Transform _playerTransform;

    private BoundMarkers _boundMarkers;

    private void OnEnable()
    {
        Init();
    }

    private void Update()
    {
        if (_boundMarkers == null) return;

        if (_playerTransform == null) return;

        Vector2 normalizedPosition = _boundMarkers.FindNormalizedPosition(_playerTransform.position);

        SetMarkerPosition(normalizedPosition);
    }

    private void Init()
    {
        _boundMarkers = FindFirstObjectByType<BoundMarkers>();

        Player player = FindFirstObjectByType<Player>();

        if (player != null)
        {
            _playerTransform = player.gameObject.transform;
        }
    }

    private void SetMarkerPosition(Vector2 normalizedPosition)
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

    public void ShowWorldMap(WorldMap worldMap)
    {
        LoadMenu(worldMap);
    }
}