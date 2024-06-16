using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegisterPanel : MonoBehaviour
{
    public InputField usernameInput, passwordInput;
    public Text noticeTxt;
    public static RegisterPanel Instance { get; set; }

    public GameObject susscess;
    public GameObject regisFail;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void RegisterBtn()
    {
        LoginController.Instance.usernameInput.text = usernameInput.text;
        LoginController.Instance.passwordInput.text = passwordInput.text;
        LoginController.Instance.noticeTxt.text = noticeTxt.text;
        LoginController.Instance.RegisterBtn();
    }
    void OnEnable()
    {
        PhotonServer.Instance.isRegister = true;
    }
    public void LoadScene()
    {
        PhotonServer.Instance.isRegister = false;
        SceneManager.LoadScene("MainMenu");
    }
  
    void OnDisable()
    {
        PhotonServer.Instance.isRegister = false;
    }
}
