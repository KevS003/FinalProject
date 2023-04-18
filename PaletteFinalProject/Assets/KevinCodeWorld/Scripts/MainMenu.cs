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
	public GameObject[] buttonsRef;
	static bool infinHealth = false;
    static bool removeDoublJmp = false;
	static bool infinTime= false;
	void Start()
	{
		//0-1 are the infinite lives
		//2-3 is hard mode
		//4-5 is infMazeTime
		if(infinHealth== false)
		{
			buttonsRef[0].SetActive(true);
			buttonsRef[1].SetActive(false);
		}
		else
		{
			buttonsRef[0].SetActive(false);
			buttonsRef[1].SetActive(true);
		}
		
		if(removeDoublJmp == false)
		{

			buttonsRef[2].SetActive(true);
			buttonsRef[3].SetActive(false);
		}
		else
		{
			buttonsRef[2].SetActive(false);
			buttonsRef[3].SetActive(true);
		}

		if(infinTime == false)
		{
			buttonsRef[4].SetActive(true);
			buttonsRef[5].SetActive(false);
		}
		else
		{
			buttonsRef[4].SetActive(false);
			buttonsRef[5].SetActive(true);
		}
	}
	public void PlayGame ()
	{
		SceneManager.LoadScene("Hub");
		//SceneManager.LoadScene("HUB");//when hub is set up
	}

	public void InfiniteLives()
	{
		playRef.OptionsSel(1);
		infinHealth = true;
	}
	public void InfiniteLivesOff()
	{
		playRef.OptionsSel(-1);
		infinHealth = false;
	}
	public void InfiniteTime()
	{
		condRef.OptionsSel(1);
		infinTime = true;
	}
	public void InfiniteTimeOff()
	{
		condRef.OptionsSel(-1);
		infinTime = false;
	}
	public void NoDoubleJump()
	{
		playRef.OptionsSel(2);
		removeDoublJmp = true;
	}
	public void NoDoubleJumpOff()
	{
		playRef.OptionsSel(-2);
		removeDoublJmp = false;
	}
	public void QuitGame()
	{
		Debug.Log("Quit!");
		Application.Quit();
	}
	public void setButtons()
	{

	}
}