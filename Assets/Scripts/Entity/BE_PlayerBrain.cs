using UnityEngine;

public class BE_PlayerBrain : BE_Brain
{
    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        BubbleEntity.Move(moveInput.normalized);

        float turnInput = 0f;
        if(Input.GetKey(KeyCode.E))
        {
            turnInput++;
        }
        if(Input.GetKey(KeyCode.Q))
        {
            turnInput--;
        }
        BubbleEntity.Turn(turnInput);
    }
}
