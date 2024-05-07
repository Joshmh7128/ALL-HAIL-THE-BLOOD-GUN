using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    public static PlayerCursor instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Cursor.visible = false;
    }

    void UpdateCursorPosition()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    void FixedUpdate()
    {
        UpdateCursorPosition();
    }
}
