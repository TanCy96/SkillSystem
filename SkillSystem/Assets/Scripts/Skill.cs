using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public SkillSetting skillSetting;

    public virtual void Activate() { }
}
