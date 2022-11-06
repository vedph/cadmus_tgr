using Cadmus.Core;
using Cadmus.Core.Layers;
using Cadmus.Seed.Tgr.Parts.Grammar;
using Cadmus.Tgr.Parts.Grammar;
using Fusi.Tools.Config;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Tgr.Parts.Test.Grammar
{
    public sealed class VarQuotationsLayerFragmentSeederTest
    {
        private static readonly PartSeederFactory _factory;
        private static readonly IItem _item;

        static VarQuotationsLayerFragmentSeederTest()
        {
            _factory = TestHelper.GetFactory();
            _item = _factory.GetItemSeeder().GetItem(1, "facet");
        }

        [Fact]
        public void TypeHasTagAttribute()
        {
            Type t = typeof(VarQuotationsLayerFragmentSeeder);
            TagAttribute? attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
            Assert.NotNull(attr);
            Assert.Equal("seed.fr.it.vedph.tgr.var-quotations", attr.Tag);
        }

        [Fact]
        public void GetFragmentType_Ok()
        {
            VarQuotationsLayerFragmentSeeder seeder = new();
            Assert.Equal(typeof(VarQuotationsLayerFragment), seeder.GetFragmentType());
        }

        [Fact]
        public void Seed_Ok()
        {
            VarQuotationsLayerFragmentSeeder seeder = new();

            ITextLayerFragment? fragment = seeder.GetFragment(_item, "1.1", "alpha");

            Assert.NotNull(fragment);

            VarQuotationsLayerFragment? fr = fragment as VarQuotationsLayerFragment;
            Assert.NotNull(fr);

            Assert.Equal("1.1", fr.Location);
            // TODO other assertions...
        }
    }
}
