using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, IHear
{
    [SerializeField] Transform  target;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 5f;
    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    EnemyHealth health;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health.IsDead())
        {
            enabled = false;
            navMeshAgent.enabled = false;
            isProvoked = false;
        }
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if(isProvoked)
        {
            EngageTarget();
        }
        else if(distanceToTarget <= chaseRange)
        {
            
            isProvoked = true;
        }
        
    }

    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    private void EngageTarget()
    {
        FaceTarget();
        if(distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        if(distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("attack",false);
        GetComponent<Animator>().SetTrigger("move");
        navMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("attack",true);
    }

    private void FaceTarget()
    {
        //transform.rotation == where the target is, we need to rotate at a certain speed
        Vector3 direction = (target.position-transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation= Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);

    }

    void OnDrawGizmosSelected()
    {
        // Display the Cexplosion radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,chaseRange);
    }

    public void RespondToSound(Sound sound)
    {
        if(sound.soundType == Sound.SoundType.Interesting)
        {
            Vector3 dir = (sound.pos - transform.position).normalized;
            MoveTo(transform.position - (dir * 10f));
        }
        else if(sound.soundType == Sound.SoundType.Danger)
        {
            Vector3 dir = (sound.pos - transform.position).normalized;
            MoveTo(transform.position - (dir * 10f));
        }
        
    }

    private void MoveTo(Vector3 pos)
    {
        navMeshAgent.SetDestination(pos);
        navMeshAgent.isStopped = false;
        
    }
}
