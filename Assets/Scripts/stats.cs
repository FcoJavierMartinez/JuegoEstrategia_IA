using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stats : MonoBehaviour
{
    public int vida;
    private int vidaMax;
    public GameObject barra_vida;

    void Start()
    {
        vidaMax = vida;
        barra_vida = Instantiate(barra_vida, transform.position, Quaternion.identity);
    }

    void Update()
    {
        barra_vida.transform.localScale = new Vector3((float)vida/vidaMax*3, 0.4f, 0.01f);

        barra_vida.transform.LookAt(barra_vida.transform.position + Vector3.up);
        barra_vida.transform.position = gameObject.transform.position + Vector3.up * 10;

        if(vida <= 0) {
            if(gameObject.CompareTag("tower")) {
                towerScript torre = this.GetComponent<towerScript>();
                torre.destruir();
            }

            Destroy(barra_vida);
            Destroy(gameObject);
        }
    }
}
