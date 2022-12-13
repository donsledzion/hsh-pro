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
        Debug.Log("<color=green>Enabling phantom renderer!</color>");

        SetToState(true);
        /*_renderer.enabled = true;
        _collider.enabled = true;*/
    }

    public void Disable()
    {
        Debug.Log("<color=red>Disablinng phantom renderer!</color>");
        SetToState(false);
        /*_renderer.enabled = false;
        _collider.enabled = false;*/
    }

    public void SetToState(bool state)
    {
        _renderer.enabled = state;
        _collider.enabled = state;
    }

    private void OnDisable()
    {
        Debug.Log("<color=red><b>Who dared to disable me?!</b></color>");
    }




}
