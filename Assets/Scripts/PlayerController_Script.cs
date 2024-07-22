using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController_Script : MonoBehaviour
{

    public GameObject gameManager;
    GameManager gameManagerScript;

    private CharacterController _controller;

    [SerializeField]
    private float _playerSpeed = 5f;

    [SerializeField]
    private float _rotationSpeed = 10f;

    [SerializeField]
    private Camera _followCamera;

    private Vector3 _playerVelocity;

    [SerializeField]
    private bool _groundedPlayer;

    [SerializeField]
    private float _jumpHeight = 1.0f;
    [SerializeField]
    private float _gravityValue = -9.81f;

     private float timer = 0.5f;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        gameManager = GameObject.Find("Game Manager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    private void Update()
    {
        Movement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable_Tag"))
        {
            Destroy(other.gameObject);
            gameManagerScript.currentScore++;

            //GameObject collectionSound;
            //collectionSound = Resources.Load ("Prefabs/afterCollection_") as GameObject;
            //Instantiate(collectionSound, other.transform.position, Quaternion.identity);
        }

        if(other.gameObject.CompareTag("Respawn")){
        SceneManager.LoadScene("_mainGame");
        }
    }

    void Movement()
    {
        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementInput = Quaternion.Euler(0, _followCamera.transform.eulerAngles.y, 0) * new Vector3(horizontalInput, 0, verticalInput);
        Vector3 movementDirection = movementInput.normalized;

        _controller.Move(movementDirection * _playerSpeed * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, _rotationSpeed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
        }

        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }
}