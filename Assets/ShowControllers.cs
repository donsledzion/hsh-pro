using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ShowControllers : MonoBehaviour
{
    [SerializeField] bool _showController = false;

    public bool ShowController
    {
        get { return _showController; }
        set { _showController = value; }
    }
    void Update()
    {
        foreach(var hand in Player.instance.hands)
        {
            if (_showController)
            {
                hand.ShowController();
                hand.SetSkeletonRangeOfMotion(Valve.VR.EVRSkeletalMotionRange.WithController);
            }
            else
            {
                hand.HideController();
                hand.SetSkeletonRangeOfMotion(Valve.VR.EVRSkeletalMotionRange.WithoutController);
            }
        }

    }
}
