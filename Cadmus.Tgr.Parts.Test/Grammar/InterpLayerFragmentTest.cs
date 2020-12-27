using Cadmus.Core;
using Cadmus.Seed.Tgr.Parts.Grammar;
using Cadmus.Tgr.Parts.Grammar;
using Fusi.Tools.Config;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace Cadmus.Tgr.Parts.Test.Grammar
{
    public sealed class InterpLayerFragmentTest
    {
        private static InterpLayerFragment GetFragment()
        {
            var seeder = new InterpLayerFragmentSeeder();
            return (InterpLayerFragment)
                seeder.GetFragment(null, "1.2", "exemplum fictum");
        }

        private static InterpLayerFragment GetEmptyFragment()
        {
            InterpLayerFragment fr = new InterpLayerFragment
            {
                Location = "1.23",
            };
            for (int n = 1; n <= 3; n++)
            {
                bool even = n % 2 == 0;
                fr.Entries.Add(new InterpEntry
                {
                    Languages = new[] { even ? "grc" : "lat" },
                    Value = $"value-{n}",
                    Tag = even? "even" : "odd"
                });
            }
            return fr;
        }

        [Fact]
        public void Fragment_Has_Tag()
        {
            TagAttribute attr = typeof(InterpLayerFragment).GetTypeInfo()
                .GetCustomAttribute<TagAttribute>();
            string typeId = attr != null ? attr.Tag : GetType().FullName;
            Assert.NotNull(typeId);
            Assert.StartsWith(PartBase.FR_PREFIX, typeId);
        }

        [Fact]
        public void Fragment_Is_Serializable()
        {
            InterpLayerFragment fragment = GetFragment();

            string json = TestHelper.SerializeFragment(fragment);
            InterpLayerFragment fragment2 =
                TestHelper.DeserializeFragment<InterpLayerFragment>(json);

            Assert.Equal(fragment.Location, fragment2.Location);
            Assert.NotEmpty(fragment.Entries);
        }

        [Fact]
        public void GetDataPins_Tag_1()
        {
            InterpLayerFragment fragment = GetEmptyFragment();
            List<DataPin> pins = fragment.GetDataPins(null).ToList();

            Assert.Equal(6, pins.Count);

            // fr-tot-count
            DataPin pin = pins.Find(p => p.Name == PartBase.FR_PREFIX + "tot-count");
            Assert.NotNull(pin);
            Assert.Equal("3", pin.Value);

            // fr-language
            pin = pins.Find(p => p.Name == PartBase.FR_PREFIX + "language"
               && p.Value == "lat");
            Assert.NotNull(pin);

            pin = pins.Find(p => p.Name == PartBase.FR_PREFIX + "language"
               && p.Value == "grc");
            Assert.NotNull(pin);

            // fr-value
            pin = pins.Find(p => p.Name == PartBase.FR_PREFIX + "value"
               && p.Value == "value");
            Assert.NotNull(pin);

            // fr-tag
            pin = pins.Find(p => p.Name == PartBase.FR_PREFIX + "tag"
               && p.Value == "odd");
            Assert.NotNull(pin);

            pin = pins.Find(p => p.Name == PartBase.FR_PREFIX + "tag"
               && p.Value == "even");
            Assert.NotNull(pin);
        }
    }
}
