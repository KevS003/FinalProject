using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image uiImage;
	public PlayerMove playRef;
	public Conditions condRef;
	public void PlayGame ()
	{
		SceneManager.LoadScene("Hub");
		//SceneManager.LoadScene("HUB");//when hub is set up
	}
	public void InfiniteLives()
	{
		playRef.OptionsSel(1);
	}
	public void InfiniteLivesOff()
	{
		playRef.OptionsSel(-1);
	}
	public void InfiniteTime()
	{
		condRef.OptionsSel(1);
	}
	public void InfiniteTimeOff()
	{
		condRef.OptionsSel(-1);
	}
	public void NoDoubleJump()
	{
		playRef.OptionsSel(2);
	}
	public void NoDoubleJumpOff()
	{
		playRef.OptionsSel(-2);
	}
	public void QuitGame()
	{
		Debug.Log("Quit!");
		Application.Quit();
	}
}