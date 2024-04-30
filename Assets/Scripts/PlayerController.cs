using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // our movement variables
    [SerializeField] float moveSpeed;
    Vector2 targetMove, finalMove;
    [SerializeField] float playerBoxFootprint; // the width and height 

    private void FixedUpdate()
    {
        // get input
        CaptureInput();
        // then move
        MovePlayer();
    }

    // get our player's input
    void CaptureInput()
    {
        // reset target movement every frame
        targetMove = Vector2.zero;

        // build our targetmovement using our inputs
        if (Input.GetKey(KeyCode.W)) targetMove.y += 1;
        if (Input.GetKey(KeyCode.A)) targetMove.x += -1;
        if (Input.GetKey(KeyCode.S)) targetMove.y += -1;
        if (Input.GetKey(KeyCode.D)) targetMove.x += 1;
    }

    // move our player
    void MovePlayer()
    {
        // let's check if the position we want to move to is free, and then move there
        Vector2 targetPosition = (targetMove * moveSpeed * Time.fixedDeltaTime) + (Vector2)transform.position;

        // check the position
        if (CheckFree(targetPosition))
        {
            // perform the movement
            ApplyMovement(targetPosition);
            return;
        }

        // break our movement down into components, can we move to these positions?
        if (!CheckFree(targetPosition))
        {
            // first check the x axis
            Vector2 xTargetPosition = (new Vector2(targetMove.x, 0) * moveSpeed * Time.fixedDeltaTime) + (Vector2)transform.position;
            Vector2 yTargetPosition = (new Vector2(0, targetMove.y) * moveSpeed * Time.fixedDeltaTime) + (Vector2)transform.position;

            // we do this so that we can move along the edges of walls
            if (CheckFree(xTargetPosition))
            {
                // perform the movement
                ApplyMovement(xTargetPosition);
                return;
            }            
            
            // check the position
            if (CheckFree(yTargetPosition))
            {
                // perform the movement
                ApplyMovement(yTargetPosition);
                return;
            }
        }
    }    

    /// <summary>
    /// Checks if a position is free for the player to move
    /// </summary>
    /// <param name="position"></param>
    bool CheckFree(Vector2 position)
    {
        // perform a box cast at the position of our footprint
        return !Physics2D.BoxCast(position, new Vector2(playerBoxFootprint, playerBoxFootprint), 0, Vector2.zero);
    }

    // applies the player movement
    void ApplyMovement(Vector2 position)
    {
        transform.position = position;
    }

    // gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(playerBoxFootprint, playerBoxFootprint, playerBoxFootprint));
    }
}
