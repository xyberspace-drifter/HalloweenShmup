﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRaiser : MonoBehaviour
{
    Tickmaster tm;
    public int ticksToRaise;
    public int ticksToReturn;
    public Transform dropLocation;
    Vector3 origPos;
    public float speed = 0;
    public float speedMultiplierUp;
    public float speedMultiplier;
    public float speedMax;
    bool isDropping = false;
    bool isReturning = false;
    public bool reversed = false;
    void Dropping()
    {
        speed += Time.deltaTime * speedMultiplier;
        transform.position += Vector3.down * Time.deltaTime * speed;
        if (speed >= speedMax)
        {
            speed = speedMax;
        }
        if (Vector3.Distance(transform.position, origPos) < 0.1f)
        {
            speed = 0;
            isDropping = false;
            return;
        }
    }

    void Returning()
    {
        speed += Time.deltaTime * speedMultiplierUp;
        transform.position += Vector3.up * Time.deltaTime * speed;
        if (speed >= speedMax)
        {
            speed = speedMax;
        }

        if (Vector3.Distance(transform.position, dropLocation.position) < 0.1f)
        {
            speed = 0;
            isReturning = false;
            return;
        }
    }
    private void Start()
    {
        tm = FindObjectOfType<Tickmaster>().GetComponent<Tickmaster>();
        origPos = transform.position;
    }
    private void Update()
    {
        if (tm.ticks == ticksToRaise)
        {
            isReturning = true;
        }
        if (tm.ticks == ticksToReturn)
        {
            isDropping = true;
        }
        if (isDropping)
        {
            Dropping();
        }
        if (isReturning)
        {
            Returning();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            FindObjectOfType<GameManager>().PlayerHit();
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }
}
