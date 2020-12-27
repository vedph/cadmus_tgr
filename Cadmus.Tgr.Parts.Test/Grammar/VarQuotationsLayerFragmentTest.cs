using Cadmus.Core;
using Cadmus.Seed.Tgr.Parts.Grammar;
using Cadmus.Tgr.Parts.Grammar;
using Fusi.Tools.Config;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Cadmus.Tgr.Parts.Test.Grammar
{
    public sealed class VarQuotationsLayerFragmentTest
    {
        private static VarQuotationsLayerFragment GetFragment()
        {
            var seeder = new VarQuotationsLayerFragmentSeeder();
            return (VarQuotationsLayerFragment)
                seeder.GetFragment(null, "1.2", "exemplum fictum");
        }

        private static VarQuotationsLayerFragment GetEmptyFragment()
        {
            VarQuotationsLayerFragment fr = new VarQuotationsLayerFragment();

            for (int n = 1; n <= 3; n++)
            {
                bool even = n % 2 == 0;
                fr.Entries.Add(new VarQuotationEntry
                {
                    Tag = even? "even" : "odd",
                    Authority = even? "gram" : "ling",
                    Work = $"work-{n}",
                    Variants = new List<QuotationVariant>(new[]
                    {
                        new QuotationVariant
                        {
                            Lemma = $"lemma{n}",
                            Value = $"value{n}"
                        }
                    })
                });
            }

            return fr;
        }

        [Fact]
        public void Fragment_Has_Tag()
        {
            TagAttribute attr = typeof(VarQuotationsLayerFragment).GetTypeInfo()
                .GetCustomAttribute<TagAttribute>();
            string typeId = attr != null ? attr.Tag : GetType().FullName;
            Assert.NotNull(typeId);
            Assert.StartsWith(PartBase.FR_PREFIX, typeId);
        }

        [Fact]
        public void Fragment_Is_Serializable()
        {
            VarQuotationsLayerFragment fragment = GetFragment();

            string json = TestHelper.SerializeFragment(fragment);
            VarQuotationsLayerFragment fragment2 =
                TestHelper.DeserializeFragment<VarQuotationsLayerFragment>(json);

            Assert.Equal(fragment.Location, fragment2.Location);
            Assert.NotEmpty(fragment.Entries);
        }

        [Fact]
        public void GetDataPins_Tag_1()
        {
            VarQuotationsLayerFragment fragment = GetEmptyFragment();

            List<DataPin> pins = fragment.GetDataPins(null).ToList();

            Assert.Equal(10, pins.Count);

            // fr-tot-count
            DataPin pin = pins.Find(p => p.Name == PartBase.FR_PREFIX + "tot-count");
            Assert.NotNull(pin);
            Assert.Equal("3", pin.Value);

            // fr-tag
            pin = pins.Find(p => p.Name == PartBase.FR_PREFIX + "tag"
                && p.Value == "odd");
            Assert.NotNull(pin);

            pin = pins.Find(p => p.Name == PartBase.FR_PREFIX + "tag"
                && p.Value == "even");
            Assert.NotNull(pin);

            // fr-authority
            pin = pins.Find(p => p.Name == PartBase.FR_PREFIX + "authority"
                && p.Value == "gram");
            Assert.NotNull(pin);

            pin = pins.Find(p => p.Name == PartBase.FR_PREFIX + "authority"
                && p.Value == "ling");
            Assert.NotNull(pin);

            // fr-work
            for (int n = 1; n <= 3; n++)
            {
                pin = pins.Find(p => p.Name == PartBase.FR_PREFIX + "work"
                    && p.Value == $"work-{n}");
                Assert.NotNull(pin);
            }

            // fr-var-lemma
            pin = pins.Find(p => p.Name == PartBase.FR_PREFIX + "var-lemma"
                && p.Value == "lemma");
            Assert.NotNull(pin);

            // fr-var-value
            pin = pins.Find(p => p.Name == PartBase.FR_PREFIX + "var-value"
                && p.Value == "value");
            Assert.NotNull(pin);
        }
    }
}
