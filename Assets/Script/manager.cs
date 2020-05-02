﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class manager : MonoBehaviour
{

	void Start()
	{
		List<int> list = new List<int>(new int[] { 2, 3, 7 });
		Debug.Log(list.Count);
		//DontDestroyOnLoad(this.gameObject);
		Button btn = this.GetComponent<Button>();
		btn.onClick.AddListener(delegate {
			OnClick(btn.name);
		});

	}

	private void OnClick(string name)
	{
		Debug.Log(name);
		switch (name)
		{
			case "UGUI":
				Debug.Log("Text1");
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
                break;
			case "Login":
				Debug.Log("Text1");
				UnityEngine.SceneManagement.SceneManager.LoadScene(2);
				break;
			case "Back":
				Debug.Log("Back");
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
			default:
				Debug.Log("Button Clicked. ClickHandler.");
				break;
		}

	}


}