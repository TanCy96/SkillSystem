using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSkill : Skill
{
    public float damageAmount;
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
                damageAmount += tal.modifierAmount;
            return true;
        }

        return false;
    }

#if UNITY_EDITOR
    public override void SetAmounts(params float[] amounts)
    {
        base.SetAmounts(amounts);
        if (amounts.Length > 0)
            damageAmount = amounts[0];
    }
#endif
}
