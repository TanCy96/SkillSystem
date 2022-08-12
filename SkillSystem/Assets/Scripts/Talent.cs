using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talent : MonoBehaviour
{
    public float modifierAmount;
    public Skill.StatsType statType = Skill.StatsType.None;     // identifier to which skill values to modify with modifierAmount
    public Skill AssignedSkill { get; protected set; }

    public virtual void OnTalentAdded(Skill skill) 
    {
        AssignedSkill = skill;
    }
}
