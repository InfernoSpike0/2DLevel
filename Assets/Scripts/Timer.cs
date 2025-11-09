using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{

    public static CountdownTimer Instance;

    [SerializeField] public float startTime = 600f; // This is 10 minutes
    [SerializeField] public TMP_Text timerText;

    private float currentTime;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            currentTime = startTime;
        }
        else
        {
            Destroy(gameObject);
            return;
        }


        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    void Update()
    {
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
            }
        }
    }
}