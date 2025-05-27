using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    [SerializeField] private ContainerCounter containerCounter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.onPemainGetObj += ContainerCounter_onPemainGetObj; //Mengambil kejadian aksi dari pemain
    }

    private void ContainerCounter_onPemainGetObj(object sender, System.EventArgs e)
    {
        animator.SetTrigger("OpenClose");
    }
}
