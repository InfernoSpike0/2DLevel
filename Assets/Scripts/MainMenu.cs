using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ChangeToMainMenu()
    {
        SceneManager.LoadScene("Main menu");
    }
}