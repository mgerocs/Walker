using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(CanvasGroup))]
public class SceneBase : MonoBehaviour, IScene
{

    private string _name;

    private bool _active;

    private bool _preloaded;

    private bool _disabled;

    private List<IScene> _neighbours;


    public string Name => _name;

    public bool Active => _active;

    public bool Preloaded => _preloaded;

    public bool Disabled => _disabled;

    public List<IScene> Neighbours => _neighbours;

}
