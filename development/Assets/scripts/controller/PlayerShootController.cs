using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShootController : MonoBehaviour {

    public GameObject bulletPrefab;

    public List<GameObject> enemies;

    private int _count;

    private PlayerModel _playerModel;

	// Use this for initialization
	void Start () {
        _playerModel = GetComponent<PlayerModel>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!_playerModel.isAlive)
        {
            return;
        }
        _count++;
        if (_count % 30 == 0)
        {
            ShootNearestEnemy();
        }
	}

    private void ShootNearestEnemy()
    {
        float min_distance = 100.0f;
        GameObject nearest_enemy = null;
        //Shoot towards the nearest enemy
        foreach (GameObject enemy in enemies)
        {
            if (Vector3.Distance(enemy.transform.position, transform.position) < min_distance)
            {
                min_distance = Vector3.Distance(enemy.transform.position, transform.position);
                nearest_enemy = enemy;
            }
        }

        if (nearest_enemy != null)
        {
            GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.GetComponent<BulletController>().SetTarget(nearest_enemy.transform.position);
            bullet.GetComponent<BulletModel>().isFriendly = true;
        }
    }
}
