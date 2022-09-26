using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GarageCanvas : MonoBehaviour
{
    [SerializeField] private TMP_Text carNameText;

    public void setCarNameText(string name)
    {
        carNameText.text = name;
    }
}
