using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameMaster
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance;

        [SerializeField]
        private SceneTracker _sceneTracker;

        [SerializeField]
        private SceneName _initialScene;



        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {

        }

        private void OnEnable()
        {
            EventManager.OnSceneChange += HandleSceneChange;
        }

        private void OnDisable()
        {
            EventManager.OnSceneChange -= HandleSceneChange;
        }

        private void HandleSceneChange()
        {
            SceneName nextScene = _sceneTracker.NextScene;

            if (nextScene != SceneName.None)
            {
                LoadScene(nextScene);
            }
            else
            {
                LoadScene(_initialScene);
            }
        }

        public void LoadScene(SceneName scene)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene.ToString());
        }
    }
}