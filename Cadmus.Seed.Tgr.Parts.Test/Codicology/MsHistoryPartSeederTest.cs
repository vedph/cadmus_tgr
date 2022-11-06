using Cadmus.Core;
using Cadmus.Seed.Tgr.Parts.Codicology;
using Cadmus.Tgr.Parts.Codicology;
using Fusi.Tools.Config;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Tgr.Parts.Test.Codicology
{
    public sealed class MsHistoryPartSeederTest
    {
        private static readonly PartSeederFactory _factory;
        private static readonly SeedOptions _seedOptions;
        private static readonly IItem _item;

        static MsHistoryPartSeederTest()
        {
            _factory = TestHelper.GetFactory();
            _seedOptions = _factory.GetSeedOptions();
            _item = _factory.GetItemSeeder().GetItem(1, "facet");
        }

        [Fact]
        public void TypeHasTagAttribute()
        {
            Type t = typeof(MsHistoryPartSeeder);
            TagAttribute? attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
            Assert.NotNull(attr);
            Assert.Equal("seed.it.vedph.tgr.ms-history", attr.Tag);
        }

        [Fact]
        public void Seed_Ok()
        {
            MsHistoryPartSeeder seeder = new();
            seeder.SetSeedOptions(_seedOptions);

            IPart? part = seeder.GetPart(_item, null, _factory);

            Assert.NotNull(part);

            MsHistoryPart? p = part as MsHistoryPart;
            Assert.NotNull(p);

            TestHelper.AssertPartMetadata(p);

            Assert.NotEmpty(p.Provenances);
            Assert.NotNull(p.History);
        }
    }
}
