using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeGame1()
    {
        SceneManager.LoadScene("Game1");
    }
}