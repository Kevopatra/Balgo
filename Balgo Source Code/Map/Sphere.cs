using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public float MinScaleSpeed, MaxScaleSpeed;
    public float MinGrowthTime, MaxGrowthTime;
    float ScaleSpeed;
    float GrowthTime;
    bool Growing = true;
    public void Start()
    {
        ScaleSpeed = Random.Range(MinScaleSpeed, MaxScaleSpeed);
        GrowthTime = Random.Range(MinGrowthTime, MaxGrowthTime);

        StartCoroutine(WaitUntilGrown());
    }
    public void Update()
    {
        if (Growing)
        {
            Vector3 DesiredScale = new Vector3(transform.localScale.x + 1, transform.localScale.y + 1, transform.localScale.z + 1);
            transform.localScale = Vector3.MoveTowards(transform.localScale, DesiredScale, ScaleSpeed * Time.deltaTime);
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer==7)
        {
            Utils.PlaySFX("Boing");
        }
    }
    public IEnumerator WaitUntilGrown()
    {
        yield return new WaitForSeconds(GrowthTime);
        Growing = false;
    }
}
