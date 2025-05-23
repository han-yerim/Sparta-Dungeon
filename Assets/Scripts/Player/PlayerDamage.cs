using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerDamage : MonoBehaviour
{
    public float fallDamageThreshold = -10f;
    public int maxFallDamage = 30;

    private Rigidbody rb;
    private PlayerCondition playerCondition;
    private float lastYVelocity;
    private bool wasGrounded;
    public Action onTakeDamage;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCondition = GetComponent<PlayerCondition>();
    }

    void Update()
    {
        bool isGrounded = CheckIsGrounded();

        // ¶¥¿¡ ´ê¾ÒÀ» ¶§¸¸ ³«ÇÏ ¼Óµµ Æò°¡
        if (isGrounded && !wasGrounded)
        {
            if (lastYVelocity < fallDamageThreshold)
            {
                int damage = Mathf.RoundToInt(Mathf.Abs(lastYVelocity) * 2);
                damage = Mathf.Min(damage, maxFallDamage);
                playerCondition.TakePhysicalDamage(damage);
                onTakeDamage?.Invoke();
                Debug.Log($"³«ÇÏ µ¥¹ÌÁö {damage} ÀÔÀ½!");
            }
        }

        lastYVelocity = rb.velocity.y;
        wasGrounded = isGrounded;
    }

    bool CheckIsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f, LayerMask.GetMask("Ground"));
    }
}
