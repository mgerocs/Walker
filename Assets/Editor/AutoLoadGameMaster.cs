/* using UnityEditor;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class AutoLoadGameMaster
{
    // The name of your persistent scene
    private const string _gameMaster = "GameMaster";

    // Static constructor is called once when Unity loads the editor
    static AutoLoadGameMaster()
    {
        // Subscribe to the play mode state change event
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    // This method will be called when Unity's play mode state changes
    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        // When entering play mode
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            // If the persistent scene isn't already loaded, load it additively
            if (!SceneManager.GetSceneByName(_gameMaster).isLoaded)
            {
                SceneManager.LoadScene(_gameMaster, LoadSceneMode.Additive);
            }
        }
    }
} */