using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class RaceFinishedCanvas : MonoBehaviour
{
    [SerializeField] private TMP_Text medalText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setMedal(string medal)
    {
        medalText.text = medal;
    }
}
