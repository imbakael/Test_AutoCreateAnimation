using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class MoguFall : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.GetType() == typeof(TilemapCollider2D)) {
            StartCoroutine(DelayDead());
        }
    }

    private IEnumerator DelayDead() {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
