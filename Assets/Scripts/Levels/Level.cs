using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private Transform _playerSpawn;

    [SerializeField]
    public int MaxBattery = 0;
    [SerializeField]
    public int MaxRewindCapacity = 0;
    [SerializeField]
    public int RewindCount = 0;

    public Vector3 GetSpawnPosition()
        => _playerSpawn.position;
}