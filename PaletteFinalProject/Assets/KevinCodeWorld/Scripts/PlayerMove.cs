using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    //options menu
    public static bool infinHealth;
    public static bool removeDoublJmp;
    //level var
    public bool starryMComplete = false;
    public bool secondLvlComp = false;
    public bool thirdLvlComp = false;
    //UI 
    public bool hiddenV = false;
    public float hiddenLedgeTimer = 10;
    //player health
    public int vantaHealth = 3;

    //Animation stuff
    public Animator playerAnim;
    public float djStartnum = 0;
    int animationHash;

    //timer stuff
    //reference script here
    public Conditions condScriptRef;
    //create death function that restarts player at timer end

    //sound stuff
    AudioSource playerSound;
    public AudioClip walking;
    public AudioClip swing;
    public AudioClip dmg;
    public AudioClip jumpSound;
    int walkingSound =1;

    //checkpoint stuff
    public GameObject startPositionO;//initialize in start function
    Vector3 startPosition;
    public Transform player;
    Transform playerTransform;
    bool isTP = false;
    Vector3 respawnRef;
    Vector3 currentCP;


    //Movement basics/cam
    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;
    float maxSpeed;
    public float startSpeed = 10f;
    float currentSpeed;
    public float rampUpRate = 2;
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
    public float jumpDistance = 4.5f;
    public float airSpeed = 3.5f;
    Vector3 velocity;
    int doubleJump = 1;

    //Sprinting stuff
    public float sprintTime = 4.5f;
    public float sprintBoost;
    float sprintTimeOG;
    bool isSprinting;

    //Melee/interacting stuff
     public float viewRadius = 5; 
     public Transform meleeRange; 
     public LayerMask enemyMask;
     public LayerMask objMask;
    [SerializeField] private float range;
    public float rcRange = 5f;
    //private Yeet[] enemyYeet;
    //private Yeet[] objYeet;

    //powerupStuff
    public bool boosted = false;
    public float boostSpeed= 25;
    float ogSpeed;
    float powerUpLength = 6;

    //Conditions
    public WinLoseScreen winLscreenRef;
    
    void Start()
    {
        Time.timeScale =1f;
        ogSpeed = speed;
        //playerAnim = this.gameObject.GetComponent<Animator>();
        playerSound = this.gameObject.GetComponent<AudioSource>();
        currentCP = startPositionO.transform.position;
        Cursor.lockState = CursorLockMode.Locked;
        currentSpeed = startSpeed;
        maxSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTP == false)
            ControlPlayer();
        if(transform.position == respawnRef)
            StartCoroutine(TPtimer(1));
        if(condScriptRef.levelIndex == 3)
        {
            if(condScriptRef.enemyKillCount == condScriptRef.enemyAmount)
            {
                Conditions(1);//win cond for lvl 3
            }
        }
    }
    void ControlPlayer()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        Debug.Log(isGrounded);
        float horizontal = Input.GetAxisRaw("Horizontal");//-1 if A, 1 if D
        float vertical = Input.GetAxisRaw("Vertical");//-1 s, 1 W
        if(horizontal > 0 || vertical >0 || horizontal<0 ||vertical < 0)
        {
            if(walkingSound == 1)
            {
                PlaySound(walking,1);
                walkingSound=0;
            }
            if(isSprinting == false)
                PlayAnim(2);
            else if(isSprinting == true)
            {
                PlayAnim(-2);
                PlayAnim(4);
            }
            if(currentSpeed<=speed)
            {
                currentSpeed+=rampUpRate*Time.deltaTime;
            }
            
            
        }
        else
        {
            currentSpeed = startSpeed;
            walkingSound= 1;
            if(walkingSound == 1)
                PlaySound(walking, 0);
            
            PlayAnim(-2);
            if(isSprinting == true)
            {
                PlayAnim(-4);
            }
        }
        Vector3 direction = new Vector3(horizontal,0f, vertical).normalized;
        if(direction.magnitude >=0.1f)
        {
            
            float targetAngle = Mathf.Atan2(direction.x, direction.z)* Mathf.Rad2Deg+ cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDir = Quaternion.Euler(0f, targetAngle, 0f)* Vector3.forward;
            if(isGrounded)
                controller.Move(moveDir.normalized * currentSpeed * Time.deltaTime);
            else
                controller.Move(moveDir.normalized * airSpeed * Time.deltaTime);
        }
        //sprint stuff
        if(Input.GetKeyDown(KeyCode.LeftShift)|| Input.GetButtonDown("Sprint"))
        {
            speed += sprintBoost;
            isSprinting = true;
                
            
        }
          
        
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed -= sprintBoost;
            isSprinting = false;
            PlayAnim(-4);
        }

        //Jump Stuff
        if(isGrounded && velocity.y <0)
        {
            PlayAnim(-3);
            velocity.y = -2f;
            velocity.x = 0;
            velocity.z = 0;
            doubleJump=1;

            
        }
        if(isGrounded&&Input.GetButtonDown("Jump"))
            JumpPlayer(vertical);
        else if(isGrounded == false&& Input.GetButtonDown("Jump")&& doubleJump>0)
            DoubleJump();
        
        velocity.y += gravity * Time.deltaTime;
        
        controller.Move(velocity * Time.deltaTime);

        //melee stuff
        if(Input.GetMouseButtonDown(0)|| Input.GetButtonDown("Melee"))
        {
            playerAnim.Play("Melee");
            MeleeLaunch(); 
        }


    }

    void JumpPlayer(float moveDirJ)
    {
        //PlaySound(jumpSound, 1);
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        if(moveDirJ > 0)
            velocity += transform.forward * (currentSpeed/1.5f);
        else if(moveDirJ<0)
            velocity += transform.forward *jumpDistance;
        PlayAnim(3);
        
    }

    void MeleeLaunch()
    {
        //PlayAnim(5);
        PlaySound(swing,1);
        Vector3 direction = Vector3.forward;
        Ray theRay = new Ray(transform.position, transform.TransformDirection(direction * rcRange));
        Debug.DrawRay(transform.position, transform.TransformDirection(direction * rcRange));
        //play animation here.
        Collider [] enemiesInRange = Physics.OverlapSphere(meleeRange.position, viewRadius, enemyMask);//checks enemies in player range
        Collider [] objInRange = Physics.OverlapSphere(meleeRange.position, viewRadius, objMask);//checks for objects in range
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
            if(enemyYeet!= null)
                enemyYeet.getDir(moveDir);
        }
        for(int i = 0; i < objInRange.Length; i++)
        {
            //Come back when yeet script is written
            //make a way to keep track of direction player is facing. 
            Yeet objYeet=objInRange[i].GetComponent<Yeet>();
            //if()//arm reaches object yeet
                objYeet.getDir(moveDir);
        }
        //yeet script is activated for these objects.
        //Yeet objects/enemies backwards//code with raycast contact as concept
        
    }

    void PlayAnim(int animNum)
    {
        if(animNum > 0)
        {

            if(animNum == 2)
                playerAnim.SetBool("Move",true);
            else if(animNum == 3)
                playerAnim.SetBool("Jump", true);
            else if(animNum ==4)
                playerAnim.SetBool("Sprint",true);
            else if(animNum == 5)
                playerAnim.SetBool("Melee", true);
        }
        else
        {

            if(animNum == -2)
                playerAnim.SetBool("Move",false);
            else if(animNum == -3)
                playerAnim.SetBool("Jump", false);
            else if(animNum == -4)
                playerAnim.SetBool("Sprint",false);
            else if(animNum == -5)
                playerAnim.SetBool("Melee", false);
        }
        

        //idle ==1
        //run== 2
        //jump==3
        //sprint == 4
        //melee ==5
    }

    void Teleport(Vector3 respawnL)
    {
        respawnRef = respawnL;
        Debug.Log("Attempting to TP");
        transform.position = respawnL;
    }

    void DoubleJump()//Try it out
    {
        if(removeDoublJmp == false)
        {
            //PlaySound(jumpSound, 1);
            velocity.y = Mathf.Sqrt(jumpHeight/2 * -2f * gravity);
            doubleJump--;
            animationHash = Animator.StringToHash("Jump");
            playerAnim.Play(animationHash, -1, djStartnum);
            //do it incase 

        }

    }

  void OnTriggerEnter(Collider contact)//I'm not sure if this works with player collider
  {
    Debug.Log("hit"); 

    if(contact.tag == "SpeedBoost")
    {
        Debug.Log("SPEED UP");
        StartCoroutine(SpeedBoost(powerUpLength));
        //Speedboost text here//reference UI script here
        //bool to make a counter go//ref UI script here
    }
    if(contact.tag == "LedgeReveal")
    {
        StartCoroutine(UItimerHV(hiddenLedgeTimer));
    }
    if(contact.tag == "Checkpoint")
    {
        currentCP = contact.transform.position;
        Debug.Log("CPFound"+ currentCP);
    }
    if(contact.tag == "DeathBorder"  ||  contact.tag == "Enemy")
    {
        
        
        Debug.Log("dmg!" + vantaHealth);
        if(contact.tag == "DeathBorder"&& vantaHealth > 0)
        {
            if(infinHealth == false)
                vantaHealth--;
            Debug.Log("TP ME!");
            isTP = true;
            Teleport(currentCP);
            //transform.position = currentCP;
            //TP player to last ledge they fell from if they fell
        }
        if(contact.tag == "Enemy"&& vantaHealth > 0)
        {
            //PlaySound(dmg, 1);
            if(infinHealth == false)
                vantaHealth--;
            Debug.Log("DMG ME!");
            //isTP = true;
            //Teleport(currentCP);
            //transform.position = currentCP;
            //TP player to last ledge they fell from if they fell
        }
        if(vantaHealth == 0)
        {
            Conditions(0);
            /*
            Debug.Log("Respawn L");
            isTP = true;
            vantaHealth = 3;
            currentCP = startPosition;
            //transform.position = startPosition;
            Teleport(startPosition);*/
        }
    }
    if(contact.tag == "Clock")
    {
        condScriptRef.timeUpd();
        Destroy(contact.gameObject);
    }
    if(contact.tag == "WinObj")
    {
        if(condScriptRef.levelIndex == 1)
        {
            starryMComplete = true;
        }
        else if(condScriptRef.levelIndex == 2)
        {
            secondLvlComp = true;
        }
        else if(condScriptRef.levelIndex == 3)
            thirdLvlComp = true;
              
        Conditions(1);
    }
  }

  private IEnumerator SpeedBoost(float interval)
  {
    boosted = true;
    if(isSprinting)
        speed= boostSpeed+6;
    else
        speed = boostSpeed;
    yield return new WaitForSeconds(interval);
    boosted = false;
    if(isSprinting)
        speed = ogSpeed+6;
    else
        speed = ogSpeed;
  }

  private IEnumerator UItimerHV(float interval)
  {
    hiddenV = true;
    yield return new WaitForSeconds(interval);
    hiddenV = false;
  }


  private IEnumerator TPtimer(float interval)
  {

    yield return new WaitForSeconds(interval);
    isTP = false;
  }


  public void PlaySound(AudioClip clip, int play)//1 is to start audio 0 is to stop audio
    {
        //playerSound.volume = 1;
        if(play ==1)
        {
            if(clip ==walking)
                playerSound.PlayOneShot(clip, 1f);
            else
                playerSound.PlayOneShot(clip, .7f);
        }
        else if(play == 0)
            playerSound.Stop();
    }
    
   private IEnumerator StopAnim(float interval)
    {
        yield return new WaitForSeconds(interval);
        PlayAnim(-5);

    }


    //functions called by other scripts
    // kill player or win condtions
    public void Conditions(int winL)
    {
        if(winL == 1)
        {
            //win stuff
            winLscreenRef.Pause(1);
        }
        else
        {
            winLscreenRef.Pause(0);
            //lose stuff
        }

    }

    public void OptionsSel(int i)
    {
        if(i==1)
         infinHealth = true;
        else if(i == 2)
         removeDoublJmp = true;
        else if(i==-1)
            infinHealth = false;
        else if(i ==-2)
            removeDoublJmp = false;
    }

    /*public void Win()
    {

    }*/
    


}