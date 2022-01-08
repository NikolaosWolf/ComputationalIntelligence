using ComputationalIntelligence.Exercises;
using ComputationalIntelligence.Exercises.Interfaces;
using System;

namespace ComputationalIntelligence.Interface
{
    public sealed class Terminal
    {
        private readonly IExerciseFactory _exerciseFactory;

        public Terminal()
        {
            _exerciseFactory = new ExerciseFactory();
        }

        public void Initiate()
        {
            LoadConsoleSettings();

            Console.WriteLine("Hello there! Welcome to the \"Computational Intelligence\" exercises!");

            ExecuteExercise(1);

            Console.WriteLine("I hope you enjoyed your time! See you next time!");
        }

        private void LoadConsoleSettings()
        {
            Console.BackgroundColor = Settings.Background;
            Console.ForegroundColor = Settings.Text;
        }

        private void ExecuteExercise(byte exerciseNumber)
        {
            _exerciseFactory.Construct(exerciseNumber).Execute();
        }
    }
}
