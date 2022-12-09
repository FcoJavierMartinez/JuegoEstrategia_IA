using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonScript : MonoBehaviour
{
    public energyScript energy;

    public int gastoEnergiaBomba;
    public int gastoEnergiaTanque;
    public int gastoEnergiaMele;
    public int gastoEnergiaMago;
    public int gastoEnergiaArquero;

    public GameObject bomba;
    public GameObject tanque;
    public GameObject mele;
    public GameObject mago;
    public GameObject arquero;

    public GameObject posicionTorreCentral;
    public GameObject posicionTorreIzq;
    public GameObject posicionTorreDer;

    //Bomba
    public void invocarBomba()
    {
        if (energy.energy > gastoEnergiaBomba)
        {
            energy.energy -= gastoEnergiaBomba;
            switch(Random.Range(0, 3))
            {
                case 0: Instantiate(bomba, posicionTorreCentral.transform); break;
                case 1: Instantiate(bomba, posicionTorreIzq.transform); break;
                case 2: Instantiate(bomba, posicionTorreDer.transform); break;
            }
        }
    }

    //Tanque
    public void invocarTanque()
    {
        if (energy.energy > gastoEnergiaTanque)
        {
            energy.energy -= gastoEnergiaTanque;
            switch (Random.Range(0, 3))
            {
                case 0: Instantiate(tanque, posicionTorreCentral.transform); break;
                case 1: Instantiate(tanque, posicionTorreIzq.transform); break;
                case 2: Instantiate(tanque, posicionTorreDer.transform); break;
            }
        }
    }

    //Mele
    public void invocarMele()
    {
        if (energy.energy > gastoEnergiaMele)
        {
            energy.energy -= gastoEnergiaMele;
            switch (Random.Range(0, 3))
            {
                case 0: Instantiate(mele, posicionTorreCentral.transform); break;
                case 1: Instantiate(mele, posicionTorreIzq.transform); break;
                case 2: Instantiate(mele, posicionTorreDer.transform); break;
            }
        }
    }

    //Mago
    public void invocarMago()
    {
        if (energy.energy > gastoEnergiaMago)
        {
            energy.energy -= gastoEnergiaMago;
            switch (Random.Range(0, 3))
            {
                case 0: Instantiate(mago, posicionTorreCentral.transform); break;
                case 1: Instantiate(mago, posicionTorreIzq.transform); break;
                case 2: Instantiate(mago, posicionTorreDer.transform); break;
            }
        }
    }

    //Arquero
    public void invocarArquero()
    {
        if (energy.energy > gastoEnergiaArquero)
        {
            energy.energy -= gastoEnergiaArquero;
            switch (Random.Range(0, 3))
            {
                case 0: Instantiate(arquero, posicionTorreCentral.transform); break;
                case 1: Instantiate(arquero, posicionTorreIzq.transform); break;
                case 2: Instantiate(arquero, posicionTorreDer.transform); break;
            }
        }
    }
}
