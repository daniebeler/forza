using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GarageCanvas : MonoBehaviour
{
    [SerializeField] private TMP_Text carNameText;

    [SerializeField] private Button selectButton, leftButton, rightButton;

    [SerializeField] private GarageController garageController;

    public void setCarNameText(string name)
    {
        carNameText.text = name;
    }

    public void selectCar()
    {
        garageController.selectCar();
    }

    public void updateSelectButton(bool carIsSelected)
    {
        if (carIsSelected)
        {
            selectButton.interactable = false;
            selectButton.GetComponentInChildren<TMP_Text>().text = "selected";
        }
        else
        {
            selectButton.interactable = true;
            selectButton.GetComponentInChildren<TMP_Text>().text = "select";
        }
    }

    public void enableLeftButton(bool state)
    {
        leftButton.interactable = state;
    }
    
    public void enableRightButton(bool state)
    {
        rightButton.interactable = state;
    }
}
