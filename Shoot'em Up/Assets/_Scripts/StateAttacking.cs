using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttacking : State
{
    SteerableBehaviour steerable;
    IShooter shooter;

    public float shootDelay = 1.0f;
    private float _lastShootTimestamp = 0.0f;

    public override void Awake()
    {
        base.Awake();

        Transition searching = new Transition();
        searching.condition = new ConditionDistGT(
            transform,
            GameObject.FindWithTag("Player").transform,
            2.0f
            );
        
        searching.target = GetComponent<StateSearch>();
        transitions.Add(searching);


        steerable = GetComponent<SteerableBehaviour>();

        shooter = steerable as IShooter;
        if( shooter == null) {
            throw new MissingComponentException("Esse GameObject não implementa IShooter");
        }
    }

    public void Update() {
        if (Time.time - _lastShootTimestamp < shootDelay) return;

       _lastShootTimestamp = Time.time;
       shooter.Shoot();
    }
}
