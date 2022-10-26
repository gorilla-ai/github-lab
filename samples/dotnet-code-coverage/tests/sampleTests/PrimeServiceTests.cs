namespace sampleTests;

public class PrimeServiceTests
{
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(5)]
    public void IsPrime_PrimesLessThan5_ReturnTrue(int value)
    {
        // arrange
        var service = new PrimeService();

        // act
        var actual = service.IsPrime(value);

        // assert
        Assert.IsTrue(actual);
    }

    [TestCase(4)]
    public void IsPrime_NonPrimesLessThan5_ReturnFalse(int value)
    {
        // arrange
        var service = new PrimeService();

        // act
        var actual = service.IsPrime(value);

        // assert
        Assert.IsFalse(actual);
    }
}