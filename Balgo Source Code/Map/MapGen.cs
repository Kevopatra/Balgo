using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    public UIManager UIM;

    public GameObject SpherePrefab;
    public GameObject FinishLine;

    public int MapSize;
    public float MapScale;
    public int Octaves;
    public Vector2 Offset;
    public float Lacunarity;
    public float Persistance;
    public float Distance;

    public PhysicMaterial[] PMaterials;

    public Transform Player;

    public void GenerateMap()
    {
        List<Vector3> NoiseMap = NoiseGen.GenerateNoiseMap(MapSize, MapSize, MapScale, Offset, Octaves, Persistance,  Lacunarity, Distance);
        foreach(Vector3 v3 in NoiseMap)
        {
            if (Random.Range(0, 20) != 15)
            {
                GameObject NewSphere = Instantiate(SpherePrefab, v3, Quaternion.identity, transform);
                NewSphere.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
                NewSphere.GetComponent<Collider>().material = PMaterials[Random.Range(0, PMaterials.Length)];
            }
        }
        Transform FirstSphere = transform.GetChild(0);
        List<Transform> FinishLineOptions = new List<Transform>();
        foreach(Transform child in transform)
        {
            if(Vector3.Distance(FirstSphere.transform.position, child.position)>Distance*8)
            {
                FinishLineOptions.Add(child);
            }
        }
        Transform FinishLineSphere = FinishLineOptions[Random.Range(0, FinishLineOptions.Count)];
        GameObject finishLine = Instantiate(FinishLine, FinishLineSphere.position, Quaternion.identity, transform);
        finishLine.transform.position += new Vector3(0, 10, 0);
        finishLine.GetComponent<FinishLine>().UIM = UIM;


        Player.transform.position = transform.GetChild(0).position + new Vector3(0, (transform.GetChild(0).localScale.x/2) + 1, 0);
        Player.GetComponent<PlayerMovement>().StartPoint = Player.transform.position;

        UIM.FinishEnteringPlaymode();
    }

    public void TerminateMap()
    {
        for(int i = 0; i<transform.childCount;i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
