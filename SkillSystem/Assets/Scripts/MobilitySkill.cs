using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilitySkill : Skill
{
    public float mobilityAmount;
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
                mobilityAmount += tal.modifierAmount;
            return true;
        }

        return false;
    }
}
