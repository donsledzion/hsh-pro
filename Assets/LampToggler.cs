using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampToggler : MonoBehaviour
{
    [SerializeField] List<Light> _lights = new List<Light>();

    public void Toggle()
    {
        foreach(Light light in _lights)
            light.enabled = !light.enabled;
    }
}
