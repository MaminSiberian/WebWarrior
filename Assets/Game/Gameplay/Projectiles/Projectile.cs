using System.Collections.Generic;
using UnityEngine;

public class Projectile : PoolableObject, IGrabable, IDamager
{
    private Collider coll;
    private Rigidbody rb;
    private Collider sender;
    private int defaultLayer = Layers.defaultLayer;
    private int wallsLayer = Layers.walls;
    private int playerLayer = Layers.player;
    private int enemyLayer = Layers.enemy;

    public List<int> layersToDamage { get; protected set; }

    private void Awake()
    {
        coll = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        layersToDamage = new List<int>() { playerLayer };
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
        if (collision.gameObject.layer == wallsLayer)
        {
            Deactivate();
            return;
        }
    }

    public void OnGrab()
    {
        Debug.Log("Grab");
        layersToDamage = new List<int>() { defaultLayer };
    }

    public void OnRelease()
    {
        Debug.Log("Release");
        layersToDamage = new List<int>() { enemyLayer };
    }

    public void OnDamage()
    {
        Deactivate();
    }
}
