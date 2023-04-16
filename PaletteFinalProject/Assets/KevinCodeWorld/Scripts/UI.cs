using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UI : MonoBehaviour
{
    //lvl3 UI
    public GameObject enemyCountImage;
    public TextMeshProUGUI enemyCount;
    //conditionsScriptRef
    public Conditions condRef;
    //starry night
    int starYeetCount=-1;
    //level tracker
    int lvlNum;
    //array with all images
    public Sprite[] healthPic;
    public Sprite [] mountainPic;
    int i = 0;
    //image
    public Image uiImage;
    public Image mountainImage;
    public GameObject mountainObjRef;
    public GameObject mountainStarObj;
    public GameObject uiTimerPic;
    public GameObject speedBoostUIObj;
    public GameObject hiddenVObj;
    //loopVar/charRef
    public PlayerMove playerHealthRef;
    //public Interactions interactRef;
    int healthCurrent;
    int cHealthTrackerLoop;

    // Start is called before the first frame update
    void Start()
    {
        healthCurrent = playerHealthRef.vantaHealth;
        cHealthTrackerLoop =healthCurrent;
        string currentSceneName = SceneManager.GetActiveScene().name;
        if(currentSceneName =="Level One")
        {
            lvlNum = 1;
            mountainObjRef.SetActive(true);
            //mountain UI possibly
        }
        else if(currentSceneName =="Level Two")
        {
            lvlNum = 2;
            uiTimerPic.SetActive(true);
        }
        else if(currentSceneName =="Level Three")
        {
            lvlNum = 3;
            enemyCountImage.SetActive(true);
            enemyCount.text = condRef.enemyKillCount.ToString("f0") + "/" + condRef.enemyAmount.ToString("f0");
            //set kill count UI image here
        }
        else if(currentSceneName == "Hub")
        {
            //Activate tracker here
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthCurrent = playerHealthRef.vantaHealth;
        if(healthCurrent!=cHealthTrackerLoop)
        {
            cHealthTrackerLoop--;
            uiImage.sprite = healthPic[i];
            i++;

        }
        if(playerHealthRef.boosted)
            speedBoostUIObj.SetActive(true);
        else
            speedBoostUIObj.SetActive(false);

            //activate UI here
        if(playerHealthRef.hiddenV)
            hiddenVObj.SetActive(true);
        else
            hiddenVObj.SetActive(false);
    }

    public void StarryUi()
    {
        starYeetCount++;
        mountainImage.sprite = mountainPic[starYeetCount];
        if(starYeetCount == 5)
        {
            mountainStarObj.SetActive(true);
            condRef.StarryNightActivateOBJ();
        }
    }
    public void LvlThree(int enemyKillC, int enemyTotal)
    {
        enemyCount.text = enemyKillC.ToString("f0") + "/" + enemyTotal.ToString("f0");
    }
}
