using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Value_Encoding
{
    class GA
    {
        double mutationRate;
        double crossOverRate;
        bool elitism;
        ArrayList generation;
        int populationSize;
        int genomeSize;
        double x1min;
        double x1max;
        double x2min;
        double x2max;
        int tourneySize;
        public delegate double Function(double[] values);

        Function gaFunction;

        internal Function GaFunction
        {
            get { return gaFunction; }
            set { gaFunction = value; }
        }

        public int PopulationSize
        {
            get { return populationSize; }
            set { populationSize = value; }
        }

        
        public bool Elitism
        {
            get { return elitism; }
            set { elitism = value; }
        }

        public double CrossOverRate
        {
            get { return crossOverRate; }
            set { crossOverRate = value; }
        }
        

        public double MutationRate
        {
            get { return mutationRate; }
            set { mutationRate = value; }
        }

        public GA()
        {
            this.crossOverRate = 0.8;
            this.mutationRate = 0.1;
            this.elitism = false;
        }

        public GA(double crossRate, double mutateRate, int populationSize, int genomeSize, bool elitism)
        {
            this.crossOverRate = crossRate;
            this.mutationRate = mutateRate;
            this.populationSize = populationSize;
            this.genomeSize = genomeSize;
            this.elitism = elitism;
            this.x1min = 0;
            this.x1max = 0;
            this.x2min = 0;
            this.x2max = 0;

            this.tourneySize = 1;

        }

        public GA(double crossRate, double mutateRate, int populationSize, int genomeSize, bool elitism, double x1min, double x1max, double x2min, double x2max)
        {
            this.crossOverRate = crossRate;
            this.mutationRate = mutateRate;
            this.populationSize = populationSize;
            this.genomeSize = genomeSize;
            this.elitism = elitism;

            this.x1min = x1min;
            this.x1max = x1max;
            this.x2min = x2min;
            this.x2max = x2max;

            this.tourneySize = 1;
        }

        public void GenerateInitialPopulation()
        {
            for (int i = 0; i < this.populationSize; i++)
            {
                Genome g = new Genome(this.genomeSize);
                g.CreateGenes();
                g.Fitness = calculateFitness(g.Genes);
                generation.Add(g);
            }
        }

        public void NextGenerationRoullete()
        {

        }

        public void NextGenerationTourney()
        {
        }

        Genome fitnessProportionateSelection()
        {
            double totalFitness = 0;

            for (int i = 0; i < this.populationSize; i++)
            {
                Genome g = ((Genome) generation[i]);
                g.Fitness = calculateFitness(g.Genes);
                totalFitness += g.Fitness;
            }

            generation.Sort(new GenomeComparer());

            double fitness = 0.0;
            List<double> fitnessTable = new List<double>();

            for (int i = 0; i < this.populationSize; i++)
            {
                fitness += ((Genome)this.generation[i]).Fitness;
                fitnessTable.Add(fitness);
            }

            Random r = new Random(Guid.NewGuid().GetHashCode());
            double randomFitness = r.NextDouble() * totalFitness;
            int idx = -1;
            int mid;
            int first = 0;
            int last = populationSize - 1;
            mid = (last - first) / 2;

            //  ArrayList's BinarySearch is for exact values only
            //  so do this by hand.
            while (idx == -1 && first <= last)
            {
                if (randomFitness < (double)fitnessTable[mid])
                {
                    last = mid;
                }
                else if (randomFitness > (double)fitnessTable[mid])
                {
                    first = mid;
                }
                mid = (first + last) / 2;
                //  lies between i and i+1
                if ((last - first) == 1)
                    idx = last;
            }

            return (Genome)generation[idx];
        }

        Genome tourneySelection()
        {
            ArrayList tourneyGenomes = new ArrayList();
            Random r = new Random(Guid.NewGuid().GetHashCode());

            int idx = 0;

            for (int i = 0; i < this.tourneySize; i++)
            {
                idx = r.Next(0,this.populationSize);

                Genome g = (Genome)this.generation[idx];
                tourneyGenomes.Add(g);
            }

            tourneyGenomes.Sort(new GenomeComparer());

            return (Genome)tourneyGenomes[0];
        }

        void CrossOver(Genome parent1, Genome parent2, Genome child1, Genome child2)
        {

        }

        void Mutate(Genome individual)
        {

        }

        double calculateFitness(double[] values)
        {
            double value = 0;
            double[] x = new double[values.Length];

            x[0] = (((this.x1max - this.x1min) / 1) * values[0]) + this.x1min;
            x[1] = (((this.x2max - this.x2min) / 1) * values[0]) + this.x2min;
            //Implement the conversion to function domain using the genes and return fitness

            this.gaFunction(x);
          
            return value;
        }
    }
}
