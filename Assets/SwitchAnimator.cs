using UnityEngine;
using System.Collections;

public class SwitchAnimator : MonoBehaviour
{


    Animator animator;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Assets/MainCharacter/MainCharacter.controller", typeof(RuntimeAnimatorController));
        }

        

    }
}
