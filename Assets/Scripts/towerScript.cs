using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerScript : MonoBehaviour
{
    public GameObject torreDestruida;
    public bool torrePrincpDestruida = false;
    public bool torrePrincpEnemigaDestruida = false;

    [Range(0.5f,1)]
    public float atRatio;
    public float atRango;
    private float atTimer;
    public GameObject bala;
    public GameObject punto_tiro;

    private GameObject target;

    public bool activa;
    private float distancia;
    private GameObject closest;
    private float distancia_aux;
    public LayerMask enemyMask;

    void Start() {
        atTimer = atRatio;
        distancia = 16;
    }

    void Update()
    {
        if(activa) {
            buscarEnemigos();
            if(target != null && atTimer >= atRatio) {
                if (Vector3.Distance(target.transform.position, this.transform.position) < atRango) {
                    disparar();
                    atTimer = 0;
                } else {
                    target = null;
                    distancia = 16;
                }
            } else {
                atTimer += Time.deltaTime;
            }
        }
    }

    void buscarEnemigos() {
        if(target == null) {
            Collider[] enemigos = Physics.OverlapSphere(transform.position, atRango, enemyMask);
            
            if(enemigos == null) {
                target = null;
            } else {
                foreach (Collider enemigo in enemigos) {
                    distancia_aux = Vector3.Distance(this.transform.position, enemigo.transform.position);
                    if(distancia_aux < distancia) {
                        closest = enemigo.gameObject;
                        distancia = distancia_aux;
                    }
                }

                target = closest;
            }
        }
    }

    void disparar() {
        GameObject tiro;
        arrow proyectil;
        tiro = Instantiate(bala, punto_tiro.transform.position, Quaternion.identity);
        proyectil = tiro.GetComponent<arrow>();
        proyectil.target = this.target;
    }

    public void destruir() {
        if (gameObject.tag == "TowerPrincipal") { 
            torrePrincpDestruida = true;
        }

        if (gameObject.tag == "enemyTowerPrincipal") {
            torrePrincpEnemigaDestruida = true;
        }

        Instantiate(torreDestruida);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, atRango);
    }
}