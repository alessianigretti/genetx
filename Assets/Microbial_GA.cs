using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microbial_GA : MonoBehaviour
{
	// initialise population
	// while no success or give up
	// select 1 random individual at position x
	// select 1 random individual at k distance from x
	// compare selected individuals to find winner w and loser l
	// copy w over l with crossover probability
	// add mutation to l with mutation probability

	// CUSTOMISABLE FEATURES
	// - genotype (i.e. scale X, scale Y, scale Z)
	// - fitness function (what is the goal? i.e. instantiating
	// cubes that fit on the plane they are placed on - the largest
	// the cube, the highest the fitness but it has to not be bigger
	// than the plane it is placed on or its fitness will be 0)

	// PUBLIC FIELDS
	// - population size
	// - number of iterations?
	// - distance k
	// - crossover probability
	// - mutation probability

	public GA_Settings m_Settings;
	public GameObject m_Sample;
	public int m_PopulationSize = 10;
	public int m_NumberOfIterations = 100;
	public int m_DistanceOfRandomSelection = 3;
	public float m_CrossoverProbability = 0.5f;
	public float m_MutationProbability = 0.5f;

	private GameObject[] m_Population;

	void Start()
	{
		m_Population = m_Settings.GeneratePopulation(m_PopulationSize, m_Sample);
		
		for (int i = 0; i < m_NumberOfIterations; i++)
		{
			Evolve();
		}
	}

	void Evolve()
	{
		int firstIndIndex = Random.Range(0, m_Population.Length);
		int wrappedIndex = (firstIndIndex + m_DistanceOfRandomSelection) % m_Population.Length;

		// temporarily assigned winner and loser values
		GameObject winner = m_Population[firstIndIndex];
		GameObject loser = m_Population[wrappedIndex];

		float firstFitness = m_Settings.MeasureFitness(winner);
		float secondFitness = m_Settings.MeasureFitness(loser);

		if (firstFitness < secondFitness)
		{
			GameObject temp = winner;
			winner = loser;
			loser = temp;
		}

		GenotypeInfo winnerGenotypeInfo = winner.GetComponent<GenotypeInfo>();
		GenotypeInfo loserGenotypeInfo = loser.GetComponent<GenotypeInfo>();

		for (int i = 0; i < winnerGenotypeInfo.m_Size; i++)
		{
			// crossover
			if (Random.Range(0f, 1f) < m_CrossoverProbability)
			{
				float newGene = loserGenotypeInfo.GetGene(i);
				winnerGenotypeInfo.ReplaceGene(i, newGene);
			}

			// mutation
			if (Random.Range(0f, 1f) < m_MutationProbability)
			{
				float newGene = Random.Range(1f, 5f);
				winnerGenotypeInfo.ReplaceGene(i, newGene);
			}
		}

		Debug.Log("x: " + winnerGenotypeInfo.GetGene(0) + 
			"; y: " + winnerGenotypeInfo.GetGene(1) +
			"; z: " + winnerGenotypeInfo.GetGene(2));
	}
}
