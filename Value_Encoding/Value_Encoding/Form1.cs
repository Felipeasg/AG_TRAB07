using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Value_Encoding
{
    public partial class Form1 : Form
    {
        GA ga;

        public static double theActualFunction(double[] values)
        {
            if (values.GetLength(0) != 2)
                throw new ArgumentOutOfRangeException("should only have 2 args");

            double x = values[0];
            double y = values[1];
            double n = 9;  //  should be an int, but I don't want to waste time casting.

            double f1 = Math.Pow(15 * x * y * (1 - x) * (1 - y) * Math.Sin(n * Math.PI * x) * Math.Sin(n * Math.PI * y), 2);
            return f1;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void initialPopulationButton_Click(object sender, EventArgs e)
        {

            ga = new GA(0.8, 0.2, 200, 2, true);

            ga.X1min = 0;
            ga.X1max = +1;
            ga.X2min = 0;
            ga.X2min = +1;

            ga.GaFunction += new GA.Function(theActualFunction);

            ga.NbrIndvElitism = 2;
            ga.TourneySize = 4;

            ga.GenerateInitialPopulation();

            
        }

        private void nextGenerationButton_Click(object sender, EventArgs e)
        {
            //ga.NextGenerationRoullete();
            ga.NextGenerationTourney();
            double[] values;
            double fitness;

            ga.getBest(out values, out fitness);

            Console.WriteLine("Best: X = {0}, Y = {1} , fitness: {2}", values[0], values[1], fitness);
        }
    }
}
