﻿using NUnit.Framework;
using PS2_TTK_calculator;



namespace NUnit.Tests1
{
    [TestFixture]
    internal class MultinomialDistribution_Test
    {
        private static readonly int MockNumberOfTrials = 20;
        private static readonly double[] MockProbabilites = { 0.3, 0.2 };
        private readonly MultinomialDistribution MockMultinomialDistribution = new MultinomialDistribution(MockNumberOfTrials, MockProbabilites);

        private int Factorial(int n)
        {
            if (n == 0 || n == 1)
            {
                return 1;
            }
            else
            {
                return n * Factorial(n - 1);
            }

        }

        [Test]
        public void Test_Binomial()
        {
            Assert.AreEqual(2300, MockMultinomialDistribution.Binomial(25, 3));
            Assert.AreEqual(12870, MockMultinomialDistribution.Binomial(16, 8));

        }

        [Test]
        public void Test_Multinomial()
        {
            int[] k1 = { 1, 4, 4, 2 };
            Assert.AreEqual(34650, MockMultinomialDistribution.Multinomial(k1));
            int[] k2 = { 2, 4, 3, 2, 1 };
            Assert.AreEqual(831600, MockMultinomialDistribution.Multinomial(k2));
            int[] k3 = { 0, 1 };
            Assert.AreEqual(1, MockMultinomialDistribution.Multinomial(k3));
        }

        [Test]
        public void Test_MultinomialPMF()
        {
            double[,] test_multPMF = MockMultinomialDistribution.GetDist();
            Assert.AreEqual(0.006898447265625, test_multPMF[5, 1], 0.0001);
            Assert.AreEqual(0.0051099609375, test_multPMF[3, 2], 0.0001);
            Assert.AreNotEqual(0, test_multPMF[0, 20]);
            foreach (double testValue in test_multPMF)
            {
                Assert.GreaterOrEqual(testValue, 0);
            }
        }

        [Test]
        public void Test_ExtremeCases()
        {
            MultinomialDistribution multinomialDistribution = new MultinomialDistribution(50, new double[] { 0, 1 });
            double[,] test_multPMF = multinomialDistribution.GetDist();
            for (int i = 0; i < test_multPMF.GetLength(0); ++i)
            {
                for (int j = 0; j < test_multPMF.GetLength(1) - i; ++j)
                {
                    Assert.LessOrEqual(0, test_multPMF[i, j], string.Format("{0},{1}", i, j));
                    Assert.GreaterOrEqual(1, test_multPMF[i, j], string.Format("{0},{1}", i, j));
                }
            }
        }
    }
}