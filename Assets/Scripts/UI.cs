using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    [SerializeField] private Button deadBtn = default;

    private void Awake() {
        deadBtn.onClick.AddListener(Dead);
    }

    private void Dead() {
        if (GameController.Instance.npc != null) {
            GameController.Instance.npc.hp = 0;
        }
        
    }
}
