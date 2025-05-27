using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    
    public event EventHandler OnAksiInteraksi;
    public event EventHandler OnAksiMotong;
    public event EventHandler OnAksiPause;

    private AksiInputPemain aksiInputPemain;

    private void Awake()
    {
        Instance = this;

        aksiInputPemain = new AksiInputPemain();
        aksiInputPemain.Pemain.Enable();

        aksiInputPemain.Pemain.Interaksi.performed += Interaksi_performed;
        aksiInputPemain.Pemain.Motong.performed += Motong_performed;
        aksiInputPemain.Pemain.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        aksiInputPemain.Pemain.Interaksi.performed -= Interaksi_performed;
        aksiInputPemain.Pemain.Motong.performed -= Motong_performed;
        aksiInputPemain.Pemain.Pause.performed -= Pause_performed;

        aksiInputPemain.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnAksiPause?.Invoke(this, EventArgs.Empty);
    }

    private void Motong_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnAksiMotong?.Invoke(this, EventArgs.Empty);
    }

    private void Interaksi_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnAksiInteraksi?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = aksiInputPemain.Pemain.AksiGerak.ReadValue<Vector2>();
        return inputVector;
    }
}
