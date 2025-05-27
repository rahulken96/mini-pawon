using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IBendaDapurParent
{
    public static event EventHandler OnAnyObjDitempatkan;

    public static void ResetStaticData()
    {
        OnAnyObjDitempatkan = null;
    }

    [SerializeField] private Transform counterTopPoint;

    private FungsiBendaDapur fungsiBendaDapur;

    public virtual void getInteract(PemainController pemain)
    {
        Debug.LogError("Interaksi virtual method");
    }
    
    public virtual void getMotong(PemainController pemain)
    {
        Debug.LogError("Interaksi motong virtual method");
    }

    public Transform GetObjBendaDapurFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetObjBendaDapur(FungsiBendaDapur fungsiBendaDapur)
    {
        this.fungsiBendaDapur = fungsiBendaDapur;

        if (fungsiBendaDapur != null)
        {
            OnAnyObjDitempatkan?.Invoke(this, EventArgs.Empty);
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
