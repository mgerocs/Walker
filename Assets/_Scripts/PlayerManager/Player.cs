using Unity.Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerData _playerData;

    [SerializeField]
    private Transform _cameraRoot;

    [SerializeField]
    private Transform _interactionTriger;

    public Transform CameraRoot => _cameraRoot;

    public Transform InteractionTrigger => _interactionTriger;


    private CinemachineImpulseSource _cinemachineImpulseSource;

    private void Awake()
    {
        _cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();

        if (_cinemachineImpulseSource == null)
        {
            Debug.LogError("No CinemachineImpulseSource.");
        }
    }

    public void Land(float fallDistance)
    {
        Debug.Log("Player fell: " + fallDistance);

        if (fallDistance < _playerData.MinFallDistance) return;

        TakeDamage(_playerData.FallDamage * fallDistance);
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Fall damage taken: " + damage);

        if (_cinemachineImpulseSource != null)
        {
            _cinemachineImpulseSource.GenerateImpulse();
        }
        //  _playerData.Health -= damage;
    }
}