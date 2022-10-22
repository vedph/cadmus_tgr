﻿using Cadmus.Core;
using Cadmus.Core.Config;
using Cadmus.Core.Storage;
using Cadmus.General.Parts;
using Cadmus.Mongo;
using Cadmus.Philology.Parts;
using Cadmus.Tgr.Parts.Grammar;
using Fusi.Tools.Config;
using System.Reflection;

namespace Cadmus.Cli.Plugin.Tgr
{
    /// <summary>
    /// Cadmus repository provider for Tgr.
    /// Tag: <c>repository-provider.tgr</c>.
    /// </summary>
    /// <seealso cref="IRepositoryProvider" />
    [Tag("repository-provider.tgr")]
    public sealed class TgrCliCadmusRepositoryProvider :
        IRepositoryProvider
    {
        private readonly IPartTypeProvider _partTypeProvider;

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        public string? ConnectionString { get; set; }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="TgrCliCadmusRepositoryProvider"/> class.
        /// </summary>
        public TgrCliCadmusRepositoryProvider()
        {
            TagAttributeToTypeMap _map = new();
            _map.Add(new[]
            {
                // Cadmus.General.Parts
                typeof(NotePart).GetTypeInfo().Assembly,
                // Cadmus.Philology.Parts
                typeof(ApparatusLayerFragment).GetTypeInfo().Assembly,
                // Cadmus.Tgr.Parts
                typeof(LingTagsLayerFragment).GetTypeInfo().Assembly
            });

            _partTypeProvider = new StandardPartTypeProvider(_map);
        }

        /// <summary>
        /// Gets the part type provider.
        /// </summary>
        /// <returns>part type provider</returns>
        public IPartTypeProvider GetPartTypeProvider()
        {
            return _partTypeProvider;
        }

        /// <summary>
        /// Creates the repository.
        /// </summary>
        /// <returns>Repository.</returns>
        /// <exception cref="ArgumentNullException">database</exception>
        public ICadmusRepository CreateRepository()
        {
            // create the repository (no need to use container here)
            MongoCadmusRepository repository =
                new(_partTypeProvider, new StandardItemSortKeyBuilder());

            repository.Configure(new MongoCadmusRepositoryOptions
            {
                ConnectionString = ConnectionString ??
                    throw new InvalidOperationException(
                    "No connection string set for IRepositoryProvider implementation")
            });

            return repository;
        }
    }
}
