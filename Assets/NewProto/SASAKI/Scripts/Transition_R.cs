using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition_R : MonoBehaviour
{
    Animator animator;

    //列挙型でアニメーション変数を定義
    public enum Anim
    {
        WALK,
        JUMP,
        KICK,
        KICKFA,
        CUTTER,
        BLAST,
        CUTTERFA
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetAnimator(Anim anim, bool setAnim)
    {
        switch (anim)
        {
            case Anim.WALK:
                animator.SetBool("Move", setAnim);
                break;

            case Anim.JUMP:
                animator.SetBool("Jump", setAnim);
                break;

            case Anim.KICK:
                animator.SetBool("Kick", setAnim);
                break;

            case Anim.KICKFA:
                animator.SetBool("FallAttackKick", setAnim);
                break;

            case Anim.CUTTER:
                animator.SetBool("Cutter", setAnim);
                break;

            case Anim.CUTTERFA:
                animator.SetBool("FallAttackCutter", setAnim);
                break;

            case Anim.BLAST:
                animator.SetBool("Blast", setAnim);
                break;
        }
    }
}
