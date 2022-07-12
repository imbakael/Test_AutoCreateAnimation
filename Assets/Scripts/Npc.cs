using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour {

    [SerializeField] private float viewRange = default;
    [SerializeField] private float attackRange = default;
    [SerializeField] private float speed = 10f;

    public int hp = 100;
    public float attakcInterval = 2f;

    private Animator animator;

    private void Awake() {
        animator = GetComponentInChildren<Animator>();
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, viewRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public bool FindTarget(Transform target) {
        return Vector3.Distance(transform.position, target.position) <= viewRange;
    }

    public bool IsInAttackRange(Transform target) {
        return Vector3.Distance(transform.position, target.position) <= attackRange;
    }

    public void MoveTo(Vector2 target) {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

}
