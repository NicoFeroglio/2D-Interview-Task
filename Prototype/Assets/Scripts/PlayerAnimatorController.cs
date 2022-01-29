using UnityEngine;

public class PlayerAnimatorController
{
    private readonly Animator _animator;
    private static readonly int Walking = Animator.StringToHash("Walking");

    public PlayerAnimatorController(Animator animator)
    {
        _animator = animator;
    }

    public void Walk(bool value)
    {
        _animator.SetBool(Walking, value);
    }
}