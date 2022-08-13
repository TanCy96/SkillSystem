using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentAbility : Talent
{
    public override void OnTalentAdded(Skill skill)
    {
        if (AssignedSkill != null)
            AssignedSkill.BeforeActivate -= ActivateAbility;
        base.OnTalentAdded(skill);
        if (AssignedSkill != null)
            AssignedSkill.BeforeActivate += ActivateAbility;
    }
    protected virtual bool ActivateAbility()
    {
        return true;
    }
}
