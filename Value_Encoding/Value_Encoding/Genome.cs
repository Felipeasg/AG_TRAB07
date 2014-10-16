using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Value_Encoding
{
    class Genome
    {
        int length;
        double[] genes;
        double fitness;

        public double Fitness
        {
            get { return fitness; }
            set { fitness = value; }
        }

        public double[] Genes
        {
            get { return genes; }
            set { genes = value; }
        }

        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        public Genome()
        {
            this.length = 1;
            this.genes = new double[this.length];
        }

        public Genome(int length)
        {
            this.length = length;
        }

        public void CreateGenes()
        {
            Random r = new Random(Guid.NewGuid().GetHashCode());
            for (int i = 0; i < this.length; i++)
            {
                this.genes[i] = r.NextDouble();
            }
        }

    }
}
