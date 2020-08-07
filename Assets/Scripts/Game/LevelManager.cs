using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Levels = new List<GameObject>();
    [SerializeField]
    private GameObject _robot = null;
    private int _currLevelIndex = 0;

    private void Start()
    {
        SpawnLevel();
    }

    public void SpawnLevel()
    {
        Instantiate(Levels[_currLevelIndex], Vector3.zero, Quaternion.identity);
        _currLevelIndex++;

        Level level = GameObject.FindGameObjectWithTag("Level").GetComponent<Level>();

        _robot.transform.position = level.GetSpawnPosition();

        _robot.GetComponent<Battery>().SetMaxBattery(level.MaxBattery);
        _robot.GetComponent<Rewind>().SetProperties(level.MaxRewindCapacity, level.RewindCount);
    }

    public void ResetLevel()
    {
        DeleteCurrentLevel();
        SpawnLevel();
    }

    public void DeleteCurrentLevel()
    {
        Destroy(GameObject.FindGameObjectWithTag("Level"));
        _currLevelIndex--;
    }

    public void LevelCompleted()
    {
        Debug.Log("Parabains");
        //Carregar o level
    }
}