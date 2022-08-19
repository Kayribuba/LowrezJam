using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationControllerScript : MonoBehaviour
{
    [Header("Clockwise, starting from left")]
    [SerializeField] string[] animNames = new string[] { "Left", "TopLeft", "Up", "TopRight", "Right", "BottomRight", "Down", "BottomLeft" };

    string currentAnimationPlaying = "";

    Animator animator;

    bool isMoving;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public string GetAnimationNameFromDir(Vector2 vector)
    {
        Dir vectorsDir = Functions.Vector2ToDir(vector);

        return animNames[(int)vectorsDir];
    }
    public void PlayAnimationOfName(string nameOfAnim)
    {
        if (currentAnimationPlaying != nameOfAnim)
        {
            if (isMoving)
                nameOfAnim = "RUN" + nameOfAnim;

            if (!animator.HasState(0, Animator.StringToHash(nameOfAnim)))
                nameOfAnim = nameOfAnim.Substring(3);

            if (animator.HasState(0, Animator.StringToHash(nameOfAnim)))
                animator.Play(nameOfAnim);
            else
                Debug.Log($"ERROR - Animation {nameOfAnim} cannot be found in the current animator.");
        }
    }
    public void SetIsMoving(bool isIt)
    {
        isMoving = isIt;
    }
}
