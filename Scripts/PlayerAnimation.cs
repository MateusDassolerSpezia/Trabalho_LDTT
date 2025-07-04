using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _playerAnimator;

    void Start()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        PlayerAnimController();
    }

    private void PlayerAnimController()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _playerAnimator.SetBool("Turn_Left", true);
            _playerAnimator.SetBool("Turn_Right", false);
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _playerAnimator.SetBool("Turn_Left", false);
            _playerAnimator.SetBool("Turn_Right", false);
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _playerAnimator.SetBool("Turn_Right", true);
            _playerAnimator.SetBool("Turn_Left", false);
        }
        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            _playerAnimator.SetBool("Turn_Right", false);
            _playerAnimator.SetBool("Turn_Left", false);
        }
    }
}
