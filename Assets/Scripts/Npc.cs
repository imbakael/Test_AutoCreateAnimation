using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NpcAnimationType {
    Idle, // ÐÝÃß
    Spot, // ÓöµÐ
    Pursuit, // ×·»÷(¿ìËÙÒÆ¶¯)
    Attack,
    Dead
}

public class Npc : MonoBehaviour {

    [SerializeField] private float viewRange = default;
    [SerializeField] private float attackRange = default;
    [SerializeField] private float idleSpeed = 10f;
    [SerializeField] private float pursuitSpeed = 10f;
    [SerializeField] private float floatSpeed = 1f;
    [SerializeField] private float floatRange = 1f;
    [SerializeField] private Transform pivot = default;

    public bool IsAttackEnd { get; set; }

    public int hp = 100;
    public float attackInterval = 5f;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private UnitAnimEvent unitAnimEvent;
    private float originalY;

    private void Awake() {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponentInChildren<Rigidbody2D>();
        unitAnimEvent = GetComponentInChildren<UnitAnimEvent>();
        unitAnimEvent.OnAttackEnd += OnAttackEnd;
        unitAnimEvent.OnDead += OnDead;
        originalY = pivot.localPosition.y;
    }


    private void OnAttackEnd() {
        IsAttackEnd = true;
    }

    private void OnDead() {
        Destroy(gameObject);
        GameObject mogubenti = GameController.Instance.CreateMogu(transform.position);
        mogubenti.GetComponent<SpriteRenderer>().flipX = spriteRenderer.flipX;
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

    public void IdleMoveTo(Vector2 target) => MoveTo(target, idleSpeed);

    public void PursuitMoveTo(Vector2 target) => MoveTo(target, pursuitSpeed);

    private void MoveTo(Vector2 target, float speed) {
        LookAt(target);
        float directionValue = (target.x - transform.position.x) > 0 ? 1 : -1;
        transform.position = new Vector2(transform.position.x + directionValue * speed * Time.deltaTime, 0);
        pivot.localPosition = new Vector2(pivot.localPosition.x, originalY + floatRange * Mathf.Sin(Time.time * floatSpeed));
    }

    public bool IsArrive(Vector2 target) {
        return Mathf.Abs(transform.position.x - target.x) <= 0.2f;
    }

    public void LookAt(Vector2 target) {
        float directionX = target.x - transform.position.x;
        spriteRenderer.flipX = directionX > 0;
    }

    public void IdleAnim() => SetAnimation(NpcAnimationType.Idle);

    public void SpotAnim() => SetAnimation(NpcAnimationType.Spot);

    public void PursuitAnim() => SetAnimation(NpcAnimationType.Pursuit);

    public void AttackAnim() => SetAnimation(NpcAnimationType.Attack);

    public void DeadAnim() {
        SetAnimation(NpcAnimationType.Dead);
    }

    private void SetAnimation(NpcAnimationType npcAnimationType) {
        animator.SetInteger("AnimationType", (int)npcAnimationType);
    }
}
