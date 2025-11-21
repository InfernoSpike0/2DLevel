using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{

   

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("player"))
        {
            PlayerMovement PlayerScore = other.GetComponent<PlayerMovement>();
            if (PlayerScore != null)
            {
                PlayerScore.ScoreCollected++;
            }
            Destroy(gameObject);
        }
    }

    }
