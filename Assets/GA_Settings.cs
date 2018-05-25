using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_Settings : MonoBehaviour
{
	// generate population
	public GameObject[] GeneratePopulation(int size, GameObject sample)
	{
		GameObject[] population = new GameObject[size];

		for (int i = 0; i < size; i++)
		{
			float randomPositionX = Random.Range(-5, 5);
			float randomPositionZ = Random.Range(-5, 5);
			sample.transform.position = new Vector3(randomPositionX, 0f, randomPositionZ);
			float randomScale = Random.Range(1f, 5f);
			sample.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
			
			population[i] = Instantiate(sample);
		}

		return population;
	}
	
	// measure of fitness
	public float MeasureFitness(GameObject individual)
	{
		Vector3 scale = individual.transform.localScale;
		if (scale.x + scale.y + scale.z > 6f)
		{
			return 0;
		}
		return 1;
	}
}
