using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    //--------------CONFIG---------------
    [SerializeField] float runSpeed = 5f;

    //-----------------------STATE---------------------------
bool isAlive = true;
    //------------------------CACHE------------------------------
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    // Start is called before the first frame update

    
    //--------------------------INITIALIZATION-------------------
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();//CACHE
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        run();
        FlipSprite();
    
    }
    private void run()
    {
        float controlFlow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlFlow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;// sets the velociy for player
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        print(playerVelocity);
        

        myAnimator.SetBool("Run",playerHasHorizontalSpeed);//second parameter looks for true or false-- this animation"run" is true or false
        
    }

    private void Jump(){

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
adasdasdasdasd