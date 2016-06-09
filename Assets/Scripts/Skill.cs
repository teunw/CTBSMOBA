using UnityEngine;
using System.Collections;

public class Skill
{
    public Sprite indicator;
    public float range;
    public float width;
    public float slowRate;
    public float pushBackRate;

    private bool skillshot;
    private bool casting;

    public Skill(Sprite indicator, float range, float width, float slowRate, float pushBackRate)
    {
        this.indicator = indicator;
        this.range = range;
        this.width = width;
        this.slowRate = slowRate;
        this.pushBackRate = pushBackRate;

        this.skillshot = true;
    }

    public Skill(Sprite indicator, float range, float slowRate, float pushBackRate)
    {
        this.indicator = indicator;
        this.range = range;
        this.slowRate = slowRate;
        this.pushBackRate = pushBackRate;

        this.skillshot = false;
    }

    public bool IsSkillshot()
    {
        return this.skillshot;
    }
}
