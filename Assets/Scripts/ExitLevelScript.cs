using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private int sceneBuildIndex;
    [SerializeField] private int ScoreRequired = 3;

    private Collider2D exitCollider;

    private void Start()
    {
        exitCollider = GetComponent<Collider2D>();
        if (exitCollider == null)
            Debug.LogError("No Collider2D found on exit!");


        exitCollider.isTrigger = false;
    }

    private void Update()
    {
        PlayerMovement player = Object.FindFirstObjectByType<PlayerMovement>();
        if (player != null)
        {
            if (player.ScoreCollected >= ScoreRequired)
            {
                // Unlock exit: make collider a trigger
                exitCollider.isTrigger = true;
            }
            else
            {
                // Keep exit solid
                exitCollider.isTrigger = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null && player.ScoreCollected >= ScoreRequired)
            {
                SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
            }
        }
    }
}