using ComputationalIntelligence.Exercises.Interfaces;

namespace ComputationalIntelligence.Exercises
{
    public class ExerciseFactory : IExerciseFactory
    {
        public IExercise Construct(byte exercise)
        {
            switch (exercise)
            {
                default:
                    return null;
            }
        }
    }
}
