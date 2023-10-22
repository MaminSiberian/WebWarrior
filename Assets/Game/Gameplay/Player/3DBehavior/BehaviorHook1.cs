using UnityEngine;





public class HookIdleBehavior : IHookBehavior
{

    public void Enter()
    {
        Debug.Log("Enter Idle state");
    }

    public void Exit()
    {
        Debug.Log("Exit Idle state");
    }

    public void UpdateBehavior()
    {
        Debug.Log("Update Idle state");
    }
}
