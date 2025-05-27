using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimasiPemainController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private PemainController pemainScr;


    private void Update()
    {
        animator.SetBool("isWalk", pemainScr.walkStatus());
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
}
