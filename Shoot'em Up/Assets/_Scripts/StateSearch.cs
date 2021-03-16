using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSearch : State
{
    SteerableBehaviour steerable;

    float angle = 0;
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
        angle += 0.1f;
        Mathf.Clamp(angle, 0.0f, 2.0f * Mathf.PI);
        float x = Mathf.Sin(angle);
        float y = Mathf.Cos(angle);

        steerable.Thrust(x, y);  
    }
}
