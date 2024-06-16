using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataAll : MonoBehaviour
{
    public int roomID;
    private static DataAll instance;
    public bool keyGetEnemyList = false;
    // Property to get the instance
    public static DataAll Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            // An instance already exists, destroy this one
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            // Optional: Don't destroy this object when a new scene loads
            DontDestroyOnLoad(gameObject);
        }
    }

    // Other code...

}
