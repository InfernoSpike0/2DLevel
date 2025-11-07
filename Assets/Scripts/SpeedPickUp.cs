using UnityEngine;

public class SpeedPickUp : MonoBehaviour
{
    [SerializeField] public float boostAmount;
    [SerializeField] public float duration;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement Player = collision.GetComponent<PlayerMovement>();

        if(Player != null)
        {
            Player.BoostSpeed(boostAmount, duration);
            Destroy(gameObject);
        }
    }

}
