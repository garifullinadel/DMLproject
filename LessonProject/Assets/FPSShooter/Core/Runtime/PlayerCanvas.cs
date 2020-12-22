using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour
{
    public Text TotalAmmoText
    {
        get => totalAmmoText;
        set => totalAmmoText = value;
    }

    public Text CurrentAmmoText
    {
        get => currentAmmoText;
        set => currentAmmoText = value;
    }

    [Header("Canvas Settings")]
    // Canvas components.
    [SerializeField] private Text totalAmmoText;
    [SerializeField] private Text currentAmmoText;

    public void AmmoTextChange(string total, string current)
    {
        totalAmmoText.text = total;
        currentAmmoText.text = current;
    }
}
