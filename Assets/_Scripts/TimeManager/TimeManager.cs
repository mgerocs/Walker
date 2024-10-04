// https://www.youtube.com/watch?v=L4t2c1_Szdk&ab_channel=KetraGames

using System;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    [SerializeField]
    private TimeTracker _timeTracker;

    [SerializeField]
    private float _timeMultiplier = 2000f;

    [SerializeField]
    private float _startHour = 12;

    [SerializeField]
    private TextMeshProUGUI _timeText;

    [SerializeField]
    private Light _sunLight;

    [SerializeField]
    private Light _moonLight;

    [SerializeField]
    private float _sunriseHour = 6;
    [SerializeField]
    private float _sunsetHour = 20.5f;

    [SerializeField]
    private Color _dayAmbientLight;
    [SerializeField]
    private Color _nightAmbientLight;
    [SerializeField]
    private AnimationCurve _lightChangeCurve;
    [SerializeField]
    private float _maxSunLightIntensity = 1;
    [SerializeField]
    private float _maxMoonLightIntensity = 0.5f;

    private TimeSpan _sunriseTime;
    private TimeSpan _sunsetTime;

    private bool _isStopped = false;

    private void Awake()
    {
        if (_timeTracker == null)
        {
            Debug.LogError("Missing TimeTracker reference.");
        }
    }

    private void OnEnable()
    {
        EventManager.OnDialogStarted += HandleDialogStarted;
        EventManager.OnDialogFinished += HandleDialogFinished;

        EventManager.SkipTime += HandleSkipTime;
    }

    private void Start()
    {
        if (_timeTracker == null) return;

        _timeTracker.CurrentTime = DateTime.Now.Date + TimeSpan.FromHours(_startHour);

        _sunriseTime = TimeSpan.FromHours(_sunriseHour);
        _sunsetTime = TimeSpan.FromHours(_sunsetHour);
    }

    private void Update()
    {
        if (_timeTracker == null) return;

        if (!_isStopped)
        {
            UpdateTimeOfDay();
            RotateSun();
            UpdateLightSettings();
        }
    }

    private void OnDisable()
    {
        EventManager.OnDialogStarted -= HandleDialogStarted;
        EventManager.OnDialogFinished -= HandleDialogFinished;

        EventManager.SkipTime -= HandleSkipTime;
    }

    private void HandleDialogStarted()
    {
        _isStopped = true;
    }

    private void HandleDialogFinished()
    {
        _isStopped = false;
    }

    private void HandleSkipTime(int hours)
    {
        SkipTime(hours);
    }

    private void UpdateTimeOfDay()
    {
        _timeTracker.CurrentTime = _timeTracker.CurrentTime.AddSeconds(Time.deltaTime * _timeMultiplier);

        if (_timeText != null)
        {
            _timeText.text = _timeTracker.CurrentTime.ToString("HH:mm");
        }
    }

    private void RotateSun()
    {
        if (_sunLight == null) return;

        float sunLighRotation;

        if (_timeTracker.CurrentTime.TimeOfDay > _sunriseTime && _timeTracker.CurrentTime.TimeOfDay < _sunsetTime)
        {
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(_sunriseTime, _sunsetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(_sunriseTime, _timeTracker.CurrentTime.TimeOfDay);

            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;

            sunLighRotation = Mathf.Lerp(0, 180, (float)percentage);
        }
        else
        {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(_sunsetTime, _sunriseTime);

            TimeSpan timeSinceSunset = CalculateTimeDifference(_sunsetTime, _timeTracker.CurrentTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;

            sunLighRotation = Mathf.Lerp(180, 360, (float)percentage);
        }

        _sunLight.transform.rotation = Quaternion.AngleAxis(sunLighRotation, Vector3.right);
    }

    private void UpdateLightSettings()
    {
        if (_sunLight == null || _moonLight == null) return;

        float dotProduct = Vector3.Dot(_sunLight.transform.forward, Vector3.down);

        _sunLight.intensity = Mathf.Lerp(0, _maxSunLightIntensity, _lightChangeCurve.Evaluate(dotProduct));
        _moonLight.intensity = Mathf.Lerp(_maxMoonLightIntensity, 0, _lightChangeCurve.Evaluate(dotProduct));

        RenderSettings.ambientLight = Color.Lerp(_nightAmbientLight, _dayAmbientLight, _lightChangeCurve.Evaluate(dotProduct));
    }

    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;

        if (difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }

        return difference;
    }

    private void SkipTime(int hours)
    {
        Debug.Log("SKIP TIME");
        _timeTracker.CurrentTime = _timeTracker.CurrentTime.AddHours(hours);
    }
}
