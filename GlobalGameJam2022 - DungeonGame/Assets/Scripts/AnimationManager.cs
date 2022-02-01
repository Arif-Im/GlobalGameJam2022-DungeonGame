using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] string leftIdleAnimation;
    [SerializeField] string rightIdleAnimation;
    [SerializeField] string upIdleAnimation;
    [SerializeField] string downIdleAnimation;
    [SerializeField] string leftWalkAnimation;
    [SerializeField] string rightWalkAnimation;
    [SerializeField] string upWalkAnimation;
    [SerializeField] string downWalkAnimation;

    public enum Animations { LEFTIDLE, RIGHTIDLE, UPIDLE, DOWNIDLE, LEFTWALK, RIGHTWALK, UPWALK, DOWNWALK };

    public Animations currentState;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ChangeAnimationState(Animations newState)
    {
        if (currentState == newState) return;

        switch (newState)
        {
            case Animations.LEFTIDLE:
                anim.Play(leftWalkAnimation);
                break;
            case Animations.RIGHTIDLE:
                anim.Play(rightIdleAnimation);
                break;
            case Animations.UPIDLE:
                anim.Play(upIdleAnimation);
                break;
            case Animations.DOWNIDLE:
                anim.Play(downIdleAnimation);
                break;
            case Animations.LEFTWALK:
                anim.Play(leftWalkAnimation);
                break;
            case Animations.RIGHTWALK:
                anim.Play(rightWalkAnimation);
                break;
            case Animations.UPWALK:
                anim.Play(upWalkAnimation);
                break;
            case Animations.DOWNWALK:
                anim.Play(downWalkAnimation);
                break;
        }
    }
}
