using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Conditions : MonoBehaviour
{
    //game tracker
    public static bool starryMountainComplete = false;
    public static bool lvlTwoComplete = false;
    public static bool lvlThreeComplete = false;
    //player script
    public PlayerMove playerVarRef;
    //determines if Player wins or loses

    //Variables for levels
    [HideInInspector]
    public int levelIndex;

    //StarryNight variables
    public float timer = 60.0f;
    public GameObject [] snowEffect;
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
            for(int i = 0; i<snowEffect.Length;i++)
            {
                snowEffect[i].SetActive(true);
            }
            //mountain update possibly
        }
        else if(currentSceneName == "Level Two")
        {
            levelIndex =2;
        }
        else if(currentSceneName == "Level Three")
        {
            levelIndex =3;
            timerText.text = timer.ToString(".0f");
        }
        else if(currentSceneName == "Level Four")
        {

        }
        else if(currentSceneName == "CodeKevinScene")
        {
            Debug.Log("IT WORKS!!!!");
        }
        //timerText.text = timer.ToString(".0f");
    }

    // Update is called once per frame
    //use if there is something that needs to be ran on the level
    void Update()
    {
        if(levelIndex ==1)//Connor
        {
            if(playerVarRef.starryMComplete == true)
                StarryNight();
        }
        else if(levelIndex == 2)//Gage lvl
        {
            //ACTIVATE PAINT COLOR UI
        }
        else if(levelIndex ==3)//Kobe lvl
        {
            //ACTIVATE WHATEVER HE WANTS
            timeUpd();
        }
        else if(levelIndex == 4)//Hub
        {
            //ACTIVATE LVL TRACKER
        }
    }

    void StarryNight()
    {
        starryMountainComplete = true;
    }
    void LevelTwo()
    {

    }
    void LevelThree()
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
