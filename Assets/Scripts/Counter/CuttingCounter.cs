using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IProgressBar;

public class CuttingCounter : BaseCounter, IProgressBar
{
    public static event EventHandler OnAnyKepotong;

    new public static void ResetStaticData()
    {
        OnAnyKepotong = null;
    }

    public event EventHandler<IProgressBar.OnProgressEventArgs> OnProgressStatus;
    public event EventHandler OnKepotong;

    [SerializeField] private PotonganBendaDapur[] objPotonganBendaDapurArr;

    private int cuttingProgress;

    public override void getInteract(PemainController pemain)
    {
        //Cek jika tidak ada benda di atas counter
        if (!HasObjBendaDapur())
        {
            //Cek jika pemain bawa barang
            if (pemain.HasObjBendaDapur())
            {
                //Cek jika pemain bawa barang yang bisa dipotong
                if (HasRecipeWithInput(pemain.GetObjBendaDapur().GetBendaDapurSO()))
                {
                    pemain.GetObjBendaDapur().SetBendaDapurParent(this);
                    cuttingProgress = 0;

                    PotonganBendaDapur objPotonganBendaDapur = GetPotonganBendaDapurWithInput(GetObjBendaDapur().GetBendaDapurSO());

                    OnProgressStatus?.Invoke(this, new IProgressBar.OnProgressEventArgs
                    {
                        progressNormalized = (float) cuttingProgress / objPotonganBendaDapur.cuttingProgressMax,
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
                    }
                }
            }
            else
            {
                GetObjBendaDapur().SetBendaDapurParent(pemain);
            }
        }
    }

    public override void getMotong(PemainController pemain)
    {
        //Cek jika ada benda di atas counter
        if (
            HasObjBendaDapur() && 
            HasRecipeWithInput(GetObjBendaDapur().GetBendaDapurSO())
        )
        {
            cuttingProgress++;

            OnKepotong?.Invoke(this, EventArgs.Empty);
            OnAnyKepotong?.Invoke(this, EventArgs.Empty);

            PotonganBendaDapur objPotonganBendaDapur = GetPotonganBendaDapurWithInput(GetObjBendaDapur().GetBendaDapurSO());

            OnProgressStatus?.Invoke(this, new IProgressBar.OnProgressEventArgs
            {
                progressNormalized = (float)cuttingProgress / objPotonganBendaDapur.cuttingProgressMax,
            });

            if (cuttingProgress >= objPotonganBendaDapur.cuttingProgressMax)
            {
                BendaDapur outputBendaDapur = GetOutputForInput(GetObjBendaDapur().GetBendaDapurSO());

                GetObjBendaDapur().DestroyBendaDapur();

                FungsiBendaDapur.MunculkanBendaDapur(outputBendaDapur, this);
            }
        }
    }

    private bool HasRecipeWithInput(BendaDapur inputBendaDapur)
    {
        PotonganBendaDapur objPotonganBendaDapur = GetPotonganBendaDapurWithInput(inputBendaDapur);
        return (objPotonganBendaDapur != null);
    }

    public BendaDapur GetOutputForInput(BendaDapur inputBendaDapur)
    {
        PotonganBendaDapur objPotonganBendaDapur = GetPotonganBendaDapurWithInput(inputBendaDapur); 

        if(objPotonganBendaDapur != null)
        {
            return objPotonganBendaDapur.output;
        }
        else
        {
            return null;
        }
    }

    private PotonganBendaDapur GetPotonganBendaDapurWithInput (BendaDapur inputBendaDapur)
    {
        foreach (PotonganBendaDapur objPotonganBendaDapur in objPotonganBendaDapurArr)
        {
            if (objPotonganBendaDapur.input == inputBendaDapur)
            {
                return objPotonganBendaDapur;
            }

        }

        return null;
    }
}
