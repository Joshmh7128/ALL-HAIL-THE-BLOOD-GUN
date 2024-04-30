using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    // our animator
    [SerializeField] Animator animator;
    bool lastFrameMirror;

    public enum AnimationStates
    {
        idle, run
    }

    // private state
    AnimationStates state;

    // our public function to change state
    public void ChangeAnimationState(AnimationStates target, bool mirror)
    {
        if (mirror) transform.localScale = new Vector2(-1, 1); else transform.localScale = Vector2.one;

        if (target == AnimationStates.idle) 
        {
            animator.Play("idle"); 
            if (lastFrameMirror)
            {
                transform.localScale = new Vector2(-1, 1);
                mirror = lastFrameMirror;
            }
        }
        if (target == AnimationStates.run) { animator.Play("run"); }

        lastFrameMirror = mirror;
    }
}
