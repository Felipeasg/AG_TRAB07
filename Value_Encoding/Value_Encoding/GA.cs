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

        public int TourneySize
        {
            get { return tourneySize; }
            set { tourneySize = value; }
        }
        public delegate double Function(double[] values);

        Function gaFunction;

        int nbrIndvElitism;

        public double X1min
        {
            get { return x1min; }
            set { x1min = value; }
        }
        public double X1max
        {
            get { return x1max; }
            set { x1max = value; }
        }
        public double X2min
        {
            get { return x2min; }
            set { x2min = value; }
        }
        public double X2max
        {
            get { return x2max; }
            set { x2max = value; }

        }
        public int NbrIndvElitism
        {
            get { return nbrIndvElitism; }
            set { nbrIndvElitism = value; }
        }

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

            this.generation = new ArrayList();

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

            this.generation = new ArrayList();
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
            ArrayList newGeneration = new ArrayList();
            Random r1 = new Random(Guid.NewGuid().GetHashCode());
            Random r2 = new Random(Guid.NewGuid().GetHashCode());

            if (this.elitism)
            {
                this.generation.Sort(new GenomeComparer());

                for (int i = 0; i < this.nbrIndvElitism; i++)
                {
                    //verificar se o melhor individuo esta na ultima ou primeira posicao
                    //newGeneration.Add(this.generation[i]);
                    newGeneration.Add(this.generation[(this.generation.Count - 1) - i]);
                }
            }

            while(newGeneration.Count <= this.populationSize)
            {

                Genome indv1 = this.fitnessProportionateSelection();
                Genome indv2 = this.fitnessProportionateSelection();
                Genome child1 = new Genome(2);
                Genome child2 = new Genome(2);

                if (this.crossOverRate > r1.NextDouble())
                {
                    CrossOver(indv1, indv2, child1, child2);
                }

                if (this.mutationRate > r2.NextDouble())
                {
                    Mutate(child1);
                    Mutate(child2);
                }

                newGeneration.Add(child1);
                newGeneration.Add(child2);
            }

            this.generation.Clear();

            this.generation = new ArrayList(newGeneration);
        }

        public void NextGenerationTourney()
        {
            ArrayList newGeneration = new ArrayList();
            Random r1 = new Random(Guid.NewGuid().GetHashCode());
            Random r2 = new Random(Guid.NewGuid().GetHashCode());

            if (this.elitism)
            {
                this.generation.Sort(new GenomeComparer());

                for (int i = 0; i < this.nbrIndvElitism; i++)
                {
                    //verificar se o melhor individuo esta na ultima ou primeira posicao
                    //newGeneration.Add(this.generation[i]);
                    newGeneration.Add(this.generation[(this.generation.Count - 1) - i]);
                }
            }

            while (newGeneration.Count <= this.populationSize)
            {

                Genome indv1 = this.tourneySelection();
                Genome indv2 = this.tourneySelection();
                Genome child1 = new Genome(2);
                Genome child2 = new Genome(2);

                if (this.crossOverRate > r1.NextDouble())
                {
                    CrossOver(indv1, indv2, child1, child2);
                }

                if (this.mutationRate > r2.NextDouble())
                {
                    Mutate(child1);
                    Mutate(child2);
                }

                newGeneration.Add(child1);
                newGeneration.Add(child2);
            }

            this.generation.Clear();

            this.generation = new ArrayList(newGeneration);
        }

        Genome fitnessProportionateSelection()
        {
            double totalFitness = 0;

            for (int i = 0; i < this.populationSize; i++)
            {
                Genome g = ((Genome) generation[i]);
                g.Fitness = this.calculateFitness(g.Genes);
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
            Random r = new Random(Guid.NewGuid().GetHashCode());
            double beta = r.NextDouble();

            child1.Genes[0] = beta * parent1.Genes[0] + (1 - beta) * parent2.Genes[0];
            child1.Genes[1] = beta * parent1.Genes[1] + (1 - beta) * parent2.Genes[1];

            child2.Genes[0] = (1 - beta) * parent1.Genes[0] + beta * parent2.Genes[0];
            child2.Genes[1] = (1 - beta) * parent1.Genes[1] + beta * parent2.Genes[1];


            child1.Fitness = this.calculateFitness(child1.Genes);
            child2.Fitness = this.calculateFitness(child2.Genes);
        }

        void Mutate(Genome individual)
        {
            Random r = new Random(Guid.NewGuid().GetHashCode());
            Random mutationValue = new Random(Guid.NewGuid().GetHashCode());
            double[] values = new double[this.genomeSize];

            individual.Genes[0] = r.NextDouble();
            individual.Genes[1] = r.NextDouble();

            individual.Fitness = this.calculateFitness(individual.Genes);

           /* int idxIndividual = r.Next(0, generation.Count);

            for (int i = 0; i < this.genomeSize; i++)
            {
                values[i] = mutationValue.NextDouble();
                ((Genome)generation[idxIndividual]).Genes[i] = values[i];                  
            }*/

            //((Genome)generation[idxIndividual]).Fitness = this.gaFunction(values);
        }

        double calculateFitness(double[] values)
        {
            double value = 0;
            double[] x = new double[values.Length];

            x[0] = (((this.x1max - this.x1min) / 1) * values[0]) + this.x1min;
            x[1] = (((this.x2max - this.x2min) / 1) * values[1]) + this.x2min;
            //Implement the conversion to function domain using the genes and return fitness

            value = this.gaFunction(x);
          
            return value;
        }

        public double bestFitness()
        {
            ArrayList pop = new ArrayList(this.generation);

            pop.Sort(new GenomeComparer());

            return ((Genome)pop[pop.Count - 1]).Fitness;
        }

        public void getBest(out double[] values, out double fitness)
        {
            ArrayList pop = new ArrayList(this.generation);

            pop.Sort(new GenomeComparer());

            double[] x = new double[genomeSize];

            x[0] = (((this.x1max - this.x1min) / 1) * ((Genome)pop[pop.Count - 1]).Genes[0]) + this.x1min;
            x[1] = (((this.x2max - this.x2min) / 1) * ((Genome)pop[pop.Count - 1]).Genes[1]) + this.x2min;

            values = x;

            fitness = ((Genome)pop[pop.Count - 1]).Fitness;
        }
    }
}
