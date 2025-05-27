using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        cuttingCounter.OnKepotong += CuttingCounter_OnKepotong;
    }

    private void CuttingCounter_OnKepotong(object sender, System.EventArgs e)
    {
        animator.SetTrigger("Cut");
    }
}
