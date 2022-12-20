using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    public int vel = 6;
    public double hitbox = 0.25;
    public GameObject target;
    public int danio = 100;
    public LayerMask enemyMask;
    public float explosionRadius;
    public GameObject explosion;

    void Update()
    {
        if(target) {
            
            gameObject.transform.LookAt(target.transform);
            gameObject.transform.Translate(Vector3.forward * vel * Time.deltaTime);

            if(Vector3.Distance(gameObject.transform.position, target.transform.position) <= hitbox) {
                Collider[] objetivos = Physics.OverlapSphere(transform.position, explosionRadius, enemyMask);
                foreach (Collider objetivo in objetivos) {
                    stats unidad = objetivo.gameObject.GetComponent<stats>();
                    unidad.vida -= danio;
                }
                Instantiate(explosion, this.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        } else {
            Destroy(gameObject);
        }
    }
    
    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

}
