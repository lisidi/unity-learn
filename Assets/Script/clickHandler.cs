using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

public class clickHandler : MonoBehaviour
{

	void Start()
	{
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
            case "Text":
				Debug.Log("Text1");
				break;
			case "Image":
				Debug.Log("Image1");
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