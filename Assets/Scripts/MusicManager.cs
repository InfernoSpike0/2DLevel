using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        var objs = FindObjectsOfType<MusicManager>();
        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }
    }
}
