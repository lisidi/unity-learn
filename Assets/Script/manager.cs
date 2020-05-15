using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;
using BestHTTP;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using LitJson;

public class User
{
    public string userId
    {
        get;
        set;
    }

    public string userName
    {
        get;
        set;
    }

    public string userPassword
    {
        get;
        set;
    }
}

public class LoginRes
{
    public int errno { get; set; }
    public List<User> userList { get; set; }
}

public class Person
{
    public string name { get; set; }
    public int age { get; set; }

}

public class manager : MonoBehaviour
{

    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(delegate
        {
            OnClick(btn.name);
        });
    }


    void OnRequestFinished(HTTPRequest request, HTTPResponse response)
    {
        Debug.Log(response);
        if (response.StatusCode == 200)
        {
            Dictionary<string, object> values = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.DataAsText);
            var errno = values["errno"];
            if (Convert.ToInt32(errno) == 0)
            {
                SSTools.ShowMessage("登录成功", SSTools.Position.bottom, SSTools.Time.threeSecond);
            }
            else
            {
                SSTools.ShowMessage("登录失败", SSTools.Position.bottom, SSTools.Time.threeSecond);
            }
        }
        else
        {
            SSTools.ShowMessage("登录失败", SSTools.Position.bottom, SSTools.Time.threeSecond);
        }
    }



    private void OnClick(string name)
    {
        TMP_InputField accountInputFiled = GameObject.Find("TextMeshPro - InputField - account").GetComponent<TMP_InputField>();
        TMP_InputField passwordInputFiled = GameObject.Find("TextMeshPro - InputField - password").GetComponent<TMP_InputField>();
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
            case "Raw Image":
                break;
            case "Button":
                break;
            case "Input Field":
                break;
            case "Toggle":
                break;
            case "Slider":
                break;
            case "Dropdown":
                break;
            case "ImageLoad":
                UnityEngine.SceneManagement.SceneManager.LoadScene(3);
                break;
            case "LoginButton":
                Login(accountInputFiled.text, passwordInputFiled.text);
                Debug.Log("LoginButton");
                break;
            case "httpLoginButton":
                Debug.Log("httpLoginButton");

                if (accountInputFiled.text.Length == 0 || passwordInputFiled.text.Length == 0)
                {
                    SSTools.ShowMessage("账号或密码为空", SSTools.Position.bottom, SSTools.Time.threeSecond);
                }
                else
                {
                    HTTPRequest request = new HTTPRequest(new Uri("https://www.yibbuda.com/admin/auth/login"), HTTPMethods.Post, OnRequestFinished);
                    Dictionary<string, string> requestParamsDic = new Dictionary<string, string>();
                    requestParamsDic.Add("username", accountInputFiled.text);
                    requestParamsDic.Add("password", passwordInputFiled.text);
                    string requestParamsString = JsonConvert.SerializeObject(requestParamsDic);
                    request.RawData = Encoding.UTF8.GetBytes(requestParamsString);
                    request.AddHeader("Accept", "application/json");
                    request.AddHeader("Content-Type", "application/json;charset=UTF-8");
                    request.Send();
                }
                break;
            case "DownloadButton":
                var urlString = "https://img04.sogoucdn.com/app/a/100520076/a5ec7bf55c2e54146b92abf35e1b7503";
                this.DownloadImage(urlString);

                break;
            default:
                Debug.Log("点击");
                break;
        }

    }

    void Login(string username, string password)
    {
        if (username.Length == 0 || password.Length == 0)
        {
            SSTools.ShowMessage("account or password is empty", SSTools.Position.bottom, SSTools.Time.threeSecond);
        }
        else
        {
            bool isUser = false;
            try
            {
                String JString = File.ReadAllText(Application.dataPath + "/Resource/user.json");
                LoginRes response = JsonMapper.ToObject<LoginRes>(JString);
                if (response != null && response.errno == 0)
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
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }

            if (isUser == true)
            {
                SSTools.ShowMessage("login successful", SSTools.Position.bottom, SSTools.Time.threeSecond);
            }
            else
            {
                SSTools.ShowMessage("login failed", SSTools.Position.bottom, SSTools.Time.threeSecond);

            }
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