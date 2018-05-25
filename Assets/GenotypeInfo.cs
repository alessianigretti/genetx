using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenotypeInfo : MonoBehaviour
{
	public int m_Size;

	private float[] genotype;

	void Awake()
	{
		genotype = new float[m_Size];
		SetGenotype();
	}

	// all the mutating features of the individual
	public void SetGenotype()
	{
		genotype[0] = gameObject.transform.localScale.x;
		genotype[1] = gameObject.transform.localScale.y;
		genotype[2] = gameObject.transform.localScale.z;
	}

	public void ReplaceGene(int index, float newGene)
	{
		genotype[index] = newGene;
	}

	public float[] GetGenotype()
	{
		return genotype;
	}

	public float GetGene(int index)
	{
		return genotype[index];
	}
}
