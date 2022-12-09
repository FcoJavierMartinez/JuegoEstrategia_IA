using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class buttonScript : MonoBehaviour
{
    public energyScript energy;

    public int gastoEnergiaBomba;
    public int gastoEnergiaTanque;
    public int gastoEnergiaMele;
    public int gastoEnergiaMago;
    public int gastoEnergiaArquero;

    //Bomba
    public void gastarEnergiaBomba()
    {
        if (energy.energy > gastoEnergiaBomba)
        {
            energy.energy -= gastoEnergiaBomba;
        }
    }

    //Tanque
    public void gastarEnergiaTanque()
    {
        if (energy.energy > gastoEnergiaTanque)
        {
            energy.energy -= gastoEnergiaTanque;
        }
    }

    //Mele
    public void gastarEnergiaMele()
    {
        if (energy.energy > gastoEnergiaMele)
        {
            energy.energy -= gastoEnergiaMele;
        }
    }

    //Mago
    public void gastarEnergiaMago()
    {
        if (energy.energy > gastoEnergiaMago)
        {
            energy.energy -= gastoEnergiaMago;
        }
    }

    //Arquero
    public void gastarEnergiaArquero()
    {
        if (energy.energy > gastoEnergiaArquero)
        {
            energy.energy -= gastoEnergiaArquero;
        }
    }
}
