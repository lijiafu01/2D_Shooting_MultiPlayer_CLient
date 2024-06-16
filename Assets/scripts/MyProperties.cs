using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyProperties : MonoBehaviour
{
    public float damage;
    public float hp;
    public float crit;
    public float moveSpeed;
    public float attackSpeed;
    public int category;
    public static MyProperties Instance;
    //test
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }



}
