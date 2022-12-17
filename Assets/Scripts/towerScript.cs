using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerScript : MonoBehaviour
{
    public float vidaTorre;
    public GameObject torreDestruida;
    public bool torrePrincpDestruida = false;
    public bool torrePrincpEnemigaDestruida = false;

    void Update()
    {
        if (vidaTorre <= 0)
        {
            if (gameObject.tag == "TowerPrincipal") torrePrincpDestruida = true;
            if (gameObject.tag == "enemyTowerPrincipal") torrePrincpEnemigaDestruida = true;
            Destroy(gameObject);
            Instantiate(torreDestruida);
        }
    }
}
