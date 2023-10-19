using UnityEngine;

public class TestPlayer : MonoBehaviour, IDamagable
{
    public void GetDamage()
    {
        Debug.Log(name + " damaged");
    }
}
