using System.Collections.Generic;
using UnityEngine;

public class FireAbleView : MonoBehaviour
{
    public MeshRenderer meshWithMaterialsToChange;
    public List<Material> materials;
    public FireExtinguisherView automaticExtinguisher;

    private float energy = 100;

    private void Start()
    {
        if (meshWithMaterialsToChange == null)
        {
            meshWithMaterialsToChange = GetComponent<MeshRenderer>();
        }
    }

    public void AddDamage(float damage)
    {
        if (automaticExtinguisher != null)
        {
            automaticExtinguisher.ExtinguisherPress(true);
            automaticExtinguisher.ExtinguisherWork();
        }
        energy -= damage;
        if (energy < 0)
        {
            tag = "Untagged";
        }

        if (materials != null && meshWithMaterialsToChange != null)
        {
            int materialIndex = (int)((materials.Count - 1) * energy / 100);
            if (materialIndex < 0)
            {
                materialIndex = 0;
            }
            else if (materialIndex > (materials.Count - 1))
            {
                materialIndex = materials.Count - 1;
            }

            meshWithMaterialsToChange.material = materials[materialIndex];
        }
    }
}
