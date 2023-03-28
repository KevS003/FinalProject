using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //Movement basics/cam
    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;
    public float turnSmoothTime = .1f;
    float turnSmoothVelocity;
    Vector3 moveDir;


    //Jumping stuff
    public float gravity = -9.81f;
    public float groundDistance = 0.45f;
    public LayerMask groundMask;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    bool isGrounded;
    Vector3 velocity;

    //Sprinting stuff
    public float sprintTime = 4.5f;
    float sprintTimeOG;
    bool isSprinting;

    //Melee/interacting stuff
     public float viewRadius = 15;  
     public LayerMask enemyMask;
     public LayerMask objMask;
    [SerializeField] private float range;
    public float rcRange = 5f;
    //private Yeet[] enemyYeet;
    //private Yeet[] objYeet;

    //powerupStuff
    public float boostSpeed= 25;
    float ogSpeed;
    float powerUpLength = 6;
    
    void Start()
    {
        ogSpeed = speed;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        ControlPlayer();
        if(Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    void ControlPlayer()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        float horizontal = Input.GetAxisRaw("Horizontal");//-1 if A, 1 if D
        float vertical = Input.GetAxisRaw("Vertical");//-1 s, 1 W
        Vector3 direction = new Vector3(horizontal,0f, vertical).normalized;
        if(direction.magnitude >=0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z)* Mathf.Rad2Deg+ cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDir = Quaternion.Euler(0f, targetAngle, 0f)* Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        //sprint stuff
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed += 6;
            isSprinting = true;
        }
          
        
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed -= 6;
            isSprinting = false;
        }

        //Jump Stuff
        if(isGrounded && velocity.y <0)
            velocity.y = -2f;
        if(isGrounded&&Input.GetButtonDown("Jump"))
            JumpPlayer();
        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //melee stuff
        if(Input.GetMouseButtonDown(0))
        {
            MeleeLaunch();
        }

        

    }

    void JumpPlayer()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    void MeleeLaunch()
    {
        Vector3 direction = Vector3.forward;
        Ray theRay = new Ray(transform.position, transform.TransformDirection(direction * rcRange));
        Debug.DrawRay(transform.position, transform.TransformDirection(direction * rcRange));
        //play animation here.
        Collider [] enemiesInRange = Physics.OverlapSphere(transform.position, viewRadius, enemyMask);//checks enemies in player range
        Collider [] objInRange = Physics.OverlapSphere(transform.position, viewRadius, objMask);//checks for objects in range
        if(enemiesInRange.Length > 0)
        {
            Debug.Log("ENEMY IN RANGE");
        }
        if(objInRange.Length > 0)
        {
            Debug.Log("Object IN RANGE");
        }
        for(int i = 0; i < enemiesInRange.Length; i++)
        {
            //Come back when yeet script is created
            //Make way to keep track of way player is facing 
            Yeet enemyYeet=enemiesInRange[i].GetComponent<Yeet>();
            enemyYeet.yeet(moveDir);
        }
        for(int i = 0; i < objInRange.Length; i++)
        {
            //Come back when yeet script is written
            //make a way to keep track of direction player is facing. 
            Yeet objYeet=objInRange[i].GetComponent<Yeet>();
            objYeet.yeet(moveDir);
        }
        //yeet script is activated for these objects.
        //Yeet objects/enemies backwards//code with raycast contact as concept
        
    }



/*
    void MeleeLaunch()
    {
        Vector3 direction = Vector3.forward;
        Ray theRay = new Ray(transform.position, transform.TransformDirection(direction * rcRange));
        Debug.DrawRay(transform.position, transform.TransformDirection(direction * rcRange));
        if(Physics.Raycast(theRay, out RaycastHit hit, rcRange))
        {
            if(hit.collider.tag == "Enemy")
            {
                Debug.Log("EnemyDetected");

            }
            else if(hit.collider.tag == "Object")
            {
                Debug.Log("ObjectPush");
            }
        }
        //Yeet objects/enemies backwards//code with raycast contact as concept
    }*/
    void Teleport()
    {
        //Move player to a direction to the right or left quickly
    }

    void DoubleJump()//Try it out
    {
        
        //do it incase 
    }

  void OnTriggerEnter(Collider contact)//I'm not sure if this works with player collider
  {
    Debug.Log("hit");
    
    if(contact.tag == "SpeedBoost")
    {
        Debug.Log("SPEED UP");
        StartCoroutine(SpeedBoost(powerUpLength));
        //Speedboost text here
        //bool to make a counter go
    }
    //change to whatever you name the stun pro.
    //sound effect for stun can go in here
  }

  private IEnumerator SpeedBoost(float interval)
  {
    if(isSprinting)
        speed= boostSpeed+6;
    else
        speed = boostSpeed;
    yield return new WaitForSeconds(interval);
    if(isSprinting)
        speed = ogSpeed+6;
    else
        speed = ogSpeed;
  }





    /*
    void SprintPlayer()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) )//&& sprintTime >= 0
        {
            speed += 6;
          //isSprinting = true;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift)) //|| sprintTime <=0
        {
          //if(speed == 12)
            speed -= 6;
          //isSprinting = false;
          //Debug.Log("NO MORE SPRINT");
        }

        
        if(isSprinting == true)
        {
          sprintTime-=Time.deltaTime;
          //Debug.Log("Subtracting: "+ sprintTime);
        }
        else
        {
            if(sprintTime < sprintTimeOG)
            {
                sprintTime += Time.deltaTime;
                Debug.Log("Adding: "+ sprintTime);
            }
        }
    }*/




    //add sounds and conditions here
    /*
    public void PlaySound(AudioClip clip)
    {
      if (clip == lose || clip == win)
      {
          if(audioPlayed == false)
          {
              playerAudio.volume = 0.06f;
              audioPlayed = true;
              playerAudio.PlayOneShot(clip);
          }     
      }
      else
          playerAudio.PlayOneShot(clip);
    }
    

    void GameOver(bool winL)
    {
    //stop all movement like the pause screen and unlock the mouse
        if(winL == false)
        {
        PlaySound(lose);
        ending--;
      //turn off game audio component here
      //UI updates to lose screen (prolly gonna do that on another screen)
        }
        if(winL == true)
        {
        PlaySound(win);
        ending++;
      //same as lose but for a win
        }
        
    }*/

}