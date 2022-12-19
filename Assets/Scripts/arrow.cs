using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{

    public int vel = 3;
    public double hitbox = 0.25;
    public GameObject target;
    public int danio = 50;

    void Update()
    {   
        if(target) {
            gameObject.transform.LookAt(target.transform);
            gameObject.transform.Translate(Vector3.forward * vel * Time.deltaTime);

            if(Vector3.Distance(gameObject.transform.position, target.transform.position) <= hitbox) {
                stats unidad = target.GetComponent<stats>();
                unidad.vida -= danio;
                Destroy(gameObject);
            }

        } else {
            Destroy(gameObject);
        }
    }
}
