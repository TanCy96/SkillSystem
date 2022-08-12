using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatSkill : Skill
{
    public float statAmount;
    public override bool Activate()
    {
        if (base.Activate())
        {
            return true;
        }

        return false;
    }

    public override bool AddTalent(Talent tal)
    {
        if (base.AddTalent(tal))
        {
            if (tal.statType == statType && statType != StatsType.None)
                statAmount += tal.modifierAmount;
            return true;
        }

        return false;
    }
}
