using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator anim;
    PlayerController controller;

    void Awake()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        anim.SetBool("IsGrounded", controller.IsGrounded);
        anim.SetBool("IsDashing", controller.IsDashing);
    }
}
