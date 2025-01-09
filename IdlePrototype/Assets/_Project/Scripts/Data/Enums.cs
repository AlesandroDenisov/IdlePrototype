public enum BuildingType
{
    Garage,
    Market,
    Workshop
}

public enum ResourceType
{
    Tokens,     // (жетоны)
    Salvage,    // (обломки)
    Glowstone  // артефакт (светящийся камень)
}

public enum CarParameter
{
    Speed,
    Hp,
    Trunk,
    SpeedAttack,
    DamageAttack
}

public enum EnemyTypeId
{
    EnemyBuggy = 0,
    Enemy      = 1
}

public enum WeaponTypeId
{
    Gun  = 0,
    Plasma = 1,
    Rocket  = 2,
    None
}

public enum BulletTypeId
{
    SmallGoldBullet = 0,
    MediumGoldBullet = 1,
    LargeGoldBullet = 2,
    None
}

public enum EnemyStateType
{
    Appear,
    Chase,
    Attack,
    Die,
    None
}

public enum WindowId
{
    None = 0,
    GarageWindow   = 1,
    MarketWindow   = 2,
    WorkshopWindow = 3
}