using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Swing : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float angleLimit = 30f;
    public float speed = 2f;

    private float startAngle;

    void Start()
    {
        startAngle = transform.eulerAngles.z;
    }

    void Update()
    {
        float angle = Mathf.Sin(Time.time * speed) * angleLimit;
        transform.rotation = Quaternion.Euler(0f, 0f, startAngle + angle);
    }
}
