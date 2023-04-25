using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplsionPrefab : MonoBehaviour
{
    public float time = .01f;

    public void Start()
    {
        StartCoroutine(Explosion());

    }
    private void FixedUpdate()
    {
        //StartCoroutine(Explosion());
    }
    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
