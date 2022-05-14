using Xunit;
using CardShow.Data.Factories;
using CardShow.Shared.Enums;

namespace CardShow.Test.Data;

public class CardSetFactoryTest
{
    [Fact]
    public void CreateCardSetEntity()
    {
        int year = 1990;
        string name = "Topps";
        Sport sport = Sport.Baseball;

        var factory = new CardSetFactory();
        var set = factory.CreateSet(year, name, (int)sport);
        
        Assert.NotNull(set);
        Assert.Equal(year, set.Year);
        Assert.Equal(name, set.Name);
        Assert.Equal((int)Sport.Baseball, set.Sport);
    }
}