using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FireController : MonoBehaviour
{
    #region instance
    public static FireController Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = (FireController)FindObjectOfType(typeof(FireController));
            }
            if (m_Instance == null)
            {
                GameObject singletonObject = new GameObject();
                m_Instance = singletonObject.AddComponent<FireController>();
                singletonObject.name = typeof(FireController).ToString();
            }
            return m_Instance;
        }
    }
    private static FireController m_Instance;
    #endregion

    #region Static fields and properties
    public static int startFileSeconds = 0;
    #endregion

    #region data fields and properties
    public Vector3 randomSpawnFireMinBounds, randomSpawnFireMaxBounds;
    public bool spawnOnStart = false, spawnDelayedAutomatically = false;
    public FireView firePrefab;

    public List<Transform> firePlaceViews;

    public List<FireView> currentFireList;

    public float maxFireDistance = 1.4f;
    public float minFireDistance = 0.35f;
    public float timeUntillFireSpread = 15;
    public float maxFireScale = 0.95f;
    private bool fireStarted = false, extinguishionStarted = false;

    public UnityEvent onExtinguishionStart, onFireDefeated;
    #endregion

    private void Start()
    {
        if (spawnOnStart)
        {
            Invoke("SpawnFireDelay", 1);
        }
        else if (spawnDelayedAutomatically)
        {
            Invoke("SpawnFireDelay", startFileSeconds);
        }
    }

    public void SpawnNewFire(float maxFireDistance, float minFireDistance, float timeUntillFireSpread, GameObject passedGameObject = null)
    {
        RemoveNullFilesFromList();
        if (passedGameObject != null)
        {
            FireView instance = FindFirstFreeFire();
            if (instance != null)
            {
                instance.Setup(maxFireDistance, minFireDistance, timeUntillFireSpread, passedGameObject, maxFireScale);
            }
            else
            {
                Debug.LogWarning("Cannot add fire, no free instance of fire!");
            }
        }
        else
        {
            Debug.LogWarning("Cannot spawn fire, no passed gameObject!");
        }

        int fogLvl = 0;
        if (currentFireList != null)
        {
            foreach (FireView fire in currentFireList)
            {
                if (fire != null)
                {
                    if (fire.isFiring)
                    {
                        fogLvl++;
                    }
                }
            }
        }
        // TODO fog? FogController.Instance.SetFogLevel(fogLvl);
    }

    public bool FireDidStarted()
    {
        if (!fireStarted && currentFireList != null)
        {
            foreach (FireView fire in currentFireList)
            {
                if (fire != null)
                {
                    if (fire.isFiring)
                    {
                        fireStarted = true;
                        break;
                    }
                }
            }
        }
        return fireStarted;
    }

    public void CheckIfIasSAtillFiring()
    {
        if (currentFireList != null)
        {
            foreach (FireView fire in currentFireList)
            {
                if (fire != null && fire.isFiring)
                {
                    return;
                }
            }
        }
        onFireDefeated?.Invoke();
        // FogController.Instance.SetFogLevel(0);
    }

    internal void ExtinguishionStarted()
    {
        if (!extinguishionStarted && FireDidStarted())
        {
            extinguishionStarted = true;
            onExtinguishionStart?.Invoke();
        }
    }

    private FireView FindFirstFreeFire()
    {
        if (currentFireList != null)
        {
            foreach (FireView fire in currentFireList)
            {
                if (fire != null)
                {
                    if (!fire.isFiring)
                    {

                        return fire;
                    }
                }
            }
        }
        return null;
    }

    /// <summary>
    /// Checks if there is a fire in range
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <param name="minimalDistance"></param>
    /// <returns></returns>
    internal bool NoFireInRange(Vector3 worldPosition, float minimalDistance)
    {
        if (currentFireList != null)
        {
            foreach (FireView fire in currentFireList)
            {
                if (fire != null)
                {
                    if (Vector3.Distance(fire.gameObject.transform.position, worldPosition) <= minimalDistance)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    public void SpawnFireDelay()
    {
        CancelInvoke("SpawnFireDelay");
        fireStarted = true;
        if (firePlaceViews != null && firePlaceViews.Count > 0)
        {
            Transform spawnPoint = firePlaceViews[UnityEngine.Random.Range(0, firePlaceViews.Count)];// firePlaceView.GetRandomFireSpawn;
            SpawnNewFire(maxFireDistance, minFireDistance, timeUntillFireSpread, spawnPoint.gameObject);
        }
        else
        {
            Debug.LogWarning("Cannot spawn fire, no any place!!");
        }
    }

    //private void AddFire(FireView instance)
    //{
    //    if (currentFireList == null)
    //    {
    //        currentFireList = new List<FireView>();
    //    }
    //    RemoveNullFilesFromList();
    //    if (instance != null)
    //    {
    //        currentFireList.Add(instance);
    //    }
    //}

    private void RemoveNullFilesFromList()
    {
        List<FireView> newFireList = new List<FireView>();
        foreach (FireView fireView in currentFireList)
        {
            if (fireView != null)
            {
                newFireList.Add(fireView);
            }
        }
        currentFireList = newFireList;
    }
}
