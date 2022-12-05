
using System.Collections.Generic;
using UnityEngine;

public class FireView : MonoBehaviour
{
    #region data fields and properties
    public GameObject visuals;
    public List<ParticleSystem> particlesFire, particlesSmoke;
    public Light fireLight;
    public AudioSource fireAudio;

    private float timeUntillFireSpread,
        currenttimeUntillFireSpread,
        maxFireDistance,
        minFireDistance;

    private GameObject firingGameObject;
    private FireAbleView firingGameObjectFireableView;
    public bool isFiring = false;

    [SerializeField]
    private float fireEnergy = 200, fireScale = 0.152f, maxFireScale = 1;
    #endregion

    public void Setup(float maxFireDistance, float minFireDistance, float timeUntillFireSpread, GameObject passedGameObjectToFireUp, float maxFireScale)
    {
       // ExtinguishersController.Instance.AnotherFireStarted();
        this.maxFireDistance = maxFireDistance;
        this.maxFireScale = maxFireScale;
        this.minFireDistance = minFireDistance;
        this.timeUntillFireSpread = timeUntillFireSpread;
        firingGameObject = passedGameObjectToFireUp;
        if (firingGameObject != null)
        {
            firingGameObjectFireableView = firingGameObject.GetComponent<FireAbleView>();
            transform.position = firingGameObject.transform.position;
            transform.parent = firingGameObject.transform;
        }
        else
        {
            Debug.LogWarning("Passed game object to fire up is null! Should not happen!");
        }
        currenttimeUntillFireSpread = timeUntillFireSpread;
        StartFire();
    }

    private GameObject FindNearbyObjectToFireUp()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, maxFireDistance + fireScale * 2);
        GameObject foundObject = null;
        float lowestDistance = maxFireDistance + fireScale *2;
        foreach (Collider collider in hitColliders)
        {
            
            if (Vector3.Distance(collider.transform.position, gameObject.transform.position) >= (minFireDistance + fireScale / 2)
                && FireController.Instance.NoFireInRange(collider.gameObject.transform.position, minFireDistance / 1.7f) &&
                collider.tag.ToLower().Equals("fireable"))
            {
                if (Vector3.Distance(gameObject.transform.position, collider.transform.position) < lowestDistance)
                {
                    lowestDistance = Vector3.Distance(gameObject.transform.position, collider.transform.position);
                    foundObject = collider.gameObject;
                }
            }
        }
        return foundObject;
    }

    /// <summary>
    /// Starts fire - visuals and logics
    /// </summary>
    [ContextMenu("Start Fire")]
    private void StartFire()
    {
        isFiring = true;
        visuals?.SetActive(isFiring);
        fireAudio.enabled = isFiring;
    }
    [ContextMenu("Stop Fire")]
    private void StopFire()
    {
        isFiring = false;
        visuals?.SetActive(isFiring);
        fireAudio.enabled = isFiring;
        FireController.Instance.CheckIfIasSAtillFiring();
    }


    public void ToggleFire()
    {
        if (isFiring)
            StopFire();
        else
            StartFire();
    }

    private bool startedfireextinguishionSent = false;
    public void HitFire(float hitPoints)
    {
        Debug.Log("Hitting fire with " + hitPoints + "hit points. Fire energy is: " + fireEnergy);
        if (hitPoints > 0)
        {
            fireEnergy -= hitPoints * 0.48f;
            if (!startedfireextinguishionSent)
            {
                startedfireextinguishionSent = true;
            }
            if (FireController.Instance != null)
                FireController.Instance.ExtinguishionStarted();
        }
    }

    private void Update()
    {
        if (isFiring)
        {
            currenttimeUntillFireSpread -= Time.deltaTime;
            if (fireScale < maxFireScale)
            {
                fireScale += 0.00023f;
            }

            UpdateScale();
            if (currenttimeUntillFireSpread <= 0)
            {
                currenttimeUntillFireSpread = timeUntillFireSpread - fireScale;
                SpawnAnotherFire();
            }
            if (firingGameObjectFireableView != null)
            {
                firingGameObjectFireableView.AddDamage(fireScale * 2);
            }
            // Ok, now fire will not die// fireEnergy -= Time.deltaTime;
            if (fireEnergy < 0)
            {
                StopFire();
                Debug.LogWarning("Fire died!");
            }
        }
    }


    private void UpdateScale()
    {
        if (fireLight != null)
        {
            fireLight.intensity = fireScale * 3;
        }
        // fire
        particlesFire?.ForEach(fire =>
        {
            fire.startSpeed = 1 + fireScale * 2;
            fire.transform.localScale = new Vector3(fireScale * 1.3f, fireScale * 1.3f, fireScale * 1.3f);
        });
        // smoke
        particlesSmoke?.ForEach(smoke =>
        {
            smoke.startSpeed = 0.48f + fireScale;
            smoke.transform.localScale = new Vector3(fireScale * 1.3f, fireScale * 1.3f, fireScale * 1.3f);
        });
        // audio
        if (fireAudio != null)
        {
            fireAudio.volume = fireScale*1.4f;
            fireAudio.pitch = 0.5f + fireScale / 3;
        }
        fireScale = Mathf.Clamp(fireEnergy / 160, 0.1f, 1f);
    }

    private void SpawnAnotherFire()
    {
        Debug.LogWarning("SpawnAnotherFire");
        GameObject foundObject = FindNearbyObjectToFireUp();
        if (foundObject != null)
        {
            Debug.LogWarning("Found object to fire up!");
            FireController.Instance.SpawnNewFire(maxFireDistance, minFireDistance, timeUntillFireSpread, foundObject);
        }
        else
        {
            Debug.LogWarning("Could not find object to fire up in distance " + (maxFireDistance + fireScale * 2));
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, maxFireDistance + fireScale * 2);
    }

    //#region photon methods
    //[PunRPC]
    //private void RPC_UpdatePosition(Vector3 position)
    //{
    //    Debug.Log("@RPC_UpdatePosition");
    //    transform.position = position;
    //}

    //[PunRPC]
    //private void RPC_UpdateEnergyAndScale(float fireEnergy, float fireScale, bool isFiring)
    //{
    //    this.fireEnergy = fireEnergy;
    //    this.fireScale = fireScale;
    //    this.isFiring = isFiring;
    //    if (isFiring)
    //    {
    //        StartFire();
    //    }
    //    else
    //    {
    //        StopFire();
    //    }
    //}
    //#endregion
}
