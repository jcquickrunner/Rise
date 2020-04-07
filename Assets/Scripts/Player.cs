using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    //--------------CONFIG---------------

    enum MethodController { Climbing, Running, Nothing, jump };

    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 4f;
    [SerializeField] float ClimbSpeed = 3f;
    //-----------------------STATE---------------------------
    bool isAlive = true;
    MethodController currentMethod = MethodController.Nothing;

    //------------------------CACHE------------------------------
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    Collider2D myCollider2D;
    float gravityScaleAtStart;// cached a variable at the top thats started at void start so you have acess throughout the code.
    
    
    // Start is called before the first frame update
    //--------------------------INITIALIZATION-------------------
    void Start()
    {
        
        myRigidBody = GetComponent<Rigidbody2D>();//CACHE
        myAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<Collider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;
    }
    // Update is called once per frame
    void Update()
    {
        run();
        FlipSprite();
        Jump();
        ClimbLadder();

    }
    private void run()
    {
        currentMethod = MethodController.Running;
        if (currentMethod == MethodController.Running)
        {
            float controlFlow = CrossPlatformInputManager.GetAxis("Horizontal");
            Vector2 playerVelocity = new Vector2(controlFlow * runSpeed, myRigidBody.velocity.y);// velocity has an x and y
            myRigidBody.velocity = playerVelocity;// sets the velociy for player
            bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;// controls set in the input manager in unity

            myAnimator.SetBool("Run", playerHasHorizontalSpeed);//second parameter looks for true or false-- this animation"run" is true or false

        }


    }
    private void Jump()
    {
        // currentMethod = MethodController.jump;
        if (currentMethod == MethodController.Running)
        {
            if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("ground"))) { return; }//if this is happening then the rest of the code will work, if not touching
            // if not touching then the rest of the code will not execute.ground layer then run the rest of jump method
            if (CrossPlatformInputManager.GetButtonDown("Jump"))//^^ return makes it opposite
            {
                Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
                myRigidBody.velocity += jumpVelocityToAdd;// the x and y of velocity to add
            }

        }


    }

    private void ClimbLadder()
    { //THIS COULD BE ALTERED SO THAT THE CLIMB LADDER METHOD IS ONLY AVAILABLE IF NOT TOUCHING GROUND LAYER
        currentMethod = MethodController.Climbing;
        if (currentMethod == MethodController.Climbing)
        {
            
            if (myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder"))) //does the opposite actually of what it reads if you put a return after it
            {
                myRigidBody.gravityScale = 0f;
                float controlFlow = CrossPlatformInputManager.GetAxis("Vertical");// lets you use both buttons already w and s
                Vector2 playerClimbVelocity = new Vector2(myRigidBody.velocity.x, controlFlow * ClimbSpeed);
                myRigidBody.velocity = playerClimbVelocity;
                bool playerHasverticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
                myAnimator.SetBool("Climb", playerHasverticalSpeed);
                
                //  Vector2 climbVelocityToAdd = new Vector2(0f,ClimbSpeed); //x and y climb velocity
                //  myRigidBody.velocity += climbVelocityToAdd; // add this velocity on x and y
            }
            // else if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("ground")))
            // {
            //     myAnimator.SetBool("Climb", false);
            // }
            else if (myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
            {
                bool playerHasverticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
                myAnimator.SetBool("Climb", playerHasverticalSpeed);
            }
            else if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
            {
                Vector2 velocityWhileClimbing = new Vector2(0f, myRigidBody.velocity.y);
                myAnimator.SetBool("Climb", false);
                myRigidBody.gravityScale = gravityScaleAtStart;

            }
            if (myRigidBody.gravityScale == 0f){
                myAnimator.SetBool("Climb", true);
            }

        }
    }
    private void FlipSprite()
    { // turn player around
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;// when first parameter is greater than second parameter then this boolean will be true
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

    // private void PlayAnimation()
    // {
    //     if (Input.GetKeyDown(KeyCode.Alpha1))
    //     {
    //         myAnimator.SetBool("Climb", true);
    //     } else if (Input.GetKeyUp(KeyCode.Alpha1)){
    //         myAnimator.SetBool("Climb",false);
    //     }

    // ----------------------------THIS WORKS------------------------}
}
