using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputFiel : MonoBehaviour
{
    public TMP_InputField target;
    public ClientSide aa;

    private void Update()
    {
        aa.sendingDataTempt = target.text;
    }
}
