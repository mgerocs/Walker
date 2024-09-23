using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{

    public static DontDestroyOnLoad Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist GameManager and its components
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate GameManager objects
        }
    }
}