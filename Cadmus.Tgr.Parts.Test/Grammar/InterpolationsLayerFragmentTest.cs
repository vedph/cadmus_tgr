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
    public sealed class InterpolationsLayerFragmentTest
    {
        private static InterpolationsLayerFragment GetFragment()
        {
            var seeder = new InterpolationsLayerFragmentSeeder();
            return (InterpolationsLayerFragment)
                seeder.GetFragment(new Item(), "1.2", "exemplum fictum")!;
        }

        private static InterpolationsLayerFragment GetEmptyFragment()
        {
            InterpolationsLayerFragment fr = new()
            {
                Location = "1.23",
            };
            for (int n = 1; n <= 3; n++)
            {
                bool even = n % 2 == 0;
                fr.Interpolations.Add(new Interpolation
                {
                    Languages = new[] { even ? "grc" : "lat" },
                    Value = $"value-{n}",
                    Tag = even? "even" : "odd",
                    Role = even? "paleo" : "gloss"
                });
            }
            return fr;
        }

        [Fact]
        public void Fragment_Has_Tag()
        {
            TagAttribute? attr = typeof(InterpolationsLayerFragment).GetTypeInfo()
                .GetCustomAttribute<TagAttribute>();
            string? typeId = attr != null ? attr.Tag : GetType().FullName;
            Assert.NotNull(typeId);
            Assert.StartsWith(PartBase.FR_PREFIX, typeId);
        }

        [Fact]
        public void Fragment_Is_Serializable()
        {
            InterpolationsLayerFragment fragment = GetFragment();

            string json = TestHelper.SerializeFragment(fragment);
            InterpolationsLayerFragment fragment2 =
                TestHelper.DeserializeFragment<InterpolationsLayerFragment>(json);

            Assert.Equal(fragment.Location, fragment2.Location);
            Assert.NotEmpty(fragment.Interpolations);
        }

        [Fact]
        public void GetDataPins_Tag_1()
        {
            InterpolationsLayerFragment fragment = GetEmptyFragment();
            List<DataPin> pins = fragment.GetDataPins(null).ToList();

            Assert.Equal(8, pins.Count);

            // fr-tot-count
            DataPin? pin = pins.Find(p => p.Name == PartBase.FR_PREFIX + "tot-count");
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

            // fr-role
            pin = pins.Find(p => p.Name == PartBase.FR_PREFIX + "role"
               && p.Value == "paleo");
            Assert.NotNull(pin);

            pin = pins.Find(p => p.Name == PartBase.FR_PREFIX + "role"
               && p.Value == "gloss");
            Assert.NotNull(pin);
        }
    }
}
