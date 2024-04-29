using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Lifetime : MonoBehaviour
{
    public float Duration = 2f;

    public float _timer;
    // Start is called before the first frame update
    void Start()
    {
        _timer = Duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            return;
        }
        Destroy(gameObject);
    }
}
