using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public static class NoiseGen
{
	public static List<Vector3> GenerateNoiseMap(int mapWidth, int mapHeight, float scale, Vector2 offset, int octaves, float persistance, float lacunarity, float Distance)
	{
		List<Vector3> noiseMap = new List<Vector3>();

		int Seed = Random.Range(1, 10000);
		System.Random prng = new System.Random(Seed);
		Vector2[] octaveOffsets = new Vector2[octaves];
		for (int i = 0; i < octaves; i++)
		{
			float offsetX = prng.Next(-100000, 100000) + offset.x;
			float offsetY = prng.Next(-100000, 100000) + offset.y;
			octaveOffsets[i] = new Vector2(offsetX, offsetY);
		}

		if (scale <= 0)
		{
			scale = 0.0001f;
		}

		float maxNoiseHeight = float.MinValue;
		float minNoiseHeight = float.MaxValue;

		float halfWidth = mapWidth / 2f;
		float halfHeight = mapHeight / 2f;

		for (int y = 0; y < mapHeight; y++)
		{
			for (int x = 0; x < mapWidth; x++)
			{
				float amplitude = 1;
				float frequency = 1;
				float noiseHeight = 0;

				for (int i = 0; i < octaves; i++)
				{
					float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x; //scale*frequency
					float sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y;

					float perlinValue = Mathf.PerlinNoise(sampleX, sampleY); 
					noiseHeight += perlinValue * amplitude;

					amplitude *= persistance;
					frequency *= lacunarity;

					Vector3 Tile = new Vector3();
					Tile.x = x*Distance;
					Tile.z = y*Distance;
					Tile.y = perlinValue*30;
					noiseMap.Add(Tile);
				}

				if (noiseHeight > maxNoiseHeight)
				{
					maxNoiseHeight = noiseHeight;
				}
				else if (noiseHeight < minNoiseHeight)
				{
					minNoiseHeight = noiseHeight;
				}
			}
		}
		return noiseMap;
	}
}