using UnityEngine;
using UnityEngine.SceneManagement;

public class Switchinglevels : MonoBehaviour
{

    public int scenceBuildIndex;



    private void OnTriggerEnter2D(Collider2D other)
    {
        print("trigger Entered");





        if (other.tag == "player")
        {
            print("Switching scene to" + scenceBuildIndex);
            SceneManager.LoadScene(scenceBuildIndex, LoadSceneMode.Single);

        }









    }


}
