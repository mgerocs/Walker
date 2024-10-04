using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Time Tracker", menuName = "Time Manager/Time Tracker")]
public class TimeTracker : ScriptableObject
{
    public DateTime CurrentTime { get; set; }
}