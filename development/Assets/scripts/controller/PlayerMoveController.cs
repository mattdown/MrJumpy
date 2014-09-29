using UnityEngine;
using System.Collections;

public class PlayerMoveController : MonoBehaviour {

    public float gravity;
    public float thrust;

    public float max_speed;

    private float vertical_velocity = 0.0f;

    private PlayerModel _playerModel;

	// Use this for initialization
	void Start () {
        _playerModel = GetComponent<PlayerModel>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0) && _playerModel.isAlive)
        {
            vertical_velocity += thrust;
        }
        else
        {
            vertical_velocity += gravity;
        }

        vertical_velocity = Mathf.Clamp(vertical_velocity, -max_speed, max_speed);  //keeps speed between +- max_speed

        //for when player hits ground
        if (transform.position.y < 0)
            vertical_velocity = 0.8f * Mathf.Abs(vertical_velocity);

	}

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + vertical_velocity, transform.position.z);
    }
}
