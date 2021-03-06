﻿using System;
using System.Reflection;
using Cadmus.Core;
using Cadmus.Core.Config;
using Cadmus.Core.Storage;
using Cadmus.Itinera.Parts.Codicology;
using Cadmus.Mongo;
using Cadmus.Parts.General;
using Cadmus.Philology.Parts.Layers;
using Cadmus.Tgr.Parts.Grammar;
using Microsoft.Extensions.Configuration;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Cadmus.Tgr.Services
{
    /// <summary>
    /// Cadmus Itinera repository provider.
    /// </summary>
    public sealed class TgrRepositoryProvider : IRepositoryProvider
    {
        private readonly IConfiguration _configuration;
        private readonly TagAttributeToTypeMap _map;
        private readonly IPartTypeProvider _partTypeProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="StandardRepositoryProvider"/>
        /// class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <exception cref="ArgumentNullException">configuration</exception>
        public TgrRepositoryProvider(IConfiguration configuration)
        {
            _configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));

            _map = new TagAttributeToTypeMap();
            _map.Add(new[]
            {
                // Cadmus.Parts
                typeof(NotePart).GetTypeInfo().Assembly,
                // Cadmus.Philology.Parts
                typeof(ApparatusLayerFragment).GetTypeInfo().Assembly,
                // Cadmus.Itinera.Parts
                typeof(MsSignaturesPart).GetTypeInfo().Assembly,
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
        /// Creates a Cadmus repository.
        /// </summary>
        /// <param name="database">The database name.</param>
        /// <returns>repository</returns>
        /// <exception cref="ArgumentNullException">null database</exception>
        public ICadmusRepository CreateRepository(string database)
        {
            if (database == null)
                throw new ArgumentNullException(nameof(database));

            // create the repository (no need to use container here)
            MongoCadmusRepository repository =
                new MongoCadmusRepository(
                    _partTypeProvider,
                    new StandardItemSortKeyBuilder());

            repository.Configure(new MongoCadmusRepositoryOptions
            {
                ConnectionString = string.Format(
                    _configuration.GetConnectionString("Default"), database)
            });

            return repository;
        }
    }
}
