using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisherView : MonoBehaviour
{
    public ParticleSystem particles;
    public Transform extinctionCenter, extinctionCenterB;
    public List<Collider> collidersList;
    
    private float energyMax = 80, currentEnergy;

    public bool isInHand = false;

    #region lifecycle methods
    private void Start()
    {
        currentEnergy = energyMax;
        if (particles != null)
        {
            particles.enableEmission = false;
        }
    }
    #endregion

    public void ExtinguisherWork()
    {
        currentEnergy -= 0.1f;
        Collider[] hitColliders = Physics.OverlapSphere(extinctionCenter.position, 0.8f);
        foreach (Collider collider in hitColliders)
        {
            if (collider.GetComponent<FireView>() != null)
            {
                collider.GetComponent<FireView>().HitFire(0.2f * currentEnergy / energyMax + 0.8f);
            }
        }
        hitColliders = Physics.OverlapSphere(extinctionCenterB.position, 0.6f);
        foreach (Collider collider in hitColliders)
        {
            if (collider.GetComponent<FireView>() != null)
            {
                collider.GetComponent<FireView>().HitFire(0.2f * currentEnergy / energyMax + 0.8f);
            }
        }
    }

    public void ExtinguisherPress(bool pressed)
    {
        if (particles != null && isInHand)
        {
            particles.enableEmission = (currentEnergy > 0 && pressed);
        }
    }

    public bool isHalon = false;
    public void ExtinguisherInHand(bool isInHand)
    {
        this.isInHand = isInHand;
        if (collidersList != null)
        {
            collidersList.ForEach(coll =>
            {
                if (coll != null)
                {
                    coll.enabled = !isInHand;
                }
            });
        }
    }


    //#region photon methods
    //[PunRPC]
    //public void RPC_Pressed(bool pressed)
    //{
    //    if (particles != null)
    //    {
    //        particles.enableEmission = pressed;
    //    }
    //}
  //  #endregion
}
