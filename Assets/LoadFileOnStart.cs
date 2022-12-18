using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadFileOnStart : MonoBehaviour
{
    public static LoadFileOnStart ins { get; private set; }

    public static string fileToLoadOnStart = "";

    private void Awake()
    {
        if (ins != null && ins != this)
        {
            Destroy(this);
        }
        else
        {
            ins = this;
        }
    }


    public void LoadFile()
    {
        if (string.IsNullOrEmpty(fileToLoadOnStart)) return;
        ReferenceController.ins.ModeController.Mode2D();
        BuildingSerializer.ins.LoadFromFile(fileToLoadOnStart);
    }

}
