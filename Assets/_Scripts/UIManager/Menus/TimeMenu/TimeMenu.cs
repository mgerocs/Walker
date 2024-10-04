using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeMenu : MenuBase
{
    [SerializeField]
    private TimeTracker _timeTarcker;

    [SerializeField]
    private TextMeshProUGUI _timeText;

    [SerializeField]
    private Transform _buttonPanel;

    [SerializeField]
    private GameObject _hoursButtonPrefab;

    [SerializeField]
    private int[] _hours = new int[] { 1, 6, 12, 24 };

    private bool _isSkippingTime = false;

    private Button[] _buttons;

    private void Awake()
    {
        if (_timeTarcker == null)
        {
            Debug.LogError("Missing TimeTracker reference.");
        }

        if (_timeText == null)
        {
            Debug.LogError("Missing TextMeshProUGUI reference.");
        }
    }

    private void OnEnable()
    {
        if (_timeTarcker == null) return;

        if (_timeText == null) return;

        _timeText.text = _timeTarcker.CurrentTime.ToString("HH:mm");

        Init();
    }

    private void Init()
    {
        GenerateUI();
    }

    private void GenerateUI()
    {

        _buttons = new Button[_hours.Length];

        foreach (Transform child in _buttonPanel)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < _hours.Length; i++)
        {

            int hour = _hours[i];

            // Instantiate the area button
            GameObject hoursButtonObj = Instantiate(_hoursButtonPrefab, _buttonPanel);
            Button hoursButton = hoursButtonObj.GetComponent<Button>();
            TextMeshProUGUI hoursButtonText = hoursButtonObj.GetComponentInChildren<TextMeshProUGUI>();
            hoursButtonText.text = $"Wait {hour} hour{(hour > 1 ? 's' : null)}";

            _buttons[i] = hoursButton;

            // Assign a click event to the button to display scenes for the selected area
            hoursButton.onClick.AddListener(() => Wait(hour));
        }
    }

    public void Wait(int hours)
    {
        _isSkippingTime = true;

        foreach (Button button in _buttons)
        {
            button.interactable = !_isSkippingTime;
        }

        EventManager.SkipTime?.Invoke(hours);
    }
}