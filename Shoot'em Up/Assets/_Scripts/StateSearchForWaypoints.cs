using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSearchForWaypoints : State
{
    public Transform[] waypoints;
    SteerableBehaviour steerable;

    float angle = 0;

    public void Start()
    {
        waypoints[0].position = transform.position;
        waypoints[1].position = GameObject.FindWithTag("Player").transform.position;
    }
    public override void Awake()
    {
        base.Awake();

        Transition attacking = new Transition();
        attacking.condition = new ConditionDistLT(
            transform,
            GameObject.FindWithTag("Player").transform,
            2.0f
        );

        attacking.target = GetComponent<StateAttacking>();
        transitions.Add(attacking);

        steerable = GetComponent<SteerableBehaviour>();
    }

    public void Update()
    {
        if (Vector3.Distance(transform.position, waypoints[1].position) > .1f)
        {
            Vector3 direction = waypoints[1].position - transform.position;
            direction.Normalize();
            steerable.Thrust(direction.x, direction.y);
        }
        else
        {
            waypoints[1].position = GameObject.FindWithTag("Player").transform.position;
        }
    }
}
