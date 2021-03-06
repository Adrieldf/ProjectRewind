﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Levels = new List<GameObject>();
    [SerializeField]
    private GameObject _robot = null;
    [SerializeField]
    private Raken _raken = null;
    private int _currLevelIndex = 0;

    private void Start()
    {
        SpawnLevel();
    }

    public void SpawnLevel()
    {
        _raken.ClearMessages();

        if (Levels.Count == _currLevelIndex)
            GoToCreditsScreen();
        else
        {
            Level level = Instantiate(Levels[_currLevelIndex], Vector3.zero, Quaternion.identity).GetComponent<Level>();
            _currLevelIndex++;

            _robot.transform.position = level.GetSpawnPosition();

            _robot.GetComponent<Battery>().SetMaxBattery(level.MaxBattery);
            _robot.GetComponent<Rewind>().SetProperties(level.MaxRewindCapacity, level.RewindCount);
        }
    }

    public void ResetLevel()
    {
        _raken.ClearMessages();
        DeleteCurrentLevel(true);
        SpawnLevel();
    }

    public void DeleteCurrentLevel(bool decreaseLevelIndex)
    {
        Destroy(GameObject.FindGameObjectWithTag("Level"));
        
        if (decreaseLevelIndex)
            _currLevelIndex--;
    }

    public void LevelCompleted()
    {
        DeleteCurrentLevel(false);
        SpawnLevel();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Reset Level"))
            ResetLevel();
    }

    private void GoToCreditsScreen()
        => SceneManager.LoadScene("Credits");
}