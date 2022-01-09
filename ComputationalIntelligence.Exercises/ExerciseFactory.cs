using ComputationalIntelligence.Exercises.Interfaces;

namespace ComputationalIntelligence.Exercises
{
    public class ExerciseFactory : IExerciseFactory
    {
        public IExercise Construct(byte exercise)
        {
            switch (exercise)
            {
                case 1:
                    return new Exercise1();
                default:
                    return null;
            }
        }
    }
}
