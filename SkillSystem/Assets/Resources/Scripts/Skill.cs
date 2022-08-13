using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public enum StatsType
    {
        None = 0,
        Damage = 1,
        ManaCost = 2,
        CooldownReduce = 3,
        StatAmount = 4,
        Mobility = 5,
        Support = 6,

        OnActivate = 101
    }

    public SkillSetting skillSetting;
    public List<Talent> talentList = new List<Talent>();
    public StatsType statType = StatsType.None;     // if this is the same as statType in Talent, modifierAmount in Talent will affect some values in this Skill

    public event Func<bool> BeforeActivate;

    public float Mana { get; protected set; }
    public float Cooldown { get; protected set; }

    public virtual void Init()
    {
        Mana = skillSetting.mana;
        Cooldown = skillSetting.cooldown;
    }
    public virtual bool Activate() 
    {
        if (BeforeActivate != null && BeforeActivate())
            return false;

        return true;
    }
    public virtual bool AddTalent(Talent tal)
    {
        if (!talentList.Contains(tal))
        {
            talentList.Add(tal);
            tal.OnTalentAdded(this);
            switch (tal.statType)
            {
                case StatsType.ManaCost:
                    Mana -= tal.modifierAmount;
                    Mana = Mathf.Max(Mana, 0);
                    break;
                case StatsType.CooldownReduce:
                    Cooldown -= tal.modifierAmount;
                    Cooldown = Mathf.Max(Cooldown, 0);
                    break;
            }
            return true;
        }

        return false;
    }

#if UNITY_EDITOR
    public virtual void AddSkillSetting(SkillSetting setting)
    {
        skillSetting = setting;
    }

    public virtual void SetAmounts(params float[] amounts)
    {

    }
#endif
}
