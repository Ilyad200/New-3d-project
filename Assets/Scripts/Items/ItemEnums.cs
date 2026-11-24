using UnityEngine;

public enum ItemType { Equipment, Consumable, Material, Currency }
public enum EquipmentSlot { LeftArm, RightArm, Chestplate, Helmet, Boots, Legins, Gauntlet, Accessory, None }
public enum StatType { Damage, Resistance, Health, Speed, AttackRange, AttackSpeed, Durability, DetectionRange, Regeneration, Penetration }
public enum ModOp { Flat, Percent }
public enum DamageType { Physical, Fire, Ice, Lightning, Poison, Magic, True }
public enum DamageTags { CanCrit, CanBeBlocked, Periodic, AOE, Melee, Ranged, CanTriggerOnHit, CanApplyStatus, LifestealEligible,
                         Resource_Pickaxe, Resource_Axe, Resource_Shovel }
