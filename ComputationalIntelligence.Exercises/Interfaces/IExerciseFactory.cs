namespace ComputationalIntelligence.Exercises.Interfaces
{
    public interface IExerciseFactory
    {
        IExercise Construct(byte exercise);
    }
}
