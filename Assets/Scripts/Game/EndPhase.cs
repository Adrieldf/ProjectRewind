using UnityEngine;

public class EndPhase : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            var levelManager = GameObject.FindGameObjectWithTag("LevelManager");
            var script = levelManager.GetComponent<LevelManager>();
            script.LevelCompleted();
        }
    }
}