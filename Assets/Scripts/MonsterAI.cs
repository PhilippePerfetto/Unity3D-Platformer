using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    [Range(0.5f, 50f)]
    public float detectDistance = 3.0f;
    public float walkSpeed = 2.0f;
    public float runSpeed = 3.0f;
    public Transform[] points;
    NavMeshAgent agent;
    int destinationIndex = 0;
    Transform player;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = points[destinationIndex].position;

        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        Walk();
        SearchPlayer();
        SetMobSize();
    }

    public void SetMobSize()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectDistance + 2.0f)
        {
            iTween.ScaleTo(gameObject, Vector3.one, 0.5f);
        }
    }

    public void Walk()
    {
        float distance = agent.remainingDistance;

        if (distance <= 0.05)
        {
            destinationIndex++;
            if (destinationIndex >= points.Length) destinationIndex = 0;
            agent.destination = points[destinationIndex].position;
        }
    }

    public void SearchPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectDistance)
        {
            // joueur détecté
            agent.destination = player.position;
            agent.speed = runSpeed;
        }
        else
        {
            agent.speed = walkSpeed;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectDistance);
    }
}
