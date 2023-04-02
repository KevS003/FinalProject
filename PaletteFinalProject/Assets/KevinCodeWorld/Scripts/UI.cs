using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    //array with all images
    public Sprite[] healthPic;
    int i = 0;
    //image
    public Image uiImage;
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
}
