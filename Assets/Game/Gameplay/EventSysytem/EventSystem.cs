using UnityEngine;
using UnityEngine.Events;
public static class EventSystem
{
    //public static UnityEvent<int> OnEnemyKilled = new UnityEvent<int>();
    //public static UnityEvent<int> OnGetMoney = new UnityEvent<int>();
    //public static UnityEvent<int> OnGiveMoney = new UnityEvent<int>();
    //public static UnityEvent OnDestroyBase = new UnityEvent();
    //public static UnityEvent OnChencheAmountMony = new UnityEvent();
    public static UnityEvent OnThrowHook = new UnityEvent();
    public static UnityEvent OnPullBackHook = new UnityEvent();
    public static UnityEvent OnHookCatch = new UnityEvent();
    public static UnityEvent OnHookThrowObject = new UnityEvent();
    public static UnityEvent OnHookStan = new UnityEvent();

    public static UnityEvent OnPauseEnable = new UnityEvent();
    public static UnityEvent OnPauseDisable = new UnityEvent();
    //public static UnityEvent OnStartCurrentWave = new UnityEvent();
    //public static UnityEvent OnCreateTower = new UnityEvent();
    //public static UnityEvent OnDestroyTower = new UnityEvent();
    //public static UnityEvent OnSellTower = new UnityEvent();


    //public static void SendEnemyKilled(int monyForKilled)
    //{
    //    OnEnemyKilled.Invoke(monyForKilled);
    //}

    //public static void SendCreateTower()
    //{
    //    OnCreateTower.Invoke();
    //}

    //public static void SendSellTower()
    //{
    //    OnSellTower.Invoke();
    //}

    //public static void SendDestroyTower()
    //{
    //    OnDestroyTower.Invoke();
    //}

    //public static void SendOnGiveMoney(int monyForKilled)
    //{
    //    OnGiveMoney.Invoke(monyForKilled);
    //}

    //public static void SendStartCurrentWave()
    //{
    //    OnStartCurrentWave.Invoke();
    //}

    public static void SendThrowHook()
    {
        OnThrowHook.Invoke();
    }

    public static void SendPullBackHook()
    {
        OnPullBackHook.Invoke();
    }

    public static void SendHookCatch()
    {
        OnHookCatch.Invoke();
    }

    public static void SendHookThrowObject()
    {
        OnHookThrowObject.Invoke();
    }
    public static void SendHookStan()
    {
        OnHookStan.Invoke();
    }


    public static void SendPauseEnable()
    {
        OnPauseEnable.Invoke();
    }
    public static void SendPauseDisable()
    {
        OnPauseDisable.Invoke();
    }

    //public static void SendOnGetMoney(int money)
    //{
    //    OnGetMoney.Invoke(money);
    //}

    //public static void SendBaseDestriy()
    //{
    //    OnDestroyBase.Invoke();
    //}

    //public static void SendChencheAmountMony()
    //{
    //    OnChencheAmountMony.Invoke();
    //}
}