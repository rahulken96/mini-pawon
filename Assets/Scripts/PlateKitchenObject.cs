using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : FungsiBendaDapur
{
    public event EventHandler<OnTambahanBahanEventArgs> OnTambahanBahan;
    public class OnTambahanBahanEventArgs : EventArgs
    {
        public BendaDapur bendaDapur;
    }

    [SerializeField] private List<BendaDapur> bendaDapurValidList;

    private List<BendaDapur> bendaDapurList;

    private void Awake()
    {
        bendaDapurList = new List<BendaDapur>();
    }

    public bool TryAddBahanBahan(BendaDapur objBendaDapur)
    {
        //Validasi benda yang dapat di taruh di piring
        if (!bendaDapurValidList.Contains(objBendaDapur))
        {
            return false;
        }

        if (bendaDapurList.Contains(objBendaDapur))
        {
            return false;
        }
        else
        {
            bendaDapurList.Add(objBendaDapur);

            OnTambahanBahan?.Invoke(this, new OnTambahanBahanEventArgs
            {
                bendaDapur = objBendaDapur
            });

            return true;
        }
    }

    public List<BendaDapur> GetBendaDapurList()
    {
        return bendaDapurList;
    }
}
