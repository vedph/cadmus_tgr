using System;
using Cadmus.Core;
using Cadmus.Core.Layers;
using Fusi.Tools.Config;
using Cadmus.Tgr.Parts.Grammar;
using Xunit;
using Cadmus.Seed.Tgr.Parts.Grammar;
using System.Reflection;
using System.Text.Json;
using Cadmus.Core.Config;

namespace Cadmus.Seed.Tgr.Parts.Test.Grammar
{
    public sealed class LingTagsLayerFragmentSeederTest
    {
        private static readonly PartSeederFactory _factory;
        private static readonly SeedOptions _seedOptions;
        private static readonly IItem _item;

        static LingTagsLayerFragmentSeederTest()
        {
            _factory = TestHelper.GetFactory();
            _seedOptions = _factory.GetSeedOptions();
            _item = _factory.GetItemSeeder().GetItem(1, "facet");
        }

        [Fact]
        public void TypeHasTagAttribute()
        {
            Type t = typeof(LingTagsLayerFragmentSeeder);
            TagAttribute? attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
            Assert.NotNull(attr);
            Assert.Equal("seed.fr.it.vedph.tgr.ling-tags", attr.Tag);
        }

        [Fact]
        public void GetFragmentType_Ok()
        {
            LingTagsLayerFragmentSeeder seeder = new();
            Assert.Equal(typeof(LingTagsLayerFragment), seeder.GetFragmentType());
        }

        private static ThesaurusEntry[] LoadThesaurusEntries()
        {
            return JsonSerializer.Deserialize<ThesaurusEntry[]>(
                TestHelper.LoadResourceText("TagEntries.json"),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })!;
        }

        [Fact]
        public void Seed_WithOptions_Ok()
        {
            LingTagsLayerFragmentSeeder seeder = new();
            seeder.SetSeedOptions(_seedOptions);
            seeder.Configure(new LingTagsLayerFragmentSeederOptions
            {
                Entries = LoadThesaurusEntries()
            });

            ITextLayerFragment? fragment = seeder.GetFragment(_item, "1.1", "alpha");

            Assert.NotNull(fragment);

            LingTagsLayerFragment? fr = fragment as LingTagsLayerFragment;
            Assert.NotNull(fr);

            Assert.Equal("1.1", fr.Location);
            Assert.Equal(3, fr.Forms.Count);
        }
    }
}
