using UnityEngine;
using UnityEngine.SceneManagement;

public class settingManager : MonoBehaviour
{
    public void TryAgain()
    {
        print("TryAgain");
        SceneManager.LoadScene("Level_1");
    }

    public void QuitGame()
    {
        print("離開遊戲");
        Application.Quit();
    }
}
