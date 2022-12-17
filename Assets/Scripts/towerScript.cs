using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerScript : MonoBehaviour
{
    public float vidaTorre;
    public GameObject torreDestruida;
    public bool torrePrincpDestruida = false;

    void Update()
    {
        if (vidaTorre <= 0)
        {
            if (gameObject.tag == "TowerPrincipal")
            {
                torrePrincpDestruida = true;
            }
            Destroy(gameObject);
            Instantiate(torreDestruida);
        }
    }
}
