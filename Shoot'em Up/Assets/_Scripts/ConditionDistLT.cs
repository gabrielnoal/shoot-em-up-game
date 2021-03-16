using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionDistLT : Condition
{
    Transform agent;
    Transform target;
    float maxDist;

    public ConditionDistLT(Transform _agent, Transform _target, float _maxDist) {
        agent = _agent;
        target = _target;
        maxDist = _maxDist;
    }

    public override bool Test() {
        return Vector2.Distance(agent.position, target.position) <= maxDist;
    }

}
