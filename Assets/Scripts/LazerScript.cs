using UnityEngine;
using UnityEngine.SceneManagement;

public class Laser : MonoBehaviour
{
    public float speed = 40f;    // Laser speed
    public float lifeTime = 3f;  // Destroy after this time

    private Vector3 moveDirection;

    void Start()
    {
        
        moveDirection = transform.right.normalized;

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