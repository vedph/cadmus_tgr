using Cadmus.Core;
using Cadmus.Seed.Tgr.Parts.Codicology;
using Cadmus.Tgr.Parts.Codicology;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Tgr.Parts.Test.Codicology
{
    public sealed class MsScriptsPartTest
    {
        private static MsScriptsPart GetPart()
        {
            MsScriptsPartSeeder seeder = new();
            IItem item = new Item
            {
                FacetId = "default",
                CreatorId = "zeus",
                UserId = "zeus",
                Description = "Test item",
                Title = "Test Item",
                SortKey = ""
            };
            return (MsScriptsPart)seeder.GetPart(item, null, null);
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            MsScriptsPart part = GetPart();

            string json = TestHelper.SerializePart(part);
            MsScriptsPart part2 =
                TestHelper.DeserializePart<MsScriptsPart>(json);

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);

            Assert.Equal(part.Scripts.Count, part2.Scripts.Count);
            // TODO: details
        }

        [Fact]
        public void GetDataPins_NoEntries_Ok()
        {
            MsScriptsPart part = GetPart();
            part.Scripts.Clear();

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Single(pins);
            DataPin pin = pins[0];
            Assert.Equal("tot-count", pin.Name);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("0", pin.Value);
        }

        [Fact]
        public void GetDataPins_Entries_Ok()
        {
            MsScriptsPart part = new()
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another"
            };
            for (int n = 1; n <= 3; n++)
            {
                bool even = n % 2 == 0;
                part.Scripts.Add(new MsScript
                {
                    Role = even ? "inferior" : "superior",
                    Languages = new List<string>(new[] { even ? "grc" : "lat" }),
                    Type = even ? "unc" : "cap",
                    Hands = new List<MsHand>(new[]
                    {
                        new MsHand
                        {
                            Id = $"h{n}"
                        }
                    })
                });
            }

            List<DataPin> pins = part.GetDataPins(null).ToList();

            Assert.Equal(10, pins.Count);

            DataPin pin = pins.Find(p => p.Name == "tot-count");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);
            Assert.Equal("3", pin.Value);

            // role
            pin = pins.Find(p => p.Name == "role" && p.Value == "superior");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "role" && p.Value == "inferior");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            // language
            pin = pins.Find(p => p.Name == "language" && p.Value == "grc");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "language" && p.Value == "lat");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            // type
            pin = pins.Find(p => p.Name == "type" && p.Value == "cap");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            pin = pins.Find(p => p.Name == "type" && p.Value == "unc");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin);

            // hand-id
            for (int n = 1; n <= 3; n++)
            {
                pin = pins.Find(p => p.Name == "hand-id" && p.Value == $"h{n}");
                Assert.NotNull(pin);
                TestHelper.AssertPinIds(part, pin);
            }
        }
    }
}
