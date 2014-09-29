using UnityEngine;
using System.Collections;

public class PlayerCollisionController : MonoBehaviour {

    public GameObject healthBar;
    private PlayerModel playerModel;
    private int totalHealth = 500;

    private Color originalColor;
    
	// Use this for initialization
	void Start () {
        playerModel = transform.GetComponent<PlayerModel>();
        playerModel.health = totalHealth;

        originalColor = renderer.material.color;
	}
	
	// Update is called once per frame
	void Update () {
        //after player hit move towards original color
        renderer.material.color = Color.Lerp(renderer.material.color, originalColor, 0.1f);
	}

    void OnTriggerEnter(Collider other)
    {
        //check for collisions with bullets/missiles
        BulletModel bulletModel = other.gameObject.GetComponent<BulletModel>();

        if (bulletModel != null && !bulletModel.isFriendly)
        {
            playerModel.health -= bulletModel.hitPoints;
            SetHealthBarScale();
            Destroy(other.gameObject);
            Debug.Log("Player Health: " + playerModel.health);
            renderer.material.color = Color.red;
        }
    }

    private void SetHealthBarScale()
    {
        float scaleX = Mathf.Max(2.0f*(float)playerModel.health / (float)totalHealth, 0);
        healthBar.transform.localScale = new Vector3(scaleX, 1, 1);
    }
}
