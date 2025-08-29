using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public LayerMask WhatIsEnemies;
    public float exp = 5f;
    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject,10);
    }

    private void Exp()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, exp, WhatIsEnemies);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyAI>().TakeDamage(100);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Destroy(transform.GetComponent<Rigidbody>());
        Debug.Log("Hello1");
        if(collision.collider.CompareTag("Enemy"))
            Exp();
        
    }
}
