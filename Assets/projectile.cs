using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject,10);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Destroy(transform.GetComponent<Rigidbody>());
        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyAI>().TakeDamage(100);
        }
    }
}
