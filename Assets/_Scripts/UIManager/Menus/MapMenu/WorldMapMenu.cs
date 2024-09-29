using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldMapMenu : MenuBase
{
    [SerializeField]
    private SceneTracker _sceneTracker;
    [SerializeField]
    private World _world;
    [SerializeField]
    private GameObject _areaButtonPrefab;   // A button prefab for each Area
    [SerializeField]
    private GameObject _sceneButtonPrefab;  // A button prefab for each Scene
    [SerializeField]
    private Transform _areaPanel;           // The parent panel where area buttons will go
    [SerializeField]
    private Transform _scenePanel;          // The parent panel where scene buttons will go

    private SceneData _currentScene;

    private void OnEnable()
    {
        _currentScene = _sceneTracker.CurrentScene;

        Init();
    }

    private void Init()
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
        foreach (Area area in _world.Areas)
        {
            // Instantiate the area button
            GameObject areaButtonObj = Instantiate(_areaButtonPrefab, _areaPanel);
            Button areaButton = areaButtonObj.GetComponent<Button>();
            TextMeshProUGUI areaButtonText = areaButtonObj.GetComponentInChildren<TextMeshProUGUI>();
            areaButtonText.text = area.AreaName;

            // Assign a click event to the button to display scenes for the selected area
            areaButton.onClick.AddListener(() => DisplayScenesForArea(area));
        }
    }

    private void DisplayScenesForArea(Area area)
    {
        // Clear the current scenes
        foreach (Transform child in _scenePanel)
        {
            Destroy(child.gameObject);
        }

        // Loop through each scene in the selected area and create buttons
        foreach (SceneData scene in area.Scenes)
        {
            // Instantiate the scene button
            GameObject sceneButtonObj = Instantiate(_sceneButtonPrefab, _scenePanel);
            TextMeshProUGUI sceneButtonText = sceneButtonObj.GetComponentInChildren<TextMeshProUGUI>();
            sceneButtonText.text = scene.SceneTitle;

            if (_currentScene != null)
            {
                Button button = sceneButtonObj.GetComponent<Button>();

                button.interactable = _currentScene.SceneField.SceneName != scene.SceneField.SceneName;
            }

            // Add a click event to the scene button (e.g., load scene or some action)
            Button sceneButton = sceneButtonObj.GetComponent<Button>();
            sceneButton.onClick.AddListener(() => OnSceneSelected(scene));
        }
    }

    private void OnSceneSelected(SceneData nextScene)
    {
        GameMaster.Instance.SceneTransitionManager.FastTravel(nextScene);
    }
}
