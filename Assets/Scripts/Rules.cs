using UnityEngine;
using UnityEngine.SceneManagement;

public class Rules: MonoBehaviour
{
    public void ChangeToRules()
    {
        SceneManager.LoadScene("Rules");
    }
}