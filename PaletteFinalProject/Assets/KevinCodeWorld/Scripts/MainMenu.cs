using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image uiImage;
	public void PlayGame ()
	{
		SceneManager.LoadScene("Hub");
		//SceneManager.LoadScene("HUB");//when hub is set up
	}


	
	public void QuitGame()
	{
		Debug.Log("Quit!");
		Application.Quit();
	}
}