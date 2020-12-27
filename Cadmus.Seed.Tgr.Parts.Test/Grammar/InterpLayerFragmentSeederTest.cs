using Cadmus.Core.Layers;
using Cadmus.Seed.Tgr.Parts.Grammar;
using Cadmus.Tgr.Parts.Grammar;
using Fusi.Tools.Config;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Tgr.Parts.Test.Grammar
{
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
            Type t = typeof(InterpLayerFragmentSeeder);
            TagAttribute attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
            Assert.NotNull(attr);
            Assert.Equal("seed.fr.it.vedph.tgr.interp", attr.Tag);
        }

        [Fact]
        public void GetFragmentType_Ok()
        {
            InterpLayerFragmentSeeder seeder = new InterpLayerFragmentSeeder();
            Assert.Equal(typeof(InterpLayerFragment), seeder.GetFragmentType());
        }

        [Fact]
        public void Seed_Ok()
        {
            InterpLayerFragmentSeeder seeder = new InterpLayerFragmentSeeder();

            ITextLayerFragment fragment = seeder.GetFragment(null, "1.1", "alpha");

            Assert.NotNull(fragment);

            InterpLayerFragment fr = fragment as InterpLayerFragment;
            Assert.NotNull(fr);

            Assert.Equal("1.1", fr.Location);
            Assert.NotEmpty(fr.Entries);
        }
    }
}
