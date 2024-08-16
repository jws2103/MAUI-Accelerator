using System.Globalization;
using Moq.AutoMock;

namespace MauiAccelerator.UnitTests;

public abstract class UnitTestBase<TSut> : UnitTestBase where TSut : class
{
    protected UnitTestBase()
    {
        Sut = Mocker.CreateInstance<TSut>();
    }

    protected TSut Sut { get; }
}

public abstract class UnitTestBase
{
    protected UnitTestBase()
    {
        CultureInfo.CurrentCulture = new CultureInfo("en-AU");

        Mocker = new AutoMocker();
    }

    protected AutoMocker Mocker { get; }
}