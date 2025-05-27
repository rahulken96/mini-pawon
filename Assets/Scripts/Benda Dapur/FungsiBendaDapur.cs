using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class FungsiBendaDapur : MonoBehaviour
{
    [SerializeField] private BendaDapur BendaDapurSO;

    private IBendaDapurParent bendaDapurParent;

    public BendaDapur GetBendaDapurSO()
    {
        return BendaDapurSO;
    }

    public void SetBendaDapurParent(IBendaDapurParent bendaDapurParent)
    {
        if(this.bendaDapurParent != null)
        {
            this.bendaDapurParent.ClearObjBendaDapur();
        }

        this.bendaDapurParent = bendaDapurParent;

        if(bendaDapurParent.HasObjBendaDapur() )
        {
            Debug.LogError("Error: counter already object");
        }

        bendaDapurParent.SetObjBendaDapur(this);

        transform.parent = bendaDapurParent.GetObjBendaDapurFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IBendaDapurParent GetBendaDapurParent()
    {
        return bendaDapurParent;
    }

    public void DestroyBendaDapur()
    {
        bendaDapurParent.ClearObjBendaDapur();
        Destroy(gameObject);
    }

    public bool TryGetPiring(out PlateKitchenObject plateKitchenObject)
    {
        if(this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }

    public static FungsiBendaDapur MunculkanBendaDapur(BendaDapur objBendaDapur, IBendaDapurParent bendaDapurParent)
    {
        Transform transformBendaDapur = Instantiate(objBendaDapur.prefab);
        FungsiBendaDapur fungsiBendaDapur = transformBendaDapur.GetComponent<FungsiBendaDapur>();

        fungsiBendaDapur.SetBendaDapurParent(bendaDapurParent);
        return fungsiBendaDapur;
    }
}
