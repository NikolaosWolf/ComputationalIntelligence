using ComputationalIntelligence.Exercises;
using ComputationalIntelligence.Exercises.Interfaces;
using System;
using System.Linq;

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
            LoadMainMenu();

            string option = GetOption();
            while (!option.Equals("Exit", StringComparison.OrdinalIgnoreCase))
            {
                ExecuteExercise(byte.Parse(option));

                LoadMainMenu();
                option = GetOption();
            }

            Console.WriteLine("I hope you enjoyed your time! See you next time!");
        }

        private void LoadConsoleSettings()
        {
            Console.BackgroundColor = Settings.Background;
            Console.ForegroundColor = Settings.Text;
        }

        private void LoadMainMenu()
        {
            Console.WriteLine("Please choose an exercise or exit:");
            Console.WriteLine("\t- Exercise 1: Multilevel Perceptron");
            Console.WriteLine("\t- Exercise 2: K-Means");
            Console.WriteLine("\t- Exit");
        }

        private string GetOption()
        {
            string option = Console.ReadLine();

            while (!OptionIsValid(option))
            {
                Console.WriteLine("Your option was invalid");

                LoadMainMenu();
                option = Console.ReadLine();
            }

            return option;
        }

        private bool OptionIsValid(string option)
        {
            byte[] validExercises = { 1, 2 };

            return option.Equals("Exit", StringComparison.OrdinalIgnoreCase) ||
                   (byte.TryParse(option, out byte exerciseNumber) && validExercises.Contains(exerciseNumber));
        }

        private void ExecuteExercise(byte exerciseNumber)
        {
            _exerciseFactory.Construct(exerciseNumber).Execute();
        }
    }
}
