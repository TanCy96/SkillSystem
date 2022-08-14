# SkillSystem
## Introduction
This is just a proof of concept on a Skill/Ability system in Unity that is expandable in a game project, it can be further developed to fit into a system of a MOBA or even Roguelike games. Depending on what is necessary in a game, more scripts are needed to be prepared earlier before a non-tech person (Designers/Artists) can use it to create different sets of abilities.

## Class Diagram
![ClassTable](https://user-images.githubusercontent.com/32587088/184530690-9a1af592-fc2f-476a-a6d7-7fbfbb9094ac.png)

The overall system uses GameObject instead of a ScriptableObject, reason being GameObject is easier to create multiple different varities on runtime/game session. A scriptable object is mostly used as a prepared asset, and it is not a good habit to change its value during runtime. The details of a skill should be preset in SkillSetting and assign to a Skill that we want to create.

### Skill
To create a skill, for example a Fireball skill, it could use a DamageSkill that inherits from parent Skill class. Changing the skill settings, damage amount, possibly VFX or sprites, then it is a usable system provided that the logic is kept simple (a fireball that does damage to an enemy). I separated into 4 basic skills which are Damage, Mobility, Stat and Support skills, depending on the complexity of a skill needed, the system might need a sibling class to these 4 skills or inherit from them and change the logic.

### Talent
My idea of talent behaves similar to a Skill object that is being created. It is also a GameObject, consisting of modifierAmount, a StatsType and the Skill that is assigned to it during "OnTalentAdded()" function, which is called in Skill class during "AddTalent()". The usage of StatsType is to let the system understands that this Talent will affect this Skill, etc a Talent with "Damage" StatsType will only affect a Skill with "Damage" StatsType. Some exceptions like ManaCost and CooldownReduce would work regardless of whether the Skill has the StatsType, due to it being in the main Skill class. Depending on the system, the StatsType storage in Skill could be changed into an array so that multiple different Talents can affect one single Skill.

TalentAbility inherits from Talent class, an example use case that I have created is a function "ActivateAbility()" which will be called in Skill class "BeforeActivate()", the return value is a boolean that will stop Skill class from calling "Activate()" and replace the skill completely. This can be further developed to have a Talent that could overwrite or add different abilities to an existing Skill <b>Before/During/After</b> activate. For example, a Fireball skill that split its projectile into two before activating.

## Editor Window
![Skill-SS](https://user-images.githubusercontent.com/32587088/184531461-08f70224-714e-4c2f-941b-1cf8ae5f3ce3.png)
![Talent-SS](https://user-images.githubusercontent.com/32587088/184531462-b0731a87-264c-426f-8e42-162527edc3a7.png)

To help with creating Skills and Talents, there is also an Editor Window that does the trick. <b>To access this window in Unity, it is located on the top bar-->Project-->SkillCreator</b>. As shown in the screenshots, the system supports creating a Skill or Talent by filling in the details and the necessary Prefabs will be created to be used in a game. Do note that the system currently does not support assigning Talent to a Skill by this Editor Window, this is because I have the idea that the Talent is only assigned to a Skill during a game session.

![Prefabs-SS](https://user-images.githubusercontent.com/32587088/184531575-64aca993-9a55-4409-b694-7469bcc2390a.png)

## Conclusion
The system is very basic and unfinished in a lot of ways. The idea is to be as generic and go as wide as possible to support a skill system. Usually in a game, especially a MOBA, there are limiting factors to the skills. For example, a character can only have a maximum of 6 skills, a skill can only have 4 talents. To actually use this system, it should be developed together with a much more solid design.
