using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/SkillSetting")]
public class SkillSetting : ScriptableObject
{
    public string skillName;
    public string desc;
    public float mana;
    public float cooldown;
}
