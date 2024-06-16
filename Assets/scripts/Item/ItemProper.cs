using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameClient.Constructor;

public class ItemProper : MonoBehaviour
{
    public string category;
    public RoleProperties proper;
    void Start()
    {
        Destroy(gameObject, 5f);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
