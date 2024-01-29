using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PoolObjectType
{
    PlayerBullet = 0,
    HitEffect,        
    PowerUp,          
    EnemyWave,        
    EnemyAsteroid,    
    EnemyAsteroidMini,
    EnemyBonus,       
    EnemyCurve,       
    EnemyBoss,        
    EnemyBossBullet,  
    EnemyBossMissile, 
}

public class Factory : Singleton<Factory>
{
    BulletPool bullet;
    HitEffectPool hit;
    PowerUpPool powerUp;
    WavePool enemy;
    AsteroidPool asteroid;
    AsteroidMiniPool asteroidMini;
    BonusPool bonus;
    CurvePool curve;
    BossPool boss;
    BossBulletPool bossBullet;
    BossMissilePool bossMissile;


    protected override void OnInitialize()
    {
        base.OnInitialize();

        bullet = GetComponentInChildren<BulletPool>();  
        if (bullet != null)
            bullet.Initialize();

        hit = GetComponentInChildren<HitEffectPool>();
        if ( hit != null )
            hit.Initialize();

        powerUp = GetComponentInChildren<PowerUpPool>();
        if (powerUp != null)
            powerUp.Initialize();

        enemy = GetComponentInChildren<WavePool>();
        if(enemy != null)
            enemy.Initialize();

        asteroid = GetComponentInChildren<AsteroidPool>();
        if (asteroid != null) asteroid.Initialize();
        
        asteroidMini = GetComponentInChildren<AsteroidMiniPool>();
        if (asteroidMini != null) asteroidMini.Initialize();

        bonus = GetComponentInChildren<BonusPool>();
        if (bonus != null) bonus.Initialize();

        curve = GetComponentInChildren<CurvePool>();
        if (curve != null) curve.Initialize();

        boss = GetComponentInChildren<BossPool>();
        if (boss != null) boss.Initialize();

        bossBullet = GetComponentInChildren<BossBulletPool>();
        if (bossBullet != null) bossBullet.Initialize();

        bossMissile= GetComponentInChildren<BossMissilePool>();
        if (bossMissile != null) bossMissile.Initialize();
    }


    public GameObject GetObject(PoolObjectType type, Vector3? position = null, Vector3? euler = null)
    {
        GameObject result = null;
        switch (type)
        {
            case PoolObjectType.PlayerBullet:
                result = bullet.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.HitEffect:
                result = hit.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.PowerUp:
                result = powerUp.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.EnemyWave:
                result = enemy.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.EnemyAsteroid:
                result = asteroid.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.EnemyAsteroidMini:
                result = asteroidMini.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.EnemyBonus:
                result = bonus.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.EnemyCurve:
                result = curve.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.EnemyBoss:
                result = boss.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.EnemyBossBullet:
                result = bossBullet.GetObject(position, euler).gameObject;
                break;
            case PoolObjectType.EnemyBossMissile:
                result = bossMissile.GetObject(position, euler).gameObject;
                break;
        }

        return result;
    }

    public Bullet GetBullet()
    {
        return bullet.GetObject();
    }

    public Bullet GetBullet(Vector3 position, float angle = 0.0f)
    {
        return bullet.GetObject(position, angle * Vector3.forward);
    }

    public Explosion GetHitEffect()
    {
        return hit.GetObject();
    }

    public Explosion GetHitEffect(Vector3 position, float angle = 0.0f)
    {
        return hit.GetObject(position, angle * Vector3.forward);
    }

    public PowerUp GetPowerUp()
    {
        return powerUp.GetObject();
    }

    public PowerUp GetPowerUp(Vector3 position, float angle = 0.0f)
    {
        return powerUp.GetObject(position, angle * Vector3.forward);
    }

    public Wave GetEnemyWave()
    {
        return enemy.GetObject();
    }

    public Wave GetEnemyWave(Vector3 position, float angle = 0.0f)
    {
        return enemy.GetObject(position, angle * Vector3.forward);
    }

    public Asteroid GetAsteroid()
    {
        return asteroid.GetObject();
    }

    public Asteroid GetAsteroid(Vector3 position, float angle = 0.0f)
    {
        return asteroid.GetObject(position, angle * Vector3.forward);
    }

    public AsteroidMini GetAsteroidMini()
    {
        return asteroidMini.GetObject();
    }

    public AsteroidMini GetAsteroidMini(Vector3 position, float angle = 0.0f)
    {
        return asteroidMini.GetObject(position, angle * Vector3.forward);
    }

    public Bonus GetBonus()
    {
        return bonus.GetObject();
    }

    public Bonus GetBonus(Vector3 position, float angle = 0.0f)
    {
        return bonus.GetObject(position, angle * Vector3.forward);
    }

    public Curve GetCurve()
    {
        return curve.GetObject();
    }

    public Curve GetCurve(Vector3 position, float angle = 0.0f)
    {
        return curve.GetObject(position, angle * Vector3.forward);
    }

    public Boss GetBoss()
    {
        return boss.GetObject();
    }

    public Boss GetBoss(Vector3 position, float angle = 0.0f)
    {
        return boss.GetObject(position, angle * Vector3.forward);
    }

    public BossBullet GetBossBullet()
    {
        return bossBullet.GetObject();
    }

    public BossBullet GetBossBullet(Vector3 position, float angle = 0.0f)
    {
        return bossBullet.GetObject(position, angle * Vector3.forward);
    }

    public BossMissile GetBossMissile()
    {
        return bossMissile.GetObject();
    }

    public BossMissile GetBossMissile(Vector3 position, float angle = 0.0f)
    {
        return bossMissile.GetObject(position, angle * Vector3.forward);
    }

}
