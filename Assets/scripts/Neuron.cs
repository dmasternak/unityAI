using UnityEngine;
using System.Collections.Generic;

public class Neuron {
	double[] weight;
	double   threshold;
	List<Neuron> outputNeurons;

	public Neuron(List<Neuron> outputNeurons) {
		this.outputNeurons = outputNeurons;
	}

	public bool fire(double[] input) {
		double sum = 0;
		double length = Mathf.Min(weight.Length, outputNeurons.Count);

		for(int i = 0; i < length; i++) {
			sum += input[i] * weight[i];
		}

		if(sum >= threshold) {
			return true;
		}
		else {
			return false;
		}
	}
}
