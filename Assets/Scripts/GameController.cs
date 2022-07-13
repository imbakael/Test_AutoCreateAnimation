using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private static GameController instance;
    public static GameController Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<GameController>();
            }
            return instance;
        }
    }

    public Player player;
    public Npc npc;
    public GameObject mogubenti;

    public Transform[] points;

    public GameObject CreateMogu(Vector2 position) {
        GameObject go = Instantiate(mogubenti);
        go.transform.position = position;
        return go;
    }

}
