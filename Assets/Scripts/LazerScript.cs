using UnityEngine;
using UnityEngine.SceneManagement;

public class Laser : MonoBehaviour
{
    public float speed = 40f;            // Laser speed
    public float lifeTime = 3f;          // Destroy after time
    public Vector2 shootDirection = Vector2.right;  // <-- Editable direction

    private Vector3 moveDirection;

    void Start()
    {
        // Use the direction chosen in the Inspector  
        moveDirection = shootDirection.normalized;

        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            SceneManager.LoadScene(1);
        }
    }
}