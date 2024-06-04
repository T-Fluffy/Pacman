using UnityEngine;
using UnityEngine.SceneManagement;
public class StartUI : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Pacman");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
