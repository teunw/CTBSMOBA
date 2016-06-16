using UnityEngine;
using System.Collections;

public class Skill {
    private Sprite indicator;
    private float range;
    private float width;
    private string name;
    private bool skillshot;

    private float slowRate;
    private float pushBackRate;

    public Skill(string name, Sprite indicator, float range, float width, float slowRate, float pushBackRate)
    {
        this.name = name;
        this.indicator = indicator;
        this.range = range;
        this.width = width;
        this.slowRate = slowRate;
        this.pushBackRate = pushBackRate;

        this.skillshot = true;
    }

    public Skill(string name, Sprite indicator, float range, float slowRate, float pushBackRate)
    {
        this.name = name;
        this.indicator = indicator;
        this.range = range;
        this.slowRate = slowRate;
        this.pushBackRate = pushBackRate;

        this.skillshot = false;
    }

    public GameObject CreateIndicator()
    {
        GameObject go = new GameObject();

        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = this.indicator;
        if(skillshot)
        {
            sr.transform.localScale = new Vector3(this.range, this.width, 1);
            sr.transform.localPosition.Set(this.range / 2, this.width / 2, 0.05f);
        }
        else
        {
            sr.transform.localScale = new Vector3(this.range, this.range, 1);
            sr.transform.localPosition.Set(this.range / 2, this.range / 2, 0.05f);
        }

        return go;
    }

    public bool IsSkillshot()
    {
        return this.skillshot;
    }

    public float GetRange()
    {
        return this.range;
    }

    public float GetWidth()
    {
        return this.width;
    }
}
