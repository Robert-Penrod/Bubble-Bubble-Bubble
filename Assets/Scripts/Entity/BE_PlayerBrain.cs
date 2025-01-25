using UnityEngine;

public class BE_PlayerBrain : BE_Brain
{
    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        BubbleEntity.Move(moveInput.normalized);
    }
}
