using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateSingleIconUI : MonoBehaviour
{
    [SerializeField] private Image image;

    public void SetBendaDapur(BendaDapur objBendaDapur)
    {
        image.sprite = objBendaDapur.spirte;
    }
}
