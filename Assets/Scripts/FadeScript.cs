using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class FadeScript : MonoBehaviour
{
    [SerializeField] public CanvasGroup popup;
    [SerializeField] public TMP_Text popuptext;
    [SerializeField] public float FadeDuration = 1f;
    [SerializeField] public float displayDuration = 2f;




     void Start()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(showpopup(" level " + sceneIndex));

    }

 
    public IEnumerator showpopup(string message)
    {
        popuptext.text = message;
        popup.alpha = 0;
        popup.gameObject.SetActive(true);


        float timer = 0f;

        while(timer > FadeDuration)
        {

            popup.alpha = Mathf.Lerp(0f,1f, timer / FadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        popup.alpha = 1f;

        yield return new WaitForSeconds(displayDuration);

        timer = 0f;
        while (timer < FadeDuration)
        {
           popup.alpha = Mathf.Lerp(1f, 0f, timer / FadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        popup.alpha = 0f;
        popup.gameObject.SetActive(false);
    }
}
