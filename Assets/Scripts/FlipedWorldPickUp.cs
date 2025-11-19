using UnityEngine;

public class FlipedWorldPickUp : MonoBehaviour
{
    [SerializeField] public float flipDuration;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
    
    
    if(player != null)
        {
            player.flipWorld(flipDuration);
            Destroy(gameObject);
        }
    }
}
