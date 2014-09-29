using UnityEngine;
using System.Collections;

public class MissileController : MonoBehaviour {

    public GameObject target;
    private BulletModel bulletModel;

    private float _speed = 8.0f;

    private int _count = 0;

	// Use this for initialization
	void Start () {
        bulletModel = transform.GetComponent<BulletModel>();
        bulletModel.hitPoints = 50;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        _count++;

        //Boid steering behaviour. 
        Vector3 targetDirection = target.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        if (_count < 80)
        {
            //turn slowly at first
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.1f);
        }
        else
        {
            //more agile missile behaviour
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.8f);
        }

        transform.position = transform.position + transform.forward * Time.deltaTime * _speed;
	}
}
