using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //-----CONFIG------------
[SerializeField] float moveSpeed = 1f;

    //------STATES----------------




    //--------------CACHE-------------
Rigidbody2D myRigidBody;
BoxCollider2D myBlobFeet;


    //-----------------INITIALIZATION----------------
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myBlobFeet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFacingRight()){//--------------------CHANGES SPEED LEFT OR RIGHT BASED ON DIRECTION FACING
            myRigidBody.velocity = new Vector2(moveSpeed, 0f);
        } else {
             myRigidBody.velocity = new Vector2(-moveSpeed, 0f);
        }
        
    }
    bool isFacingRight(){ //this is true if the below is true---------------------CHECKS IF FACING RIGHT OR LEFT
        return transform.localScale.x>0;
    }
    private void OnTriggerExit2D(Collider2D collision) { //------------------------------------------FLIP SPRITE when the collider 2d on this enemy stops 
    //WHEN IT EXITS ANY COLLISION FLIP SPRITE
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f); // flip object left or right when your not the one controlling it
//         ^So what I've done in here is said okay, there's a velocity for the enemy,
// it's either moving right or moving left.
// And we know the sign of that.
// And if it's moving right it'll be plus 1, and if it's moving left it'll be minus 1.
// But what we want to do is force the flipping of that force,
// the flipping of the entire local scale of the entire enemy itself.
// So that we turn it back the other way, if this is a plus,
// then this will make it a minus, we'll be going left.
// If this is a minus, then adding two minuses together, so
// minus of minus 1 make it a plus, so therefore makes it go to the right.
        
    }
}
