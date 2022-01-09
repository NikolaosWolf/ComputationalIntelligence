using ComputationalIntelligence.Core.Models.Enums;

namespace ComputationalIntelligence.Exercises
{
    public static class Settings1
    {
        public static readonly int d = 10; // Number of Input
        public static readonly int K = 10; // Number of Categories
        public static readonly int H1 = 3; // Number of Neurons of first hidden layer
        public static readonly int H2 = 3; // Number of Neurons of second hidden layer
        public static readonly int H3 = 3; // Number of Neurons of third hidden layer
        public static readonly ActivationFunction F = ActivationFunction.Tanh; // Type of actication function
    }
}
