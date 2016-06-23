using System;
using UnityEngine;
using Assets.Scripts.Skills;
using Action = Assets.Scripts.Skills.Action;

public class SkillUseIndicatorManager : MonoBehaviour
{

    public GameObject Kick;
    public GameObject TiedTogether;

    public void SetActive(Action a, bool active = false)
    {
        if (a is KickAction)
        {
            Kick.SetActive(active);
        }
        if (a is TiedTogetherAction)
        {
            TiedTogether.SetActive(active);
        }
    }

    public void SetActive(Component a, bool active = false)
    {
        Action action = a as Action;
        if (action == null) throw new ArgumentException("Invalid component");
        SetActive(action, active);
    }

    public void Activate(Action a)
    {
        SetActive(a, true);
    }

    public void Deactivate(Action a)
    {
        SetActive(a);
    }
}
