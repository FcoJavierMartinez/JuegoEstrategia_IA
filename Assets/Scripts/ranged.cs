using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ranged : MonoBehaviour
{
    private GameObject target, target2;
    private float distancia, distancia_aux;
    private float distancia2, distancia_aux2;
    private GameObject closest;
    private GameObject closest2;
    public LayerMask enemyMask;
    private Animator m_Animator;
    [Range(0.1f,2)]
    public float atRatio;
    public float atRango;
    private float atTimer;
    public GameObject punto_tiro;
    public GameObject bala;
    private GameObject[] torres;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        atTimer = atRatio;
        distancia = atRango;
        distancia2 = 200;
        
        m_Animator.SetBool("IsWalking", false);
        m_Animator.SetBool("IsAttacking", false);
    }

    void Update()
    {
        buscarTarget();
        if(target != null && atTimer >= atRatio) {
            
            gameObject.transform.LookAt(target.transform);

            if(Vector3.Distance(target.transform.position, this.transform.position) < atRango) {   
                m_Animator.SetBool("IsWalking", false);
                m_Animator.SetBool("IsAttacking", true);
                disparar();
                atTimer = 0;
            } else {
                target = null;
                distancia = atRango;
            }
        } else {
            m_Animator.SetBool("IsWalking", true);
            m_Animator.SetBool("IsAttacking", false);
            // Codigo pathfinding hacia torre (target2)
            // Solo se para para atacar
            atTimer += Time.deltaTime;
            buscarTorre();
        }
    }

    void buscarTarget() {
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
        if (gameObject.CompareTag("wizard")) {
            fireball proyectil2;
            tiro = Instantiate(bala, punto_tiro.transform.position, Quaternion.identity);
            proyectil2 = tiro.GetComponent<fireball>();
            proyectil2.target = this.target;
            proyectil2.enemyMask = this.enemyMask;
        } else {
            arrow proyectil;
            tiro = Instantiate(bala, punto_tiro.transform.position, Quaternion.identity);
            proyectil = tiro.GetComponent<arrow>();
            proyectil.target = this.target;
        }
    }    

    /*
    Mientras no tenga enemigos cerca, camina hacia la torre mas cercana
    */
    void buscarTorre() {
        if(target2 == null) {
            torres = GameObject.FindGameObjectsWithTag("tower");
            foreach (GameObject torre in torres) {
                if(torre.layer == enemyMask) {
                    distancia_aux2 = Vector3.Distance(this.transform.position, torre.transform.position);
                    if(distancia_aux < distancia2) {
                        closest2 = torre;
                        distancia2 = distancia_aux;
                    }
                }
            }

            target2 = closest2;
        }
    }
    
    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, atRango);
    }
}