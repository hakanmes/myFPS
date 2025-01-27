using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    bool isDead = false;

    public bool IsDead()
    {
        return isDead;
    }
    
    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken"); //bu metodun içinde cagırdığımız metod; aynı isimde görevleri farklı olsada farklı scriptlerde çağırılır. İki görevde uygulanır.!
        hitPoints -= damage;
        
        if(hitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if(isDead)
        {
            return;
        }
        isDead = true;
        GetComponent<Animator>().SetTrigger("die");
    }

}


