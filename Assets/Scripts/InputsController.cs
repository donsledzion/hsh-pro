using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputsController : MonoBehaviour
{
    private string input;


    public void ReadStringInput(string s) {

        float temp = float.Parse(s);
        Debug.Log(input);
    }
}
