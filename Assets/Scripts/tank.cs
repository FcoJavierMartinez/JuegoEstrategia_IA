using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tank : MonoBehaviour
{
    private GameObject target;
    private float distancia = 200;
    private float distancia_aux;
    private float distancia_min = 2f;
    private GameObject closest;
    private GameObject[] torres;
    public int enemyLayer;
    private Animator m_Animator;
    [Range(1,10)]
    public float atRatio;
    private float atTimer;
    public int danio;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        atTimer = atRatio;
        buscarTarget();
        
        m_Animator.SetBool("IsWalking", false);
        m_Animator.SetBool("IsAttacking", false);
    }

    void Update()
    {
        if (target != null) {

            gameObject.transform.LookAt(target.transform);

            m_Animator.SetBool("IsWalking", true);
            m_Animator.SetBool("IsAttacking", false);
            // Codigo pathfinding (solo se para al llegar a la torre)

            if(Vector3.Distance(target.transform.transform.position, this.transform.position) <= distancia_min && atTimer >= atRatio) {
                m_Animator.SetBool("IsWalking", false);
                m_Animator.SetBool("IsAttacking", true);
                stats objetivo = target.GetComponent<stats>();
                objetivo.vida -= danio;
                atTimer = 0;
            } else {
                m_Animator.SetBool("IsWalking", false);
                m_Animator.SetBool("IsAttacking", false);
                atTimer += Time.deltaTime;
            }
        } else {
            buscarTarget();
        }
    }

    void buscarTarget() {
        if(target == null) {
            torres = GameObject.FindGameObjectsWithTag("tower");
            foreach (GameObject torre in torres) {
                if(torre.layer == enemyLayer) {
                    distancia_aux = Vector3.Distance(this.transform.position, torre.transform.position);
                    if(distancia_aux < distancia) {
                        closest = torre;
                        distancia = distancia_aux;
                    }
                }
            }

            target = closest;
        }
    }
}