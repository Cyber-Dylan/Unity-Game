using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterDestroyed_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyTimer());
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
