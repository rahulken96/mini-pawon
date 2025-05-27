using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PemainController : MonoBehaviour, IBendaDapurParent
{
    public static PemainController Instance { get; private set; }

    public event EventHandler OnAmbilSesuatu;
    public event EventHandler<OnSelectedCounterEventArgs> OnSelectedCounter;
    public class OnSelectedCounterEventArgs : EventArgs
    {
        public BaseCounter selectedCounterArgs;
    }

    [SerializeField] private float vPemain = 7f;
    [SerializeField] GameInput gameInputScr;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform bendaDapurHoldPoint;

    private bool isWalking;
    private Vector3 lastArahInteraksi;
    private BaseCounter selectedCounter;
    private FungsiBendaDapur fungsiBendaDapur;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Ada lebih dari 1 Instance Pemain");
        }

        Instance = this;
    }

    void Start()
    {
        gameInputScr.OnAksiInteraksi += GameInputScr_OnAksiInteraksi;
        gameInputScr.OnAksiMotong += GameInputScr_OnAksiMotong;
    }

    private void GameInputScr_OnAksiMotong(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null)
        {
            selectedCounter.getMotong(this);
        }
    }

    private void GameInputScr_OnAksiInteraksi(object sender, System.EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null)
        {
            selectedCounter.getInteract(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        handleGerak();
        handleInteraksi();
    }

    public bool walkStatus()
    {
        return isWalking;
    }

    private void handleGerak()
    {
        Vector2 inputVector = gameInputScr.GetMovementVectorNormalized();
        Vector3 arahGerak = new Vector3(inputVector.x, 0f, inputVector.y);

        float jarakGerak = vPemain * Time.deltaTime;
        float radiusPemain = 0.6f;
        float tinggiPemain = 2f;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * tinggiPemain, radiusPemain, arahGerak, jarakGerak);

        if (!canMove)
        {
            Vector3 arahGerakX = new Vector3(arahGerak.x, 0, 0).normalized;
            canMove = (arahGerak.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * tinggiPemain, radiusPemain, arahGerakX, jarakGerak));

            //Cek jika hanya bisa gerak ke arah X saja
            if (canMove)
            {
                arahGerak = arahGerakX;
            }
            else
            {
                //Jika tidak bisa bergerak ke arah X
                Vector3 arahGerakZ = new Vector3(0, 0, arahGerak.z).normalized;
                canMove = (arahGerak.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * tinggiPemain, radiusPemain, arahGerakZ, jarakGerak));

                //Cek jika hanya bisa gerak ke arah Z saja
                if (canMove)
                {
                    arahGerak = arahGerakZ;
                }
            }
        }

        if (canMove)
        {
            transform.position += arahGerak * jarakGerak;
        }

        isWalking = arahGerak != Vector3.zero;
        transform.forward = Vector3.Slerp(transform.forward, arahGerak, Time.deltaTime * 10);
    }

    private void handleInteraksi()
    {
        Vector2 inputVector = gameInputScr.GetMovementVectorNormalized();
        Vector3 arahGerak = new Vector3(inputVector.x, 0f, inputVector.y);

        float jarakInteraksi = 2f;

        if (arahGerak != Vector3.zero)
        {
            lastArahInteraksi = arahGerak;
        }

        bool isHit = Physics.Raycast(transform.position, lastArahInteraksi, out RaycastHit raycastHit, jarakInteraksi, countersLayerMask);

        if (isHit)
        {
            bool isGetComponent = raycastHit.transform.TryGetComponent(out BaseCounter baseCounter);

            if (isGetComponent)
            {
                if (baseCounter != selectedCounter)
                {
                    setSelectedCounterArgs(baseCounter);
                }
            }
            else
            {
                setSelectedCounterArgs(null);
            }
        }
        else
        {
            setSelectedCounterArgs(null);
        }
    }

    private void setSelectedCounterArgs(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounter?.Invoke(this, new OnSelectedCounterEventArgs
        {
            selectedCounterArgs = selectedCounter
        });
    }

    public Transform GetObjBendaDapurFollowTransform()
    {
        return bendaDapurHoldPoint;
    }

    public void SetObjBendaDapur(FungsiBendaDapur fungsiBendaDapur)
    {
        this.fungsiBendaDapur = fungsiBendaDapur;

        if (fungsiBendaDapur != null)
        {
            OnAmbilSesuatu?.Invoke(this, EventArgs.Empty);
        }
    }

    public FungsiBendaDapur GetObjBendaDapur()
    {
        return fungsiBendaDapur;
    }

    public void ClearObjBendaDapur()
    {
        fungsiBendaDapur = null;
    }

    public bool HasObjBendaDapur()
    {
        return fungsiBendaDapur != null;
    }
}
