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
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
	}


	
	public void QuitGame()
	{
		Debug.Log("Quit!");
		Application.Quit();
	}
}