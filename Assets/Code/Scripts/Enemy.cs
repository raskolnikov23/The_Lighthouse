using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public bool stunned;
    public NavMeshAgent agent;
    public Rigidbody rb;
    //public Rigidbody rb;
    public Vector3 destination;

    // patrolling state
    public Transform player;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").transform;

        rb.isKinematic = true;
        agent.enabled = true;
    }

    private void Update()
    {
        if (destination != null && !stunned)
        {
            agent.SetDestination(destination);
        }
        else { } // roam
        
        //GetComponent<Rigidbody>().position = agent.transform.position;
    }

    public void ReceiveHit(Vector3 hitDir, float hitStr)
    {
        stunned = true;
        agent.enabled = false;
        rb.isKinematic = false;
        rb.AddForce(hitDir * hitStr, ForceMode.VelocityChange);
    }

}
