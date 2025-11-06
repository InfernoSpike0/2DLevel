using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float speed;
    public Rigidbody2D body;
    //Vector2 startpostion;
    [SerializeField]public float jumpForce;
    [SerializeField] public float jumpcooldown = 0.5f;
    [SerializeField] public float nextjumptime;


    private void Awake()
    {
        //grabs the Rigidbody2D
        body = GetComponent<Rigidbody2D>();
        //grabs the animator

   
      // startpostion = transform.position;
    
    }






    void Update()
    {
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextjumptime)
        {
            
            
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
            nextjumptime = Time.time + jumpcooldown;
        
       
        
        }



    }

public void Die()
    {

        SceneManager.LoadScene("Level1");
        //transform.position = startpostion;
    }

}
