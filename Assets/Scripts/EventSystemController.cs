using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystemController : MonoBehaviour
{
    private static EventSystemController _eventSystemController;

    private void Awake()
    {
        if (_eventSystemController == null)
        {
            _eventSystemController = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
