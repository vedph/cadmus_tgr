using Cadmus.Core;
using Cadmus.Core.Layers;
using Cadmus.Seed.Tgr.Parts.Grammar;
using Cadmus.Tgr.Parts.Grammar;
using Fusi.Tools.Configuration;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Tgr.Parts.Test.Grammar;

public sealed class InterpLayerFragmentSeederTest
{
    private static readonly PartSeederFactory _factory;

    static InterpLayerFragmentSeederTest()
    {
        _factory = TestHelper.GetFactory();
    }

    [Fact]
    public void TypeHasTagAttribute()
    {
        Type t = typeof(InterpolationsLayerFragmentSeeder);
        TagAttribute? attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
        Assert.NotNull(attr);
        Assert.Equal("seed.fr.it.vedph.tgr.interpolations", attr.Tag);
    }

    [Fact]
    public void GetFragmentType_Ok()
    {
        InterpolationsLayerFragmentSeeder seeder = new();
        Assert.Equal(typeof(InterpolationsLayerFragment), seeder.GetFragmentType());
    }

    [Fact]
    public void Seed_Ok()
    {
        InterpolationsLayerFragmentSeeder seeder = new();

        ITextLayerFragment? fragment = seeder.GetFragment(new Item(), "1.1", "alpha");

        Assert.NotNull(fragment);

        InterpolationsLayerFragment? fr = fragment as InterpolationsLayerFragment;
        Assert.NotNull(fr);

        Assert.Equal("1.1", fr.Location);
        Assert.NotEmpty(fr.Interpolations);
    }
}
