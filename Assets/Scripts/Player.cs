using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    private CharacterController _controller;
    private Vector3 moveDir = Vector3.zero;
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _gravity = 0.25f;
    [SerializeField]
    private float _jumpSpeed = 18.0f;
    private float _yVelocity;
    private bool _canDoubleJump = false;
    [SerializeField]
    private int _coins = 0;
    private UIManager _uiManager;
    [SerializeField]
    private int _lives = 3;

    // Start is called before the first frame update
    void Start() {
        _controller = GetComponent<CharacterController>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager == null)
            Debug.LogError("UI Manager is null");

        _uiManager.UpdateLivesDisplay(_lives);
        _uiManager.UpdateCoinDisplay(_coins);
    }

    // Update is called once per frame
    void Update() {
        CalculateMovement();
    }

    private void FixedUpdate() {
        if (this.transform.position.y <= -7f) {
            RemoveLife();
            if (_lives < 1)
                SceneManager.LoadScene(0);
            else {
                this.transform.position = new Vector3(-4.65f, 1.53f);
            }
            
        }
    }

    void CalculateMovement() {
        moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        Vector3 velocity = moveDir * _speed;

        if (_controller.isGrounded) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                _yVelocity = _jumpSpeed;
                _canDoubleJump = true;
            }
                
        } else {
            if (Input.GetKeyDown(KeyCode.Space) && _canDoubleJump) {
                _yVelocity += _jumpSpeed;
                _canDoubleJump = false;
            }
                
            _yVelocity -= _gravity;
        }

        velocity.y = _yVelocity;
        _controller.Move(velocity * Time.deltaTime);
    }

    public void AddCoin() {
        _coins++;
        _uiManager.UpdateCoinDisplay(_coins);
    }

    private void RemoveLife() {
        _lives--;
        _uiManager.UpdateLivesDisplay(_lives);
    }
}
