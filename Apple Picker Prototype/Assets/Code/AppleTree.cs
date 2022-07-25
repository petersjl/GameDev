using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour {
    [Header("Set in Inspector")]
    // Prefab for apples
    public GameObject applePrefab;

    // Spped of appletree
    public float speed = 1f;

    // Distance where tree turns around
    public float leftAndRightEdge = 10f;

    // Chance for tree to change directions
    public float chanceToChangeDirections = 0.1f;

	// Cooldown for direction change
	public float directionCooldown = 10;

    // Apple spawn rate
    public float secondsBetweenAppleDrops = 1f;

	// Private timer for direction cooldown
	private float timer;

    // Use this for initialization
	void Start () {
		timer = 0;
		Invoke("DropApple", 2f);
	}

	void DropApple() {
		GameObject apple = Instantiate<GameObject>(applePrefab);
		apple.transform.position = transform.position;
		Invoke("DropApple", secondsBetweenAppleDrops);
	}

	// Update is called once per frame
	void Update() {

        // Basic Movement
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        // Change Direction
        if (pos.x < -leftAndRightEdge) {
            speed = Mathf.Abs(speed); //Move right
        } else if (pos.x > leftAndRightEdge) {
            speed = -Mathf.Abs(speed); //Move left
        } else if (Random.value < chanceToChangeDirections && timer <= 0) {
			speed *= -1; // Change direction
			timer = directionCooldown; // Set cooldown timer
        }
		if (timer > 0) timer -= Time.deltaTime; // Increment timer
	}
}
