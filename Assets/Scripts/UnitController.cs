using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour, IUnit
{
    public Transform Target;
    public float speed = 0.1f;
    public Rigidbody rb;

    public int MaxHealth { get; set; }
    public int listIndex { get; set; }

    [SerializeField] 
    private int maxHealth => MaxHealth;
    private int currentHealth;
    public void Initialize(Transform target)
    {
        Target = target;
        currentHealth = MaxHealth;
    }

    private void Update()
    {
        Vector3 direction = Target.position - transform.position;
        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
    }

    public Vector3 GetPosition() =>  transform.position;

    public void SetDamage(int value)
    {
        Debug.Log("Я получил такой урон = " + value);
        currentHealth -= value;
        if (currentHealth <= 0)
        {
            ICreatorUnit.instance.RemoveUnit(listIndex);
            Destroy(gameObject);
        }
    }
    
}
