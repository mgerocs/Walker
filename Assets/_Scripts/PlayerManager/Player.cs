using Unity.Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerData _playerData;

    private CinemachineImpulseSource _cinemachineImpulseSource;

    private CharacterController _characterController;
    private PlayerController _playerController;

    private void Awake()
    {
        _characterController = gameObject.GetComponent<CharacterController>();

        if (_characterController == null)
        {
            Debug.LogError("Missing CharacterController component.");
        }

        _playerController = gameObject.GetComponent<PlayerController>();

        if (_playerController == null)
        {
            Debug.LogError("Missing PlayerController component.");
        }

        _cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();

        if (_cinemachineImpulseSource == null)
        {
            Debug.LogError("Missing CinemachineImpulseSource component.");
        }
    }

    private void Start()
    {
        if (_characterController == null) return;

        _characterController.enabled = true;
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