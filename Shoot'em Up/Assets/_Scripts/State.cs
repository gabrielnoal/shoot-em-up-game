using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public List<Transition> transitions;
    public virtual void Awake()
    {
        transitions = new List<Transition>();
    }

    public void LateUpdate() {
        foreach (Transition transition in transitions)
        {
            if (transition.condition.Test())
            {
                transition.target.enabled = true;
                this.enabled = false;
                return;
            }
        }
    }
}
