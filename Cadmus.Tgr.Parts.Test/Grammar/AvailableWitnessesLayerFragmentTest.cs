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
    public sealed class AvailableWitnessesLayerFragmentTest
    {
        private static AvailableWitnessesLayerFragment GetFragment()
        {
            var seeder = new AvailableWitnessesLayerFragmentSeeder();
            return (AvailableWitnessesLayerFragment)
                seeder.GetFragment(null, "1.2", "exemplum fictum");
        }

        private static AvailableWitnessesLayerFragment GetEmptyFragment()
        {
            return new()
            {
                Location = "1.23",
            };
        }

        [Fact]
        public void Fragment_Has_Tag()
        {
            TagAttribute attr = typeof(InterpolationsLayerFragment).GetTypeInfo()
                .GetCustomAttribute<TagAttribute>();
            string typeId = attr != null ? attr.Tag : GetType().FullName;
            Assert.NotNull(typeId);
            Assert.StartsWith(PartBase.FR_PREFIX, typeId);
        }

        [Fact]
        public void Fragment_Is_Serializable()
        {
            AvailableWitnessesLayerFragment fragment = GetFragment();

            string json = TestHelper.SerializeFragment(fragment);
            AvailableWitnessesLayerFragment fragment2 =
                TestHelper.DeserializeFragment<AvailableWitnessesLayerFragment>(json);

            Assert.Equal(fragment.Location, fragment2.Location);
            Assert.NotEmpty(fragment.Witnesses);
        }

        [Fact]
        public void GetDataPins_Entries_Ok()
        {
            AvailableWitnessesLayerFragment fr = GetEmptyFragment();

            const string ids = "ABC";
            for (int n = 1; n <= 3; n++)
            {
                fr.Witnesses.Add(new AvailableWitness
                {
                    Id = new string(ids[n - 1], 1),
                    IsPartial = n == 2,
                    Note = "A note"
                });
            }

            List<DataPin> pins = fr.GetDataPins(null).ToList();

            Assert.Equal(4, pins.Count);

            DataPin pin = pins.Find(p => p.Name == PartBase.FR_PREFIX + "tot-count");
            Assert.NotNull(pin);
            Assert.Equal("3", pin.Value);

            string name = PartBase.FR_PREFIX + "witness";
            pin = pins.Find(p => p.Name == name && p.Value == "A");
            Assert.NotNull(pin);

            pin = pins.Find(p => p.Name == name && p.Value == "B");
            Assert.NotNull(pin);

            pin = pins.Find(p => p.Name == name && p.Value == "C");
            Assert.NotNull(pin);
        }
    }
}
