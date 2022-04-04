namespace Backend.Utils
{

    public class ArrayOperationsV7 : ArrayOperations
    {

        public float[] SubtractAverage(float[] ar)
        {
            if (ar == null || ar.Length == 0)
            {
                throw new ArgumentException();
            }
            // Find average
            var avg = 0f;
            var t = 1;
            foreach (float f in ar)
            {
                if (float.IsNaN(f) || float.IsInfinity(f))
                {
                    throw new ArgumentException();
                }
                avg += (f - avg) / t;
                ++t;
            }
            // Subtract
            for (int i = 0; i < ar.Length; i++)
            {
                ar[i] -= avg;
            }
            return ar;
        }

    }
}