using ComputationalIntelligence.DataSets.Models;
using ComputationalIntelligence.DataSets.Models.Enums;
using System;
using System.Collections.Generic;

namespace ComputationalIntelligence.DataSets
{
    public class ExampleGenerator
    {
        private readonly int _learningSetSize = 4000;
        private readonly int _controlSetSize = 4000;

        public ExampleResult Generate()
        {
            Random rnd = new Random();

            var examples = new ExampleResult
            {
                LearningSet = GenerateLearningSet(rnd),
                ControlSet = GenerateControlSet(rnd)
            };

            return examples;
        }

        private ISet<Example> GenerateLearningSet(Random rnd)
        {
            HashSet<Example> learningSet = new HashSet<Example>(_learningSetSize);

            for (int i = 0; i < _learningSetSize; i++)
                learningSet.Add(CreateExample(rnd));

            return learningSet;
        }

        private ISet<Example> GenerateControlSet(Random rnd)
        {
            HashSet<Example> controlSet = new HashSet<Example>(_controlSetSize);

            for (int i = 0; i < _controlSetSize; i++)
                controlSet.Add(CreateExample(rnd));

            return controlSet;
        }

        private Example CreateExample(Random rnd)
        {
            var example = new Example()
            {
                X1 = Math.Round((rnd.NextDouble() * 2) - 1, 2),
                X2 = Math.Round((rnd.NextDouble() * 2) - 1, 2)
            };

            SetCategory(example);

            return example;
        }

        private void SetCategory(Example example)
        {
            if (Math.Pow(example.X1 - 0.5, 2.0) + Math.Pow(example.X2 - 0.5, 2.0) <= 0.16)
            {
                example.Category = Category.C1;
            }
            else if (Math.Pow(example.X1 + 0.5, 2.0) + Math.Pow(example.X2 + 0.5, 2.0) <= 0.16)
            {
                example.Category = Category.C1;
            }
            else if (Math.Pow(example.X1 - 0.5, 2.0) + Math.Pow(example.X2 + 0.5, 2.0) <= 0.16)
            {
                example.Category = Category.C2;
            }
            else if (Math.Pow(example.X1 + 0.5, 2.0) + Math.Pow(example.X2 - 0.5, 2.0) <= 0.16)
            {
                example.Category = Category.C2;
            }
            else
            {
                if (example.X1 >= 0 && example.X2 >= 0 || example.X1 < 0 && example.X2 < 0)
                {
                    example.Category = Category.C3; // 1rst or 3rd quadrant
                }
                else
                {
                    example.Category = Category.C4; //2nd or 4rth quadrant
                }
            }
        }
    }
}
