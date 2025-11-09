using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeGame1()
    {
        SceneManager.LoadScene("Level1");
    }


    public void ChangeToMainMenu()
    {
        Debug.Log(" Main menu");
        SceneManager.LoadScene("Main menu");
    }

    public void TryAgainToLevel1()
    {

        Debug.Log(" Try Again");
        SceneManager.LoadScene("Level1");


    }

    public void Quitgame()
    {
        Debug.Log(" You have exited the game");
        Application.Quit();


    }
}