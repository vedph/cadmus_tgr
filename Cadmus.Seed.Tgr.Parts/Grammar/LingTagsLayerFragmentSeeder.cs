using Bogus;
using Cadmus.Core;
using Cadmus.Core.Config;
using Cadmus.Core.Layers;
using Cadmus.Tgr.Parts.Grammar;
using Fusi.Tools.Config;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Tgr.Parts.Grammar
{
    /// <summary>
    /// Seeder for <see cref="LingTagsLayerFragment"/>'s.
    /// Tag: <c>seed.fr.it.vedph.tgr.ling-tags</c>.
    /// </summary>
    /// <seealso cref="FragmentSeederBase" />
    /// <seealso cref="IConfigurable{LingTagsLayerFragmentSeederOptions}" />
    [Tag("seed.fr.it.vedph.tgr.ling-tags")]
    public sealed class LingTagsLayerFragmentSeeder : FragmentSeederBase,
        IConfigurable<LingTagsLayerFragmentSeederOptions>
    {
        private LingTagsLayerFragmentSeederOptions? _options;

        /// <summary>
        /// Gets the type of the fragment.
        /// </summary>
        /// <returns>Type.</returns>
        public override Type GetFragmentType() => typeof(LingTagsLayerFragment);

        /// <summary>
        /// Configures the object with the specified options.
        /// </summary>
        /// <param name="options">The options.</param>
        public void Configure(LingTagsLayerFragmentSeederOptions options)
        {
            _options = options;
        }

        private LingTaggedForm GetForm()
        {
            LingTaggedForm form = new Faker<LingTaggedForm>()
                .RuleFor(f => f.Lemmata,
                    f => new List<string>(new[] { f.Lorem.Word() }))
                .RuleFor(f => f.IsDubious, f => f.Random.Bool(0.2F))
                .RuleFor(f => f.Note,
                    f => f.Random.Bool(0.2F)? f.Lorem.Sentence() : null)
                .Generate();

            if (_options?.Entries?.Count > 0)
            {
                for (int i = 0; i < Randomizer.Seed.Next(1, 3 + 1); i++)
                {
                    form.Tags.Add(new Faker<AnnotatedTag>()
                        .RuleFor(t => t.Value,
                            f => f.PickRandom(_options.Entries).Id)
                        .Generate());
                }
            }

            return form;
        }

        private List<LingTaggedForm> GetForms(int count)
        {
            List<LingTaggedForm> forms = new();
            for (int i = 0; i < count; i++)
                forms.Add(GetForm());
            return forms;
        }

        /// <summary>
        /// Creates and seeds a new part.
        /// </summary>
        /// <param name="item">The item this part should belong to.</param>
        /// <param name="location">The location.</param>
        /// <param name="baseText">The base text.</param>
        /// <returns>A new fragment.</returns>
        /// <exception cref="ArgumentNullException">location or
        /// baseText</exception>
        public override ITextLayerFragment GetFragment(
            IItem item, string location, string baseText)
        {
            if (location == null)
                throw new ArgumentNullException(nameof(location));
            if (baseText == null)
                throw new ArgumentNullException(nameof(baseText));

            return new Faker<LingTagsLayerFragment>()
                .RuleFor(fr => fr.Location, location)
                .RuleFor(fr => fr.Forms, GetForms(3))
                .Generate();
        }
    }

    /// <summary>
    /// Options for <see cref="LingTagsLayerFragmentSeeder"/>.
    /// </summary>
    public sealed class LingTagsLayerFragmentSeederOptions
    {
        public IList<ThesaurusEntry>? Entries { get; set; }
    }
}
