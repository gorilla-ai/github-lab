namespace sample;

public class PrimeService
{
    public bool IsPrime(int candidate)
    {
        var root = Math.Sqrt(candidate);
        for (int divisor = 2; divisor <= root; ++divisor)
        {
            if (candidate % divisor == 0)
            {
                return false;
            }
        }

        return true;
    }
}
