using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossFade : MonoBehaviour
{

    static Animator animator;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    public static void fadeOut()
    {
        animator.SetTrigger("fadeOut");
    } 

    public static void fadeIn()
    {
        animator.SetTrigger("fadeOut");
    }


}
