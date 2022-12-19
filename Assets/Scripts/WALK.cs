using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WALK : MonoBehaviour
{
    void Update()
    {
            gameObject.transform.Translate(Vector3.forward * 3 * Time.deltaTime);
    }
}
