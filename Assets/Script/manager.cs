using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;
using BestHTTP;
using LitJson;
//using System.Text;
//using Newtonsoft.Json;
//using System.IO;


public class User
{
    public string userId { get; set; }

    public string userName { get; set; }

    public string userPassword { get; set; }
}

public class LoginRes
{
    public int errno { get; set; }
    public List<User> userList { get; set; }
}

public class manager : MonoBehaviour
{

    void Start()
    {
        Button btn = this.GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(delegate
            {
                OnClick(btn.name);
            });
        }

    }


    private void OnClick(string name)
    {
        SSTools.ShowMessage(name, SSTools.Position.bottom, SSTools.Time.twoSecond);
        switch (name)
        {
            case "UGUI":
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
                break;
            case "Login":
                UnityEngine.SceneManagement.SceneManager.LoadScene(2);
                break;
            case "Back":
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                break;
            case "ImageLoad":
                UnityEngine.SceneManagement.SceneManager.LoadScene(3);
                break;
            case "LoginButton":
                Login();
                break;
            case "DownloadButton":
                var urlString = "https://img04.sogoucdn.com/app/a/100520076/a5ec7bf55c2e54146b92abf35e1b7503";
                DownloadImage(urlString);
                break;
            case "DetailBack":
                UnityEngine.SceneManagement.SceneManager.LoadScene(2);
                break;
            default:
                break;
        }

    }

    void Login()
    {
        TMP_InputField accountInputFiled = GameObject.Find("TextMeshPro - InputField - account").GetComponent<TMP_InputField>();
        TMP_InputField passwordInputFiled = GameObject.Find("TextMeshPro - InputField - password").GetComponent<TMP_InputField>();
        string username = accountInputFiled.text;
        string password = passwordInputFiled.text;
        if (username.Length == 0 || password.Length == 0)
        {
            SSTools.ShowMessage("account or password is empty", SSTools.Position.bottom, SSTools.Time.twoSecond);
            return;
        }

        bool isUser = false;
        try
        {
            //本地数据验证
            string jsonString = "{ \"errno\": 0, \"userList\": [ { \"userId\": \"1\", \"userName\": \"lisd\", \"userPassword\": \"123\" }, { \"userId\": \"0\", \"userName\": \"nick\", \"userPassword\": \"321\" } ] }";
            LoginRes response = JsonMapper.ToObject<LoginRes>(jsonString);
            if (response != null && response.errno == 0)
            {
                if ((response.userList != null) && (response.userList.Count != 0))
                {
                    foreach (User user in response.userList)
                    {
                        if (username.Equals(user.userName) && password.Equals(user.userPassword))
                        {
                            isUser = true;
                        }
                    }
                }

            }
        }
        catch (Exception e)
        {
            throw new Exception(e.ToString());
        }


        if (isUser == true)
        {
            SSTools.ShowMessage("login successful", SSTools.Position.bottom, SSTools.Time.twoSecond);
            UnityEngine.SceneManagement.SceneManager.LoadScene(4);
        }
        else
        {
            SSTools.ShowMessage("login failed", SSTools.Position.bottom, SSTools.Time.twoSecond);

        }

    }


    void DownloadImage(string urlString)
    {
        new HTTPRequest(new Uri(urlString), (request, response) =>
        {
            var tex = new Texture2D(0, 0);
            tex.LoadImage(response.Data);
            RawImage loadImage = GameObject.Find("loadImage").GetComponent<RawImage>();
            loadImage.texture = tex;
        }).Send();
    }

}