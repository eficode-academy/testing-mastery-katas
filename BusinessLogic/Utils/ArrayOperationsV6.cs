namespace Backend.Utils
{
    public class ArrayOperationsV6 : ArrayOperations
    {

        public float[] SubtractAverage(float[] ar)
        {
            if (ar == null || ar.Length == 0)
            {
                throw new ArgumentException();
            }
            // Find average
            var sum = 0f;
            foreach (float f in ar)
            {
                if (float.IsNaN(f) || float.IsInfinity(f))
                {
                    throw new ArgumentException();
                }
                sum += f;
            }
            float avg = sum / ar.Length;
            // Subtract
            var result = new float[ar.Length];
            for (int i = 0; i < ar.Length; i++)
            {
                result[i] = ar[i] - avg;
            }
            return result;
        }

    }
}