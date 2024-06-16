using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PendingFriend : MonoBehaviour
{
    public Transform content;
    public GameObject template;
    public static PendingFriend Instance { get; private set; }
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
    private void OnEnable()
    {
        FriendServer.SendPendingList();
    }

}
