using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

    private Vector3 directionVector;

    private float speed = 0.2f;

    private float lifespan = 4.0f;  //lifespan in seconds
    private float _startTime;

	// Use this for initialization
	void Start () {
        GetComponent<BulletModel>().hitPoints = 40;
        _startTime = Time.time;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //Move bullet in straight line
        transform.position = transform.position + directionVector * speed;

        if (Time.time > _startTime + lifespan)
        {
            Destroy(transform.gameObject);
        }
	}

    public void SetTarget(Vector3 target)
    {
        directionVector = Vector3.Normalize(target - transform.position);
    }
}
