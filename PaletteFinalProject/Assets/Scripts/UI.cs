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
    //loopVar/charRef
    public PlayerMove playerHealthRef;
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
            i++;
            if(i== 3)
            {
                i=0;
            }
            uiImage.sprite = healthPic[i];

        }
    }
}
