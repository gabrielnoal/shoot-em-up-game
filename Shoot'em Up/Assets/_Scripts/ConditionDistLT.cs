using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionDistLT : Condition
{
    Transform agent;
    Transform target;
    float maxDist;

    public ConditionDistLT(Transform _agent, Transform _target, float _maxDist) {

