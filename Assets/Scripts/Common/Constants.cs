using System;

static class Constants
{
    // Login Service
    public const Int32 LoginServiceID = 2000;
    public const Int32 HandleLoginCommandID = 1001;
    public const Int32 LoginSuccessCommandID = 1002;
    public const Int32 LoginFailedCommandID = 1003;

    // Game Service
    public const Int32 GameServiceID = 3000;
    public const Int32 ClientSyncCommandID = 1001;
    public const Int32 TryModifyEnemyCommandID = 1002;
    public const Int32 GameDefeatCommandID = 1003;
    public const Int32 GameVictoryCommandID = 1004;
    public const Int32 GetOnTankCommandID = 1005;
    public const Int32 StoreResourceCommandID = 1006;

    // Player Action Code
    public const Int32 PlayerCreate = 3001;
    public const Int32 PlayerShoot = 3002;
    public const Int32 PlayerThrow = 3003;
    public const Int32 PlayerReload = 3004;
    public const Int32 PlayerRun = 3006;
    public const Int32 PlayerWalk = 3007;
    public const Int32 PlayerIdle = 3008;
    public const Int32 PlayerTakgeDamage = 3009;
    public const Int32 PlayerDeath = 3010;
    public const Int32 PlayerUseGun = 3011;
    public const Int32 PlayerUserGrenade = 3012;

    // Enemy Type
    public const Int32 EnemyType01 = 1;
    public const Int32 EnemyType02 = 2;

    // Enemy Action Code
    public const Int32 EnemyCreate = 4001;
    public const Int32 EnemyShoot = 4002;
    public const Int32 EnemyExplosion = 4003;
    public const Int32 EnemyTakeDamage = 4004;
    public const Int32 EnemyMove = 4005;
    public const Int32 EnemyIdle = 4006;
    public const Int32 EnemyDeath = 4007;

    // Tank Action Code
    public const Int32 TankShoot = 5002;
    public const Int32 TankNoAction = 5008;

    // Ammunition
    public const Int32 bulletsNumPerShuttle = 24;
    public const Int32 grenadesNumPerBox = 3;
    public const Int32 tankBulletsNumPerShuttle = 3;
    public const Int32 bulletPricePerShuttle = 6;
    public const Int32 grenadePricePerBox = 2;
    public const Int32 tankBulletpricePerShuttle = 2;
}