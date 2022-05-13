using Xunit;
using CardShow.Data.Factories;

namespace CardShow.Test.Data;

public class CardSetFactoryTest
{
    [Fact]
    public void CreateCardSetEntity()
    {
        int year = 1990;
        string name = "Topps";

        var factory = new CardSetFactory();
        var set = factory.CreateSet(year, name);
        
        Assert.NotNull(set);
        Assert.Equal(year, set.Year);
        Assert.Equal(name, set.Name);
    }
}