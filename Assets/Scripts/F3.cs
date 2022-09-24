using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class F3 : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textFPS;

    void Update()
    {
        textFPS.text = ((int)(1f / Time.unscaledDeltaTime)).ToString() + " FPS";
    }
}
