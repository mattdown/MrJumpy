using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    private EnemyModel _enemyModel;

    public GameObject bulletPrefab;
    public GameObject missilePrefab;

    public GameObject player;

    private int _count = 0;
    private int _fireFrequency;

	// Use this for initialization
	void Start () {
        _enemyModel = GetComponent<EnemyModel>();
        _enemyModel.isAlive = true;
        _enemyModel.health = 20;

        switch (_enemyModel.enemyType)
            {
                case EnemyModel.STRAIGHT_SHOOTER:
                    _fireFrequency = 40;
                    break;
                case EnemyModel.AUTO_AIM:
                    _fireFrequency = 60;
                    break;
                case EnemyModel.HOMING_MISSILE:
                    _fireFrequency = 80;
                    break;
            }
	}
	
	// Update is called once per frame
	void Update () {
        _count++;
        if (_count % _fireFrequency == 0)
        {
            switch (_enemyModel.enemyType)
            {
                case EnemyModel.STRAIGHT_SHOOTER:
                    FireStraightBullet();
                    break;
                case EnemyModel.AUTO_AIM:
                    FireAutoAimBullet();
                    break;
                case EnemyModel.HOMING_MISSILE:
                    FireMissile();
                    break;
            }
        }
	}

    private void FireStraightBullet(){
        GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation);
        Vector3 targetPosition = new Vector3(0, transform.position.y, 0);   //always aim for center of screen
        bullet.GetComponent<BulletController>().SetTarget(targetPosition);
        bullet.GetComponent<BulletModel>().isFriendly = false;
    }

    private void FireAutoAimBullet(){
        GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<BulletController>().SetTarget(player.transform.position);
        bullet.GetComponent<BulletModel>().isFriendly = false;
    }

    private void FireMissile(){
        GameObject missile = (GameObject)Instantiate(missilePrefab, transform.position, Quaternion.Euler(-90, 0, 0));
        missile.GetComponent<MissileController>().target = player;
    }

    public void OnTriggerEnter(Collider other)
    {
        BulletModel bulletModel = other.gameObject.GetComponent<BulletModel>();
        if (bulletModel != null && bulletModel.isFriendly)
        {
            Destroy(other.gameObject);
            _enemyModel.health -= bulletModel.hitPoints;
            CheckForDeath();
        }
    }

    private void CheckForDeath()
    {
        if (_enemyModel.health < 0)
        {
            GameController.DestroyEnemy(transform.gameObject);
        }
    }
}
