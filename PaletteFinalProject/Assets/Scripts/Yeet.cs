using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yeet : MonoBehaviour
{
    // Start is called before the first frame update
    public float yeetPowerEnemy = 10000000;
    public float yeetPowerObject = 10000;
    bool isEnemy;
    public GameObject painting;
    public MeshRenderer offObj;
    Rigidbody stopMove;
    float yeetPower;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void yeet(Vector3 direction)
    {
        gameObject.GetComponent<Rigidbody>().AddForce(direction *yeetPower * Time.deltaTime);
    }

    void OnCollisionEnter(Collision impact)
    {
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

    }
}
