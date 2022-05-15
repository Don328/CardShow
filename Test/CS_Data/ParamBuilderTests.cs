using Xunit;
using CardShow.Data.Factories;
using CardShow.Shared.Enums;
using Microsoft.Data.Sqlite;
using CardShow.Data.Sqlite;
using System;

namespace CardShow.Test.Data;

public class ParamBuilderTests : IDisposable
{
    SqliteCommand cmd;

    public ParamBuilderTests()
    {
        cmd = new SqliteCommand();
    }


    [Fact]
    public void Test_Build()
    {
        ParamBuilder.Build(cmd, "@id", 4);
        Assert.True(cmd.Parameters
            .Contains("@id"));

    }

    [Fact]
    public void Test_NullArgument()
    {
        Assert.Throws<ArgumentNullException>(
            () => ParamBuilder.Build(
                cmd, "@id", null));
    }

    [Fact]
    public void Test_UnhandledType()
    {
        var ex = Assert.Throws<ArgumentException>(
            () => ParamBuilder.Build(
                cmd, "@id", new object()));

        Assert.Equal($"Unrecognized Type: {typeof(object)}", ex.Message);
    }

    public void Dispose()
    {
        if (cmd != null)
        {
            cmd.Dispose();
        }    
    }
}