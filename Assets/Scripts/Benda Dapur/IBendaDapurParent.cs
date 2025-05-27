using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBendaDapurParent
{
    public Transform GetObjBendaDapurFollowTransform();

    public void SetObjBendaDapur(FungsiBendaDapur fungsiBendaDapur);

    public FungsiBendaDapur GetObjBendaDapur();

    public void ClearObjBendaDapur();

    public bool HasObjBendaDapur();
}
