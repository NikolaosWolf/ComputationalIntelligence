using ComputationalIntelligence.DataSets.Models;
using ComputationalIntelligence.DataSets.Models.Enums;
using System;
using System.Collections.Generic;

namespace ComputationalIntelligence.DataSets
{
    public class NoiseGenerator
    {
        public ISet<Example> GenerateNoise(ISet<Example> learningSet)
        {
            var rnd = new Random();

            foreach (Example example in learningSet)
            {
                double probability = rnd.NextDouble();

                if (probability <= 0.1)
                    SetRandomCategory(example, rnd);
            }

            return learningSet;
        }

        private void SetRandomCategory(Example example, Random rnd)
        {
            var randomCategory = rnd.NextDouble();
            switch (example.Category)
            {
                case Category.C1:
                    if (randomCategory <= 0.33)
                        example.Category = Category.C2;
                    else if (randomCategory >= 0.66)
                        example.Category = Category.C4;
                    else
                        example.Category = Category.C3;
                    break;
                case Category.C2:
                    if (randomCategory <= 0.33)
                        example.Category = Category.C1;
                    else if (randomCategory >= 0.66)
                        example.Category = Category.C4;
                    else
                        example.Category = Category.C3;
                    break;
                case Category.C3:
                    if (randomCategory <= 0.33)
                        example.Category = Category.C1;
                    else if (randomCategory >= 0.66)
                        example.Category = Category.C4;
                    else
                        example.Category = Category.C2;
                    break;
                case Category.C4:
                    if (randomCategory <= 0.33)
                        example.Category = Category.C1;
                    else if (randomCategory >= 0.66)
                        example.Category = Category.C3;
                    else
                        example.Category = Category.C2;
                    break;
                default:
                    throw new ArgumentException("Category is wrong.");
            }
        }
    }
}
