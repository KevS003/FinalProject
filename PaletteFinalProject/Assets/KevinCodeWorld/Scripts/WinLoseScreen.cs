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
        //changes text
        if(winL ==1)
        {
            winLoseText.text = "Paint onto the night";//win
        }
        else
        {
            winLoseText.text = "Try again?";//lose
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
}
