using UnityEngine;
using UnityEngine.UI;

public class manager : MonoBehaviour
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
			case "UGUI":
				Debug.Log("Text1");
				UnityEngine.SceneManagement.SceneManager.LoadScene(1);
				break;
			case "Login":
				Debug.Log("Text1");
				UnityEngine.SceneManagement.SceneManager.LoadScene(1);
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