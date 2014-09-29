using UnityEngine;
using System.Collections;

public class CameraMoveScript : MonoBehaviour {

    public GameObject target;

    public float frac;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //"elastic" connection between player and camera
        float y_pos = Mathf.Lerp(transform.position.y, target.transform.position.y + 3, frac);
        transform.position = new Vector3(transform.position.x, y_pos, transform.position.z);
	}
}
