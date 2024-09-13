using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{

    private Camera _mainCam;
    [SerializeField] private TextMeshProUGUI _textMesh;

    private string _text;


    private void Start()
    {
        _mainCam = Camera.main;
        _textMesh.text = _text;
    }

    private void LateUpdate()
    {
        var rotation = _mainCam.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
        _textMesh.text = _text;
    }

    public bool isDisplayed = false;

    public void SetUp(string promptText)
    {
        _text = promptText;
        isDisplayed = true;
    }

    public void Close()
    {
        _text = null;
        isDisplayed = false;
    }
}
