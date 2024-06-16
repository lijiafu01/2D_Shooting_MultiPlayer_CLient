using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public float speed = 2f;
    public float lifeTime = 1f;
    public Vector3 initialScale = Vector3.one;
    public Vector3 finalScale = Vector3.zero;

    private float startTime;

    void Start()
    {
        startTime = Time.time;
        transform.localScale = initialScale;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {

        float t = (Time.time - startTime) / lifeTime;
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        transform.localScale = Vector3.Lerp(initialScale, finalScale, t);
    }
    void LateUpdate()
    {
       
    }

   

}
