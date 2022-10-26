namespace sample;

public class PrimeService
{
    public bool IsPrime(int candidate)
    {
        if (candidate < 2)
        {
            return false;
        }
        else if (candidate % 2 == 0 && candidate != 2)
        {
            return false;
        }

        var root = Math.Sqrt(candidate);
        for (int divisor = 3; divisor <= root; divisor += 2)
        {
            if (candidate % divisor == 0)
            {
                return false;
            }
        }

        return true;
    }
}
