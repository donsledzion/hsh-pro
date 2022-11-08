using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedItem : MonoBehaviour
{
    [SerializeField] Material _selectedMaterial;
    

    public GameObject SelectedGameObject { get; set; }
    public GameObject Phantom { get; set; }
}
