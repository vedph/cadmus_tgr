using Cadmus.Core;
using Cadmus.Seed.Tgr.Parts.Grammar;
using Cadmus.Tgr.Parts.Grammar;
using Fusi.Tools.Config;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Tgr.Parts.Test.Grammar
{
    public sealed class AvailableWitnessesPartSeederTest
    {
        private static readonly PartSeederFactory _factory;
        private static readonly SeedOptions _seedOptions;
        private static readonly IItem _item;

        static AvailableWitnessesPartSeederTest()
        {
            _factory = TestHelper.GetFactory();
            _seedOptions = _factory.GetSeedOptions();
            _item = _factory.GetItemSeeder().GetItem(1, "facet");
        }

        [Fact]
        public void TypeHasTagAttribute()
        {
            Type t = typeof(AvailableWitnessesPartSeeder);
            TagAttribute? attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
            Assert.NotNull(attr);
            Assert.Equal("seed.it.vedph.tgr.available-witnesses", attr.Tag);
        }

        [Fact]
        public void Seed_Ok()
        {
            AvailableWitnessesPartSeeder seeder = new();
            seeder.SetSeedOptions(_seedOptions);

            IPart? part = seeder.GetPart(_item, null, _factory);

            Assert.NotNull(part);

            AvailableWitnessesPart? p = part as AvailableWitnessesPart;
            Assert.NotNull(p);

            TestHelper.AssertPartMetadata(p);

            Assert.NotEmpty(p.Witnesses);
        }
    }
}
