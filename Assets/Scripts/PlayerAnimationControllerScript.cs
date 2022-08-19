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
    int[] decoderArray = new int[8];
    bool isMoving;

    private void Start()
    {
        animator = GetComponent<Animator>();

        decoderArray[(int)Dir.Left] = 0;
        decoderArray[(int)Dir.TopLeft] = 1;
        decoderArray[(int)Dir.Up] = 2;
        decoderArray[(int)Dir.TopRight] = 3;
        decoderArray[(int)Dir.Right] = 4;
        decoderArray[(int)Dir.BottomRight] = 5;
        decoderArray[(int)Dir.Down] = 6;
        decoderArray[(int)Dir.BottomLeft] = 7;

        //foreach (int a in decoderArray)
        //    Debug.Log(a);
    }

    public string GetAnimationNameFromDir(Vector2 vector)
    {
        Dir vectorsDir = Functions.Vector2ToDir(vector);

        return animNames[decoderArray[(int)vectorsDir]];
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
