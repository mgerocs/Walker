using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    private float _maxHealth = 100f;
    [SerializeField]
    private float _health = 100f;

    [SerializeField]
    private float _fallDamage = 7f;
    [SerializeField]
    private float _minFallDistance = 3f;

    public float MaxHealth => _maxHealth;
    public float Health { get; set; }

    public float FallDamage => _fallDamage;
    public float MinFallDistance => _minFallDistance;

    public bool IsAlive { get; set; }
    public bool IsSprinting { get; set; }
}
