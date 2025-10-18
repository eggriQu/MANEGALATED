using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private bool key1;
    private bool key2;
    private bool key3;
    private bool key4;
    private bool key6;
    public bool anyInput;

    public int lastKey;
    public int key1st;
    public int key2nd;
    public int key3rd;
    public int key4th;
    public int key5th;
    public int key6th;

    public float timeThreshold;
    public MovementSM smMV;
    // Start is called before the first frame update
    void Start()
    {
        SetKeys(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (smMV.isPlayer1)
        {
            if (!smMV.trackOpp.flipped)
            {
                key1 = Input.GetKeyDown(KeyCode.A) && Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.S);
                key3 = Input.GetKeyDown(KeyCode.D) && Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.S);
                key2 = Input.GetKeyDown(KeyCode.S) && !key1 || Input.GetKeyUp(KeyCode.A) && Input.GetKey(KeyCode.S);
                key4 = Input.GetKeyDown(KeyCode.A) && !key1 || Input.GetKeyUp(KeyCode.S) && Input.GetKey(KeyCode.A);
                key6 = Input.GetKeyDown(KeyCode.D) && !key3 || Input.GetKeyUp(KeyCode.S) && Input.GetKey(KeyCode.D);
            }
            else
            {
                key1 = Input.GetKeyDown(KeyCode.D) && Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.S);
                key3 = Input.GetKeyDown(KeyCode.A) && Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.S);
                key2 = Input.GetKeyDown(KeyCode.S) && !key1 || Input.GetKeyUp(KeyCode.D) && Input.GetKey(KeyCode.S);
                key4 = Input.GetKeyDown(KeyCode.D) && !key1 || Input.GetKeyUp(KeyCode.S) && Input.GetKey(KeyCode.D);
                key6 = Input.GetKeyDown(KeyCode.A) && !key3 || Input.GetKeyUp(KeyCode.S) && Input.GetKey(KeyCode.A);
            }
        }
        else
        {
            if (!smMV.trackOpp.flipped)
            {
                key1 = Input.GetKeyDown(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) && Input.GetKeyDown(KeyCode.DownArrow);
                key3 = Input.GetKeyDown(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow) && Input.GetKeyDown(KeyCode.DownArrow);
                key2 = Input.GetKeyDown(KeyCode.DownArrow) && !key1 || Input.GetKeyUp(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow);
                key4 = Input.GetKeyDown(KeyCode.LeftArrow) && !key1 || Input.GetKeyUp(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow);
                key6 = Input.GetKeyDown(KeyCode.RightArrow) && !key3 || Input.GetKeyUp(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow);
            }
            else
            {
                key1 = Input.GetKeyDown(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow) && Input.GetKeyDown(KeyCode.DownArrow);
                key3 = Input.GetKeyDown(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) && Input.GetKeyDown(KeyCode.DownArrow);
                key2 = Input.GetKeyDown(KeyCode.DownArrow) && !key1 || Input.GetKeyUp(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow);
                key4 = Input.GetKeyDown(KeyCode.RightArrow) && !key1 || Input.GetKeyUp(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow);
                key6 = Input.GetKeyDown(KeyCode.LeftArrow) && !key3 || Input.GetKeyUp(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow);
            }
        }
        anyInput = key1 || key3 || key2 || key4 || key6;

        if (key1)
        {
            SetKeys(1);
        }
        
        if (key2)
        {
            SetKeys(2);
        }

        if (key3)
        {
            SetKeys(3);
        }

        if (key4)
        {
            SetKeys(4);
        }

        if (key6)
        {
            SetKeys(6);
        }

        if (anyInput)
        {
            timeThreshold = 0;
        }
    }

    void FixedUpdate()
    {
        if (timeThreshold < 0.4f)
        {
            timeThreshold += 0.0166666666667f;
        }
        else
        {
            timeThreshold = 0.4f;
        }
    }

    private void SetKeys(int lastPressedKey)
    {
        key6th = key5th;
        key5th = key4th;
        key4th = key3rd;
        key3rd = key2nd;
        key2nd = key1st;
        key1st = lastKey;
        lastKey = lastPressedKey;
    }
}
