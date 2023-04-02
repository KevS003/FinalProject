using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    //Movement basics/cam
    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;
    public float turnSmoothTime = .1f;
    float turnSmoothVelocity;


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


    // Update is called once per frame
    void Update()
    {
        //Basic Movement and camera control
        float horizontal = Input.GetAxisRaw("Horizontal");//-1 if A, 1 if D
        float vertical = Input.GetAxisRaw("Vertical");//-1 s, 1 W
        Vector3 direction = new Vector3(horizontal,0f, vertical).normalized;
        if(direction.magnitude >=0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z)* Mathf.Rad2Deg+ cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f)* Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        //Jumping
        if(isGrounded && velocity.y <0)
        {
            velocity.y = -2f;
        }
        if(Input.GetButtonDown("Jump")&& isGrounded)//remove if jump is not needed
        {
          velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //sprint
       
      
        if(Input.GetKeyDown(KeyCode.LeftShift) && sprintTime >= 0)
        {
          speed += 6;
          isSprinting = true;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift) || sprintTime <=0)
        {
          if(speed == 12)
            speed -= 6;
          isSprinting = false;
          Debug.Log("NO MORE SPRINT");
        }
      
        
        if(isSprinting == true)
        {
          sprintTime-=Time.deltaTime;
          Debug.Log("Subtracting: "+ sprintTime);
        }
        else
        {
            if(sprintTime < sprintTimeOG)
            {
                sprintTime += Time.deltaTime;
                Debug.Log("Adding: "+ sprintTime);
            }
        }

        
    }
}
