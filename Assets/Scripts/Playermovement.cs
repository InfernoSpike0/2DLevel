using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    public bool isflipped = false;
    public Rigidbody2D body;
    public int ScoreCollected = 0;
    public int ScoreToAdvance = 3;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpcooldown = 0.5f;
    [SerializeField] private float nextjumptime;
    [SerializeField] private float Duration_Fliped_World;
    [SerializeField] private Animator _animator;
    Collider2D target;
    bool grab = false;
    bool isFlipped = false;
    Transform originalParent;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        SceneManager.sceneLoaded += OnSceneLoaded;
        originalParent = transform.parent;originalParent = transform.parent;
    }

    void Update()
    {
        if (grab == false)
        {
            movement();
        }
        else
        {
            GrabbedMovement();
        }
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextjumptime)
        {
            jump();
            Debug.Log("Jump Active");
        }
        if (Input.GetKeyDown(KeyCode.F) && target != null)
        {
            if (grab == false)
            {
                Grab();
                Debug.Log("Tried To Grab");
            }
            else
            {
                Release();
            }
        }
        if (grab == true && target != null)
        {
            transform.rotation = target.transform.rotation;
        }
    }
    
    void NormalizeRotation()
    {
        
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetWorld();
    }

    public void Die()
    {
        SceneManager.LoadScene("Level1");
    }

    public void jump()
    {
        float jumpDirection = isflipped ? -1f : 1f;
        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce * jumpDirection);
        nextjumptime = Time.time + jumpcooldown;
    }

    public void movement()
    {
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y);
        if(_animator != null) // This if statement is only here for the testing area, where the player is just a block with no animations. It just makes unity shut up about having no animator.
        {
            if (body.linearVelocity.x != 0)
            {
                _animator.SetBool("isRunning", true);

                if (body.linearVelocity.x > 0)
                {
                    transform.localScale = new Vector3(10, 10, 10);
                }
                else if (body.linearVelocity.x < 0)
                {
                    transform.localScale = new Vector3(-10, 10, 10);
                }
            }
            else
            {
                _animator.SetBool("isRunning", false);
            }
        }        
    }
    public void BoostSpeed(float boostAmount, float duration)
    {
        StartCoroutine(boostSpeedRountine(boostAmount, duration));
    }

    public IEnumerator boostSpeedRountine(float boostAmount, float duration)
    {
        speed += boostAmount;
        yield return new WaitForSeconds(duration);
        speed -= boostAmount;
    }

    public void flipWorld(float durationFlipworld)
    {
        StartCoroutine(FlipedworldRountine(durationFlipworld));
    }

    public IEnumerator FlipedworldRountine(float Duration_Fliped_World)
    {
        isflipped = true;
        Physics2D.gravity = new Vector2(0, 9.81f);

        if (Camera.main != null)
        {
            Quaternion startRotation = Camera.main.transform.rotation;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, 180f);
            float elapsed = 0f;
            float rotateTime = 2f;
            while (elapsed < rotateTime)
            {
                Camera.main.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsed / rotateTime);
                elapsed += Time.deltaTime;
                yield return null;
            }
            Camera.main.transform.rotation = targetRotation;
        }

        transform.localScale = new Vector3(transform.localScale.x, -Mathf.Abs(transform.localScale.y), transform.localScale.z);
        yield return new WaitForSeconds(Duration_Fliped_World);

        isflipped = false;
        Physics2D.gravity = new Vector2(0, -9.81f);

        if (Camera.main != null)
        {
            Quaternion startRotation = Camera.main.transform.rotation;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, 0f);
            float elapsed = 0f;
            float rotateTime = 2f;
            while (elapsed < rotateTime)
            {
                Camera.main.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsed / rotateTime);
                elapsed += Time.deltaTime;
                yield return null;
            }
            Camera.main.transform.rotation = targetRotation;
        }

        transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);
    }

    private void ResetWorld()
    {
        isflipped = false;
        Physics2D.gravity = new Vector2(0, -9.81f);

        if (Camera.main != null)
            Camera.main.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);
    }
     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Grabbable"))
        {
            Debug.Log("Able to grab!");
            target = other;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Grabbable") && other == target)
        {
            target = null;
            if (grab)
            {
                Release();
            }
        }
    }
    void Grab()
    {
        grab = true;
        // Stops all movement after grabbing
        body.linearVelocity = Vector2.zero;
        body.isKinematic = true;
        // Gets the closest point to grab onto
        Vector2 closest = target.ClosestPoint(transform.position);
        Vector2 direction = ((Vector2)transform.position - closest).normalized;

        Vector2 grabPosition = closest + direction;
        // Grabs onto the point and fixes rotation to it
        transform.position = grabPosition;
        transform.rotation = target.transform.rotation;
        //Sets itself as a child of the grabbed object 
        transform.SetParent(target.transform, true);

        Debug.Log("Grabbed " + target.name);
    }
    void GrabbedMovement() // Locks the movement to the rotation of the grabbed object, and allows up/down
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector2 inputDir = new Vector2(h, v);
        Vector2 worldDir = target.transform.TransformDirection(inputDir);

        body.linearVelocity = worldDir * speed;
    }
    void Release() // wow i sure wonder what this does
    {
        grab = false;

        body.isKinematic = false;
        transform.SetParent(originalParent, true);

        Debug.Log("Released " + target.name);
    }
}
