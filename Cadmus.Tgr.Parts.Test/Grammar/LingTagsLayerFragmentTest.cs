using Cadmus.Core;
using Cadmus.Tgr.Parts.Grammar;
using Fusi.Tools.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Cadmus.Tgr.Parts.Test.Grammar;

public sealed class LingTagsLayerFragmentTest
{
    private static LingTagsLayerFragment GetFragment()
    {
        LingTagsLayerFragment fragment = new()
        {
            Location = "1.23",
        };

        for (int i = 0; i < 3; i++)
        {
            fragment.Forms.Add(new LingTaggedForm
            {
                IsDubious = i == 1,
                Lemmata = new List<string>(
                    new[] { $"lemma-{(char)('A' + i)}" }),
                Note = "note",
                Tags = new List<AnnotatedTag>(new[]
                {
                    new AnnotatedTag
                    {
                        Value = $"tag-{i+1}",
                        Notes = new List<TaggedNote>(new[]
                        {
                            new TaggedNote
                            {
                                Tag = "tag",
                                Note = "note"
                            }
                        })
                    }
                })
            });
        }

        return fragment;
    }

    [Fact]
    public void Fragment_Has_Tag()
    {
        TagAttribute? attr = typeof(LingTagsLayerFragment).GetTypeInfo()
            .GetCustomAttribute<TagAttribute>();
        string? typeId = attr != null ? attr.Tag : GetType().FullName;
        Assert.NotNull(typeId);
        Assert.StartsWith(PartBase.FR_PREFIX, typeId);
    }

    [Fact]
    public void Fragment_Is_Serializable()
    {
        LingTagsLayerFragment fragment = GetFragment();

        string json = TestHelper.SerializeFragment(fragment);
        LingTagsLayerFragment fragment2 =
            TestHelper.DeserializeFragment<LingTagsLayerFragment>(json);

        Assert.Equal(fragment.Location, fragment2.Location);
        Assert.Equal(3, fragment.Forms.Count);
    }

    [Fact]
    public void GetDataPins_Ok()
    {
        LingTagsLayerFragment fragment = GetFragment();

        List<DataPin> pins = fragment.GetDataPins(null).ToList();
        Assert.Equal(7, pins.Count);

        // fr.tot-count
        DataPin? pin = pins.Find(p => p.Name == "fr.tot-count");
        Assert.NotNull(pin);
        Assert.Equal("3", pin.Value);

        // fr.lemma and fr.tag
        for (int i = 0; i < 3; i++)
        {
            pin = pins.Find(p => p.Name == "fr.lemma"
                && p.Value == $"lemma{(char)('a' + i)}");
            Assert.NotNull(pin);

            pin = pins.Find(p => p.Name == "fr.tag"
                && p.Value == $"tag-{i + 1}");
            Assert.NotNull(pin);
        }
    }
}
