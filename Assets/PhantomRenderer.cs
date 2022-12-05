using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PhantomRenderer : MonoBehaviour
{
    [SerializeField] Renderer _renderer;
    [SerializeField] Collider _collider;
    [SerializeField] JoineryType _joineryType;
    public JoineryType JoineryType => _joineryType;

    public bool HasJoinery => gameObject.GetComponentsInChildren<Joinery>().Length > 0;

    public void Enable()
    {
        _renderer.enabled = true;
        _collider.enabled = true;
    }

    public void Disable()
    {
        _renderer.enabled = false;
        _collider.enabled = false;
    }

    


}
