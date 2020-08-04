using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Levels = new List<GameObject>();
    private int _currLevelIndex = 0;



    public void SpawnLevel()
    {
        Instantiate(Levels[_currLevelIndex], Vector3.zero, Quaternion.identity);
        _currLevelIndex++;

        
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