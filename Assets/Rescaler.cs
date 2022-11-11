using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rescaler : MonoBehaviour
{
    [SerializeField] Vector3 originalScale = Vector3.one;
    [SerializeField] Vector3 targetScale = new Vector3(0.01f,0.01f,0.01f);

    [ContextMenu("Scale To Target")]
    public void ScaleToTarget()
    {
        Scale(targetScale);
    }
    [ContextMenu("Scale Back")]
    public void ScaleBack()
    {
        Scale(originalScale);
    }

    void Scale(Vector3 scale)
    {
        transform.localScale = scale;
    }


}
