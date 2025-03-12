private readonly BasicCalc _calculator;

public Basic() 
{
    _calculator = new BasicCalc();
}

[Fact]
public void Log_ShouldReturnCorrectLog()
{
    double result = _calculator.Log(9, 3);
    Assert.Equal(2, result);
}

[Theory]
[InlineData(9, 3, 2)]
[InlineData(25, 5, 2)]
[InlineData(100, 10, 2)]
public void Log_Theory(double a, double b, double expectedResult)
{
    double result = _calculator.Log(a, b);
    Assert.Equal(expectedResult, result, 6);
}

[Theory]
[InlineData(0, -2, 2)]
[InlineData(-1, 2, 2)]
[InlineData(4, 1, -2)]
public void Log_Exception(double a, double b, double expectedResult)
{
    Assert.Throws<ArgumentException>(() => _calculator.Log(a, b));
}