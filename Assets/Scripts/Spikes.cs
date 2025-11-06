using UnityEngine;
using UnityEngine.SceneManagement;

public class Spikes : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.GetComponent<PlayerMovement>()) {

            SceneManager.LoadScene("Level1");
            //collision.collider.GetComponent<PlayerMovement>().Die();

        }
    }
}
