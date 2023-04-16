using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Conditions : MonoBehaviour
{
    //options
    public static bool infinTime;
    //UI reference
    public UI uiref;
    //HUB FINAL WIN OBJECT SPAWNS
    public GameObject winZoneForPainting;
    public GameObject fullPainting;
    //lvl two var
    public bool notTesting = false;
    //game tracker
    static int starryMountainComplete=0;
    public int sMCcopy;
    public GameObject starryMountainCanvas;
    static int lvlTwoComplete=0;
    public int lvlTCcopy;
    public GameObject lvlTwoCanvas;
    static int lvlThreeComplete=0;
    public int lvlThreeCcopy;
    public GameObject lvlThreeCanvas;
    //player script
    public PlayerMove playerVarRef;
    //determines if Player wins or loses

    //lvl three var
    public int enemyKillCount=0;
    public int enemyAmount;//update if enemy amount is increased

    //Variables for levels
    [HideInInspector]
    public int levelIndex;

    //StarryNight variables
    public float timer = 60.0f;
    public GameObject [] snowEffect;
    //float timeOG;
    public float timeBonus = 15f;
    public TextMeshProUGUI timerText;
    //SN Win obj activate
    public GameObject starWinAct;

    // Start is called before the first frame update
    //fill start function with level conditions
    void Start()
    {
        sMCcopy = starryMountainComplete;
        lvlTCcopy = lvlTwoComplete;
        lvlThreeCcopy = lvlThreeComplete;

        string currentSceneName = SceneManager.GetActiveScene().name;
        if(currentSceneName =="Level One")
        {
            levelIndex =1;
            for(int i = 0; i<snowEffect.Length;i++)
            {
                snowEffect[i].SetActive(true);
            }
            //star counter
            //mountain update possibly
        }
        else if(currentSceneName == "Level Two")
        {
            levelIndex =2;
            timerText.text = timer.ToString("f0");
        }
        else if(currentSceneName == "Level Three")
        {
            levelIndex =3;
            
        }
        else if(currentSceneName == "Hub")
        {
            levelIndex = 4;
            //tracks canvas 
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
                StarryNightComplete();
        }
        else if(levelIndex == 2)//Gage lvl
        {
            if(playerVarRef.secondLvlComp == true)
                LevelTwoComplete();
            //ACTIVATE PAINT COLOR UI
            if(notTesting == false)
                LevelTwo();
        }
        else if(levelIndex ==3)//Kobe lvl
        {
            //ACTIVATE WHATEVER HE WANTS
            if(enemyKillCount == enemyAmount)
            {
                LevelThreeComplete();
            }

        }
        else if(levelIndex == 4)//Hub
        {
            if(sMCcopy==1)
            {
                Debug.Log("Lvl1Complete");
                starryMountainCanvas.SetActive(true);
            }
            else if(lvlTCcopy==1)
            {
                Debug.Log("Lvl2Complete");
                lvlTwoCanvas.SetActive(true);//paint object
            }
            else if(lvlThreeCcopy==1)
            {
                Debug.Log("Lvl3Complete");
                lvlThreeCanvas.SetActive(true);
            }
            //ACTIVATE LVL TRACKER
            if(lvlThreeCcopy==1 && lvlTCcopy==1 && sMCcopy==1)
            {
                //Spawn game winning object here
                fullPainting.SetActive(true);
                winZoneForPainting.SetActive(true);
            }
        }
    }


    public void StarryNightActivateOBJ()
    {
        starWinAct.SetActive(true);
    }
    void StarryNightComplete()
    {
        starryMountainComplete = 1;
    }
    void LevelTwo()
    {
        if (timer > 0f && playerVarRef.vantaHealth >0)
        {
            if(infinTime == false)
            {
                timer -= Time.deltaTime;
                timerText.text = timer.ToString("f0");
                Debug.Log("Timer: "+ timer);
            }

        }
        else
        {
            playerVarRef.Conditions(0);
            //kill the player and open menu options
        }
    }
    void LevelTwoComplete()
    {
        lvlTwoComplete = 1;
    }
    void LevelThree()
    {
        //enemy kill count = win
    }
    void LevelThreeComplete()
    {
        lvlThreeComplete = 1;
    }
    public void timeUpd()
    {
        //add timer plus code here
        timer += timeBonus;
    }
    public void enemyKill()
    {
        enemyKillCount++;
        uiref.LvlThree(enemyKillCount, enemyAmount);
        Debug.Log(enemyKillCount);
    }
    public void OptionsSel(int i)
    {
        if(i == 1)
            infinTime = true;
        else if(i ==-1)
            infinTime = false;
    }
}
