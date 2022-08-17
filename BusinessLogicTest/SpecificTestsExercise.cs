// Copied from https://github.com/thedrlambda/specific-tests-kata

using System;
using System.Collections.Generic;
using Backend.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class SpecificTestsExercise
    {
        [DataTestMethod]
        [DynamicData(nameof(Data), DynamicDataSourceType.Property)]
        public void PositiveTest1(ArrayOperations implementation)
        {
            var input = new float[] { 1f, 2f, 3f };
            var output = implementation.SubtractAverage(input);
            CollectionAssert.AreEqual(output, new float[] { -1f, 0f, 1f });
        }

        [DataTestMethod]
        [DynamicData(nameof(Data), DynamicDataSourceType.Property)]
        public void NegativeTest1(ArrayOperations implementation)
        {
            var input = new float[] { };
            try
            {
                var output = implementation.SubtractAverage(input);
                Assert.Fail("Should throw");
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(true);
            }
        }

        public static IEnumerable<object[]> Data
        {
            get
            {
                yield return new object[] { new ArrayOperationsV1() };
                yield return new object[] { new ArrayOperationsV2() };
                yield return new object[] { new ArrayOperationsV3() };
                yield return new object[] { new ArrayOperationsV4() };
                yield return new object[] { new ArrayOperationsV5() };
                yield return new object[] { new ArrayOperationsV6() };
                yield return new object[] { new ArrayOperationsV7() };
                yield return new object[] { new ArrayOperationsV8() };
                yield return new object[] { new ArrayOperationsWorking() };
            }
        }

    }
}

