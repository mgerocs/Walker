using System;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private SceneTracker _sceneTracker;

    [SerializeField]
    private string _playerTag;

    [SerializeField]
    private GameObject _player;

    public void SpawnPlayer()
    {
        if (_player == null)
        {
            throw new Exception("No Player object.");
        }

        if (_player.tag != _playerTag)
        {
            throw new Exception($"Player object should have the tag '{_playerTag}'");
        }

        if (_player.activeInHierarchy)
        {
            throw new Exception("There is a Player already in the scene.");
        }

        SpawnPoint spawnPoint = FindSpawnPoint();

        if (spawnPoint == null)
        {
            throw new Exception("No designated SpawnPoints found.");
        }

        GameObject instance = Instantiate(_player, spawnPoint.gameObject.transform.position, spawnPoint.gameObject.transform.rotation);
        EventManager.OnPlayerSpawned?.Invoke(instance);
    }

    private SpawnPoint FindSpawnPoint()
    {
        Gateway[] gatewaysInScene = FindObjectsByType<Gateway>(FindObjectsSortMode.None);

        if (gatewaysInScene.Length == 0)
        {
            return FindDefaultSpawnPoint();
        }

        if (gatewaysInScene.Length == 1)
        {
            return gatewaysInScene[0].SpawnPoint;
        }

        string lastGatewayName = _sceneTracker.LastGatewayName;

        if (lastGatewayName != null)
        {
            Gateway targetGateway = gatewaysInScene.FirstOrDefault(gw => gw.GatewayName == lastGatewayName);

            if (targetGateway != null)
            {
                return targetGateway.SpawnPoint;
            }
        }

        return FindDefaultSpawnPoint();
    }

    private SpawnPoint FindDefaultSpawnPoint()
    {
        SpawnPoint[] spawnPointsInScene = FindObjectsByType<SpawnPoint>(FindObjectsSortMode.None);

        if (spawnPointsInScene.Length == 0)
        {
            throw new Exception("No SpwanPoints in Scene.");
        }

        return spawnPointsInScene.FirstOrDefault(sp => sp.IsDefault);
    }

}