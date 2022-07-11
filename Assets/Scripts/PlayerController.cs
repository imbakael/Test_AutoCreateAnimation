using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private Rigidbody2D rigidbody2D = default;
    [SerializeField] private float speed = default;

    private void Update() {
        Move();
    }

    private void Move() {
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0) {
            rigidbody2D.velocity = new Vector2(horizontal * speed * Time.deltaTime, rigidbody2D.velocity.y);
        }
    }
}
