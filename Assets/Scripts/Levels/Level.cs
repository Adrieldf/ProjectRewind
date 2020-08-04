using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private Transform _playerSpawn;

    [SerializeField]
    public int MaxBattery { get; private set; }
    [SerializeField]
    public int MaxRewindCapacity { get; private set; }
    [SerializeField]
    public int RewindCount { get; private set; }

    public Vector3 GetSpawnPosition()
        => _playerSpawn.position;
}