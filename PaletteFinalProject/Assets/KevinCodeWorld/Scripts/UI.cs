using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UI : MonoBehaviour
{
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
}
