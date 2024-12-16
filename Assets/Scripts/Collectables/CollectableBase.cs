using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectableBase : MonoBehaviour
{
    private Rigidbody rb;
    public int amount;
    public enum collectableTypes
    {
        Null,
        Log
    }
   
    
    public collectableTypes collectableType;
    public virtual void  Start()
    {
        rb = GetComponent<Rigidbody>();
        ThrowOnSpawn();
    }
    protected virtual void Update()
    {
        Rotate();
    }

    public virtual void Collected()
    {

    }
   
    private void Rotate()
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);
    }
    private void ThrowOnSpawn()
    {
        rb.AddRelativeForce(new Vector3(Random.Range(-70f, 70f), 150f, Random.Range(-70f, 70f)));
    }
}
