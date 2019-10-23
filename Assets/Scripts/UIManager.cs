using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField]
    private Text _coinText, _livesText;

    public void UpdateCoinDisplay(int coins) {
        _coinText.text = "Coins: " + coins;
    }

    public void UpdateLivesDisplay (int lives) {
        _livesText.text = "Lives: " + lives;
    }
}
