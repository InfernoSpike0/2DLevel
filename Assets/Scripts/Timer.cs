using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public static CountdownTimer Instance;

    [SerializeField] public float startTime = 600f; // 10 minutes
    [SerializeField] public TMP_Text timerText;

    private float currentTime;
    private bool initialized = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
            currentTime = startTime;
            initialized = true;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Update()
    {
        if (!initialized) return;

        currentTime -= Time.deltaTime;
        currentTime = Mathf.Max(currentTime, 0f);
        UpdateTimerText();

        if (currentTime <= 0f)
        {
            currentTime = startTime;
            SceneManager.LoadScene(12);
        }
    }

    void UpdateTimerText()
    {
        if (timerText == null) return;

        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       
        if (timerText == null)
        {
            GameObject foundText = GameObject.FindGameObjectWithTag("TimerText");
            if (foundText != null)
            {
                timerText = foundText.GetComponent<TMP_Text>();
                UpdateTimerText();
                Debug.Log("TimerText re-linked in new scene.");
            }
            else
            {
                Debug.LogWarning("No TimerText found in new scene!");
            }
        }
    }

    public void ResetTime()
    {
        currentTime = startTime;
        UpdateTimerText();
    }

    public float GetCurrentTime() => currentTime;
}