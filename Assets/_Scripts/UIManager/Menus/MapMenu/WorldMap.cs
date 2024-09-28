using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldMap : MenuBase
{
    [SerializeField]
    private World _world;
    [SerializeField]
    private SceneTracker _sceneTracker;
    [SerializeField]
    private GameObject _areaButtonPrefab;   // A button prefab for each Area
    [SerializeField]
    private GameObject _sceneButtonPrefab;  // A button prefab for each Scene
    [SerializeField]
    private Transform _areaPanel;           // The parent panel where area buttons will go
    [SerializeField]
    private Transform _scenePanel;          // The parent panel where scene buttons will go

    private AreaNode _selectedArea;

    private SceneNode _currentSceneNode;

    private void OnEnable()
    {
        _currentSceneNode = _sceneTracker.CurrentScene;
        Debug.Log(_currentSceneNode);
    }

    private void Start()
    {
        GenerateUI();
    }

    private void GenerateUI()
    {
        // Clear existing UI if necessary
        foreach (Transform child in _areaPanel)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in _scenePanel)
        {
            Destroy(child.gameObject);
        }

        // Loop through each area and create a button for it
        foreach (AreaNode areaNode in _world.Areas)
        {
            // Instantiate the area button
            GameObject areaButtonObj = Instantiate(_areaButtonPrefab, _areaPanel);
            Button areaButton = areaButtonObj.GetComponent<Button>();
            TextMeshProUGUI areaButtonText = areaButtonObj.GetComponentInChildren<TextMeshProUGUI>();
            areaButtonText.text = areaNode.AreaName;

            // Assign a click event to the button to display scenes for the selected area
            areaButton.onClick.AddListener(() => DisplayScenesForArea(areaNode));
        }
    }

    private void DisplayScenesForArea(AreaNode areaNode)
    {
        if (_selectedArea == areaNode) return;

        _selectedArea = areaNode;

        // Clear the current scenes
        foreach (Transform child in _scenePanel)
        {
            Destroy(child.gameObject);
        }

        // Loop through each scene in the selected area and create buttons
        foreach (SceneNode sceneNode in areaNode.Scenes)
        {
            // Instantiate the scene button
            GameObject sceneButtonObj = Instantiate(_sceneButtonPrefab, _scenePanel);
            TextMeshProUGUI sceneButtonText = sceneButtonObj.GetComponentInChildren<TextMeshProUGUI>();
            sceneButtonText.text = sceneNode.SceneTitle;

            Button button = sceneButtonObj.GetComponent<Button>();

            button.interactable = _currentSceneNode.SceneField.SceneName != sceneNode.SceneField.SceneName;

            // Add a click event to the scene button (e.g., load scene or some action)
            Button sceneButton = sceneButtonObj.GetComponent<Button>();
            sceneButton.onClick.AddListener(() => OnSceneSelected(sceneNode.SceneField));
        }
    }

    private void OnSceneSelected(SceneField scene)
    {
        EventManager.OnChangeScene?.Invoke(scene, null);
    }
}
