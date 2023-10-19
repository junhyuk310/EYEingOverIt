using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clear : MonoBehaviour
{
    public GameObject panel;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Clear"))
        {
            panel.SetActive(true);
        }
    }
}
