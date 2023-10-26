using UnityEngine;
using UnityEngine.Events;
public static class EventSystem
{
    public static UnityEvent OnPlayerDeath = new UnityEvent();
    public static UnityEvent OnPlayerRevive = new UnityEvent();
    public static UnityEvent OnDataPlayerChanged = new UnityEvent();

    public static UnityEvent OnThrowHook = new UnityEvent();
    public static UnityEvent OnPullBackHook = new UnityEvent();
    public static UnityEvent OnHookCatch = new UnityEvent();
    public static UnityEvent OnHookThrowObject = new UnityEvent();
    public static UnityEvent OnHookStan = new UnityEvent();

    public static UnityEvent OnPauseEnable = new UnityEvent();
    public static UnityEvent OnPauseDisable = new UnityEvent();

    public static UnityEvent OnLevelFinished = new UnityEvent();

    #region PLAYER
    public static void SendPlayerDeath()
    {
        OnPlayerDeath?.Invoke();
    }
    public static void SendPlayerRevive()
    {
        OnPlayerRevive?.Invoke();
    }
    public static void SendDataPlayerChanged()
    {
        OnDataPlayerChanged?.Invoke();
    }
    #endregion

    #region HOOK
    public static void SendThrowHook()
    {
        OnThrowHook?.Invoke();
    }

    public static void SendPullBackHook()
    {
        OnPullBackHook?.Invoke();
    }

    public static void SendHookCatch()
    {
        OnHookCatch?.Invoke();
    }

    public static void SendHookThrowObject()
    {
        OnHookThrowObject?.Invoke();
    }
    public static void SendHookStan()
    {
        OnHookStan?.Invoke();
    }
    #endregion

    #region PAUSE
    public static void SendPauseEnable()
    {
        OnPauseEnable?.Invoke();
    }
    public static void SendPauseDisable()
    {
        OnPauseDisable?.Invoke();
    }
    #endregion

    public static void SendLevelFinished()
    {
        OnLevelFinished?.Invoke();
    }
}