using GameClient.Constructor;
using GameClient.Enums;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendServer : MonoBehaviour
{
    #region ===================Request====================
    public static void SendRemoveFriend(int id)
    {
        var data = new Dictionary<byte, object>();
        data[1] = FriendCode.Remove;
        data[2] = id;
        PhotonServer.PhotonPeer.OpCustom((byte)RequestCode.Friend, data, true);
        Debug.Log(id);
    }
    public static void SendFriendList()
    {
        var data = new Dictionary<byte, object>();
        data[1] = FriendCode.Friend;
        PhotonServer.PhotonPeer.OpCustom((byte)RequestCode.Friend, data, true);
    }
    public static void SendFriended(int id,bool isFriend)
    {
        Debug.Log(isFriend);
        var data = new Dictionary<byte, object>();
        data[1] = FriendCode.Respon;
        data[2] = id;
        data[3] = isFriend;
        PhotonServer.PhotonPeer.OpCustom((byte)RequestCode.Friend, data, true);
    }
    public static void SendPendingList()
    {
        var data = new Dictionary<byte, object>();
        data[1] = FriendCode.Pending;
        PhotonServer.PhotonPeer.OpCustom((byte)RequestCode.Friend, data, true);
    }
    public static void SendAddFriend(int id)
    {
        var data = new Dictionary<byte, object>();
        data[1] = FriendCode.Add;
        data[2] = id;
        PhotonServer.PhotonPeer.OpCustom((byte)RequestCode.Friend, data, true);
    }
    public static void SendSearchUser(string username)
    {
        var data = new Dictionary<byte, object>();
        data[1] = FriendCode.Search;
        data[2] = username;
        PhotonServer.PhotonPeer.OpCustom((byte)RequestCode.Friend, data, true);
    }
    
    
    #endregion

    #region ===================Response===================
    public static void Response(Dictionary<byte, object> dt)
    {
        switch ((int)dt[1])
        {
            case (int)FriendCode.Search:
                GetSearchList(dt);
                break;
            case (int)FriendCode.Pending:
                GetPendingList(dt);
                break;
            case (int)FriendCode.Friend:
                GetFriendList(dt);
                break;
            case (int)FriendCode.Add:
                AddFriendSv(dt);
                break;
            case (int)FriendCode.Remove:
                Remove(dt);
                break;
        }
    }

    private static void Remove(Dictionary<byte, object> dt)
    {
        if ((bool)dt[2])
        {
            FriendList.Instance.notificationTab.SetActive(true);
        }
        else
        {
            Debug.LogError("loi xoa ban be");
        }
    }

    private static void AddFriendSv(Dictionary<byte, object> dt)
    {
        if((bool)dt[2])
        {
            AddFriend.Instance.notificationTab.transform.GetChild(1).GetComponent<Text>().text = "has sent a friend request!";
            AddFriend.Instance.notificationTab.SetActive(true);    
        }
        else
        {
            AddFriend.Instance.notificationTab.SetActive(true);
            AddFriend.Instance.notificationTab.transform.GetChild(1).GetComponent<Text>().text = "You are friends!";
        }
    }

    private static void GetFriendList(Dictionary<byte, object> dt)
    {
        foreach (Transform child in FriendList.Instance.content)
        {
            GameObject.Destroy(child.gameObject);
        }

        List<Player> players = new List<Player>();
        players = JsonConvert.DeserializeObject<List<Player>>((string)dt[2]);
        foreach (Player p in players)
        {
            GameObject newObj = Instantiate(FriendList.Instance.template, FriendList.Instance.content);
            newObj.SetActive(true);
            newObj.transform.GetChild(1).GetComponent<Text>().text = p.name;
            Action<int> buttonClicked2 = (id) =>
            {
                Destroy(newObj);
                SendRemoveFriend(id);
            };

            newObj.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => buttonClicked2(p.userID));



        }
    }
    private static void GetPendingList(Dictionary<byte, object> dt)
    {
        foreach (Transform child in PendingFriend.Instance.content)
        {
            GameObject.Destroy(child.gameObject);
        }
        List<Player> players = new List<Player>();
        players = JsonConvert.DeserializeObject<List<Player>>((string)dt[2]);
        foreach (Player p in players)
        {
            GameObject newObj = Instantiate(PendingFriend.Instance.template, PendingFriend.Instance.content);
            newObj.SetActive(true);
            newObj.transform.GetChild(1).GetComponent<Text>().text = p.name;

            Action<bool> buttonClicked = (isFriended) =>
            {
                Destroy(newObj);
                SendFriended(p.userID, isFriended);
            };

            newObj.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => buttonClicked(true));
            newObj.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => buttonClicked(false));

        }
    }

    private static void GetSearchList(Dictionary<byte, object> dt)
    {
        foreach (Transform child in AddFriend.Instance.content)
        {
            GameObject.Destroy(child.gameObject);
        }
        List<Player> players = new List<Player>();
        players = JsonConvert.DeserializeObject<List<Player>>((string)dt[2]);
        foreach (Player p in players)
        {
            GameObject newObj = Instantiate(AddFriend.Instance.template, AddFriend.Instance.content);
            newObj.SetActive(true);
            newObj.transform.GetChild(0).GetComponent<Text>().text = p.name;
            newObj.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => SendAddFriend(p.userID));
        }
    }
    #endregion
}
