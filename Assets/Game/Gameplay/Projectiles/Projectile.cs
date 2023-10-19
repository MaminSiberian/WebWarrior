using UnityEngine;

public class Projectile : PoolableObject
{
    private Collider coll;
    private Rigidbody rb;
    private Collider sender;

    private void Awake()
    {
        coll = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }
    private void OnDisable()
    {
        TurnOnCollision();
        rb.velocity = Vector3.zero;
    }
    public void TurnOffCollision(Collider sender)
    {
        this.sender = sender;
        Physics.IgnoreCollision(coll, this.sender);
    }
    private void TurnOnCollision()
    {
        if (sender == null) return;
        Physics.IgnoreCollision(coll, this.sender, false);
        sender = null;
    }
    private void OnCollisionEnter(Collision collision)
    {
        TurnOnCollision();
        var obj = collision.gameObject.GetComponent<IDamagable>();

        if (obj != null)
        {
            obj.GetDamage();
            Deactivate();
        }
    }
}
