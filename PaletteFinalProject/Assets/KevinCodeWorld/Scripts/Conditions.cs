using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Conditions : MonoBehaviour
{
    //player script
    public PlayerMove playerVarRef;
    //determines if Player wins or loses

    //Variables for levels
    int levelIndex;

    //StarryNight variables
    public float timer = 60.0f;
    //float timeOG;
    public float timeBonus = 15f;
    public TextMeshProUGUI timerText;

    // Start is called before the first frame update
    //fill start function with level conditions
    void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if(currentSceneName =="Level One")
        {
            levelIndex =1;
        }
        timerText.text = timer.ToString(".0f");
    }

    // Update is called once per frame
    void Update()
    {
        if(levelIndex ==1)//Connor
        {
            StarryNight();
            //timeUpd();
        }
        else if(levelIndex == 2)//Gage lvl
        {
            
        }
        else if(levelIndex ==3)//Kobe lvl
        {

        }
        else if(levelIndex == 4)//Hub
        {

        }
    }

    void StarryNight()
    {
        if (timer > 0f && playerVarRef.vantaHealth >0)
        {
            timer -= Time.deltaTime;
            timerText.text = timer.ToString("f0");
            Debug.Log("Timer: "+ timer);
        }
        else
        {
            playerVarRef.Conditions(0);
            //kill the player and open menu options
        }
    }
    public void timeUpd()
    {
        //add timer plus code here
        timer += timeBonus;
    }
}
