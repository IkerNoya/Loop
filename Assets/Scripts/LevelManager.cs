using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    [SerializeField] SpriteRenderer level;
    [SerializeField] Sprite[] levelsSprites;

    int actualLevel = 1;

    private void Start() {
        GameplayManager.DoorClicked += ChangeLevel;
    }

    private void OnDisable() {
        GameplayManager.DoorClicked -= ChangeLevel;
    }

    void ChangeLevel() {
        actualLevel++;
        if (actualLevel > 3)
            actualLevel = 1;

        level.sprite = levelsSprites[actualLevel];
    }

}
