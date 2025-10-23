using Unity.VisualScripting;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    [SerializeField] public float speed;
    private Rigidbody2D body;
    private void Awake()
    {
        //grabs the Rigidbody2D
        body = GetComponent<Rigidbody2D>();
        //grabs the animator

    }






    void Update()
    {
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, speed);
        }



    }



}
