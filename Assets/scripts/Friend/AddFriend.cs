using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddFriend : MonoBehaviour
{
    public GameObject notificationTab;
    public Transform content;
    public GameObject template;
    public static AddFriend Instance { get; private set; }
    public InputField searchInPut;
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
    void OnEnable()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
        searchInPut.text = "";

    }
    public void SearchBtn()
    {
       
        FriendServer.SendSearchUser(searchInPut.text);
    }
}
