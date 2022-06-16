using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Doomed());
    }
    public IEnumerator Doomed()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
