using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class energyScript : MonoBehaviour
{
    public TMP_Text energyText;
    public float energy;
    public int aumento;

    // Descomentar
    private void Start()
    {
        energy = 10000;
    }
    void Update()
    {
        if (energy < 100)
        {
            energy += aumento * Time.deltaTime;
            energyText.text = energy.ToString("F0");
        }
    }
}
