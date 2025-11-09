using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public bool isflipped = false;
    public Rigidbody2D body;
    [SerializeField] public float speed;
    [SerializeField] public float jumpForce;
    [SerializeField] public float jumpcooldown = 0.5f;
    [SerializeField] public float nextjumptime;
    [SerializeField] public float Duration_Fliped_World;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        movement();
        jump();
        
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
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextjumptime)
        {
            float jumpDirection = isflipped ? -1f : 1f;
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce * jumpDirection);
            nextjumptime = Time.time + jumpcooldown;
        }
    }

    public void movement()
    {
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y);
    }

    public void BoostSpeed(float boostAmount, float duration)
    {
        StartCoroutine(boostSpeedRountine(boostAmount, duration));
    }

    public System.Collections.IEnumerator boostSpeedRountine(float boostAmount, float duration)
    {
        speed += boostAmount;
        yield return new WaitForSeconds(duration);
        speed -= boostAmount;
    }

    public void flipWorld(float durationFlipworld)
    {
        StartCoroutine(FlipedworldRountine(durationFlipworld));
    }

    public System.Collections.IEnumerator FlipedworldRountine(float Duration_Fliped_World)
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
}