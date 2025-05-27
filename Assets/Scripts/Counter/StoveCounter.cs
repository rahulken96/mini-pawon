using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter, IProgressBar
{
    public event EventHandler<IProgressBar.OnProgressEventArgs> OnProgressStatus;
    public event EventHandler<OnStateMasakEventArgs> OnStateMasak;

    public class OnStateMasakEventArgs : EventArgs
    {
        public State state;
    }

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    [SerializeField] private MasakanBendaDapur[] masakanBendaDapurArr;
    [SerializeField] private GosongBendaDapur[] gosongBendaDapurArr;

    private State state;
    private float fryingProgress;
    private float burningProgress;
    private MasakanBendaDapur masakanBendaDapur;
    private GosongBendaDapur gosongBendaDapur;

    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        if (HasObjBendaDapur())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingProgress += Time.deltaTime;

                    OnProgressStatus?.Invoke(this, new IProgressBar.OnProgressEventArgs
                    {
                        progressNormalized = fryingProgress / masakanBendaDapur.fryingProgressMax,
                    });

                    if (fryingProgress > masakanBendaDapur.fryingProgressMax)
                    {
                        GetObjBendaDapur().DestroyBendaDapur();

                        FungsiBendaDapur.MunculkanBendaDapur(masakanBendaDapur.output, this);

                        state = State.Fried;
                        burningProgress = 0f;
                        
                        gosongBendaDapur = GetGosongBendaDapurWithInput(GetObjBendaDapur().GetBendaDapurSO());

                        OnStateMasak?.Invoke(this, new OnStateMasakEventArgs
                        {
                            state = state
                        });
                    }

                    break;
                case State.Fried:
                    burningProgress += Time.deltaTime;

                    OnProgressStatus?.Invoke(this, new IProgressBar.OnProgressEventArgs
                    {
                        progressNormalized = burningProgress / gosongBendaDapur.burningProgressMax,
                    });

                    if (burningProgress > gosongBendaDapur.burningProgressMax)
                    {
                        GetObjBendaDapur().DestroyBendaDapur();

                        FungsiBendaDapur.MunculkanBendaDapur(gosongBendaDapur.output, this);
                        
                        state = State.Burned;

                        OnStateMasak?.Invoke(this, new OnStateMasakEventArgs
                        {
                            state = state
                        });

                        OnProgressStatus?.Invoke(this, new IProgressBar.OnProgressEventArgs
                        {
                            progressNormalized = 0f,
                        });
                    }

                    break;
                case State.Burned:
                    break;
            }
        }
    }

    public override void getInteract(PemainController pemain)
    {
        //Cek jika tidak ada benda di atas counter
        if (!HasObjBendaDapur())
        {
            //Cek jika pemain bawa barang
            if (pemain.HasObjBendaDapur())
            {
                //Cek jika pemain bawa barang yang bisa dimasak
                if (HasRecipeWithInput(pemain.GetObjBendaDapur().GetBendaDapurSO()))
                {
                    pemain.GetObjBendaDapur().SetBendaDapurParent(this);

                    masakanBendaDapur = GetMasakanBendaDapurWithInput(GetObjBendaDapur().GetBendaDapurSO());

                    state = State.Frying;
                    fryingProgress = 0f;

                    OnStateMasak?.Invoke(this, new OnStateMasakEventArgs
                    {
                        state = state
                    });

                    OnProgressStatus?.Invoke(this, new IProgressBar.OnProgressEventArgs
                    {
                        progressNormalized = fryingProgress / masakanBendaDapur.fryingProgressMax,
                    });
                }
            }
        }
        else
        {
            //Cek jika pemain sedang bawa barang
            if (pemain.HasObjBendaDapur())
            {
                //Cek jika pemain membawa piring
                if (pemain.GetObjBendaDapur().TryGetPiring(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddBahanBahan(GetObjBendaDapur().GetBendaDapurSO()))
                    {
                        GetObjBendaDapur().DestroyBendaDapur();

                        state = State.Idle;

                        OnStateMasak?.Invoke(this, new OnStateMasakEventArgs
                        {
                            state = state
                        });

                        OnProgressStatus?.Invoke(this, new IProgressBar.OnProgressEventArgs
                        {
                            progressNormalized = 0f,
                        });
                    }
                }
            }
            else
            {
                GetObjBendaDapur().SetBendaDapurParent(pemain);

                state = State.Idle;

                OnStateMasak?.Invoke(this, new OnStateMasakEventArgs
                {
                    state = state
                });

                OnProgressStatus?.Invoke(this, new IProgressBar.OnProgressEventArgs
                {
                    progressNormalized = 0f,
                });
            }
        }
    }

    private bool HasRecipeWithInput(BendaDapur inputBendaDapur)
    {
        MasakanBendaDapur objMasakanBendaDapur = GetMasakanBendaDapurWithInput(inputBendaDapur);
        return (objMasakanBendaDapur != null);
    }

    public BendaDapur GetOutputForInput(BendaDapur inputBendaDapur)
    {
        MasakanBendaDapur objMasakanBendaDapur = GetMasakanBendaDapurWithInput(inputBendaDapur); 

        if(objMasakanBendaDapur != null)
        {
            return objMasakanBendaDapur.output;
        }
        else
        {
            return null;
        }
    }

    private MasakanBendaDapur GetMasakanBendaDapurWithInput (BendaDapur inputBendaDapur)
    {
        foreach (MasakanBendaDapur objMasakanBendaDapur in masakanBendaDapurArr)
        {
            if (objMasakanBendaDapur.input == inputBendaDapur)
            {
                return objMasakanBendaDapur;
            }

        }

        return null;
    }
    
    private GosongBendaDapur GetGosongBendaDapurWithInput (BendaDapur inputBendaDapur)
    {
        foreach (GosongBendaDapur objGosongBendaDapur in gosongBendaDapurArr)
        {
            if (objGosongBendaDapur.input == inputBendaDapur)
            {
                return objGosongBendaDapur;
            }

        }

        return null;
    }
}
