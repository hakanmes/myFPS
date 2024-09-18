using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float lifePoints = 100f;

    public void TakeDamage(float damage)
    {
        lifePoints -= damage;

        if(lifePoints <= 0)
        {
            GetComponent<DeathHandler>().HandleDeath();
        }   
    }
}
