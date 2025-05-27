using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProgressBar
{
    public event EventHandler<OnProgressEventArgs> OnProgressStatus;

    public class OnProgressEventArgs : EventArgs
    {
        public float progressNormalized;
    }
}
