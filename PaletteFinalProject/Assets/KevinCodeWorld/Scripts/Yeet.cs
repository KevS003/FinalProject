using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yeet : MonoBehaviour
{
    //UI REF
    public UI uiRef;
    //star var
    int amountPoint = 0;
    // Start is called before the first frame update
    public float yeetPowerEnemy = 10000000;
    public float yeetPowerObject = 10000;
    public float yeetPowerStar = 5000000;
    bool isEnemy;
    bool isStar;
    public GameObject painting;
    public MeshRenderer offObj;
    Rigidbody stopMove;
    float yeetPower;
    Vector3 dir;
    void Start()
    {
        if(this.gameObject.tag ==("Object"))
        {
            yeetPower = yeetPowerObject;
            isEnemy = false;
        }
        else if(this.gameObject.tag == ("Enemy"))
        {

            yeetPower = yeetPowerEnemy;
            isEnemy = true;
            stopMove = this.gameObject.GetComponent<Rigidbody>();
        }
        else if(this.gameObject.tag ==("Star"))
        {
            yeetPower = yeetPowerStar;
            isStar = true;
            stopMove = this.gameObject.GetComponent<Rigidbody>();
        }

    }
    public void getDir(Vector3 direction)
    {
        dir = direction;
    }


    void OnCollisionEnter(Collision impact)
    {
        if(impact.collider.tag == ("Brush"))
        {
            yeet(dir);
        }
        if(isEnemy)
        {
            //make wall impact to stick enemies
            if(impact.collider.tag == ("Wall"))
            {
                
                painting.SetActive(true);
                stopMove.isKinematic = true;
                offObj.enabled = false;
            }
        }
        else if(isStar)
        {
                if(impact.collider.tag == ("Wall"))
                {
                    painting.SetActive(true);
                    stopMove.isKinematic = true;
                    offObj.enabled = false;
                    //send info to UI
                    if(amountPoint<1)
                    {
                        amountPoint++;
                        uiRef.StarryUi();
                    }
                }
        }

    }
    
    public void yeet(Vector3 direction)
    {
        gameObject.GetComponent<Rigidbody>().AddForce(direction *yeetPower * Time.deltaTime);
    }
}
