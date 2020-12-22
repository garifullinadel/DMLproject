using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIsTaked : MonoBehaviour
{
    [SerializeField] private bool isTaked = false;
    public bool IsTaked
    {
        get => isTaked;
        set => isTaked = value;
    }
}
