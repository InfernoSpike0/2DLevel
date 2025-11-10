using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] public PlayerMovement player;
    [SerializeField] public TMP_Text ScoreText;

    public int Score_To_Advance = 3;

    void Update()
    {
        if (player != null && ScoreText != null)
        {

            ScoreText.text = " Score:  " + player.ScoreCollected + "/" + Score_To_Advance;




        }
    }
}
