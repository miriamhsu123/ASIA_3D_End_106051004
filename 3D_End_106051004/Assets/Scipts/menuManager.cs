using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{
    public void StartGame()
    {
        print("開始遊戲");
        SceneManager.LoadScene("Level_1");
    }

    public void QuitGame()
    {
        print("離開遊戲");
        Application.Quit();
    }
}
