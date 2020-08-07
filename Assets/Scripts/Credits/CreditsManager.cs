using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    public void GoToMainMenu()
        => SceneManager.LoadScene("MainMenu");
}