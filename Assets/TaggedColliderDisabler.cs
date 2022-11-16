using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaggedColliderDisabler : MonoBehaviour
{
    [SerializeField] string tagToDisable = "someTag";

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == tagToDisable)
            other.enabled = false;
    }
}
