using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WinLoseScreen : MonoBehaviour
{
    public GameObject winLMenuUI;
    public TextMeshProUGUI winLoseText;


    // Update is called once per frame
    void Update()
    {

    }
    public void Pause(int winL)
    {
        winLMenuUI.SetActive(true);
        if(SceneManager.GetActiveScene().name == "Level One")
        {
        //changes text
            if(winL ==1)
            {
                //winLoseText.text = "Paint onto the night";//win
            }
            else
            {
                winLoseText.text = "Try again?";//lose
            }
        }
        else if(SceneManager.GetActiveScene().name == "Level Two")
        {
            if(winL ==1)
            {
                //winLoseText.text = "You dominated the maze!";//win
            }
            else
            {
                winLoseText.text = "Try again?";//lose
            }
        }
        else if(SceneManager.GetActiveScene().name == "Level Three")
        {
            if(winL ==1)
            {
                //winLoseText.text = "You survived the gauntlet!";//win
            }
            else
            {
                winLoseText.text = "Try again?";//lose
            }
        }
        else if(SceneManager.GetActiveScene().name == "Hub")
        {
            if(winL ==1)
            {
                //winLoseText.text = "The painting has been complete! The artist has found their art again!";//win
            }
            else
            {
                winLoseText.text = "\nWe tried to warn you";
            }
        }
        winLMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0f;
        Cursor.visible = true;
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        //Load Menu scene
        SceneManager.LoadScene("MainMenu");
        Debug.Log("MenuLoad");
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Hub()
    {
        SceneManager.LoadScene("Hub");
    }
}
