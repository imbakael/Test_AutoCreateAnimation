using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float speed = default;

    public int hp = 100;
    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        Move();
    }

    private void Move() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        transform.position = new Vector3(transform.position.x + horizontal * speed * Time.deltaTime, transform.position.y, 0);
    }
}
