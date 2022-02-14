using Cadmus.Cli.Core;
using Cadmus.Core;
using Cadmus.Core.Config;
using Cadmus.Core.Storage;
using Cadmus.Mongo;
using Cadmus.Parts.General;
using Cadmus.Philology.Parts;
using Cadmus.Tgr.Parts.Grammar;
using Fusi.Tools.Config;
using System.Reflection;

namespace Cadmus.Cli.Plugin.Tgr
{
    /// <summary>
    /// CLI Cadmus repository provider for Tgr.
    /// Tag: <c>cli-repository-provider.tgr</c>.
    /// </summary>
    /// <seealso cref="ICliCadmusRepositoryProvider" />
    [Tag("cli-repository-provider.tgr")]
    public sealed class TgrCliCadmusRepositoryProvider :
        ICliCadmusRepositoryProvider
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
        /// Creates the repository.
        /// </summary>
        /// <param name="database">The database.</param>
        /// <returns>Repository.</returns>
        /// <exception cref="ArgumentNullException">database</exception>
        public ICadmusRepository CreateRepository(string database)
        {
            if (database == null)
                throw new ArgumentNullException(nameof(database));

            // create the repository (no need to use container here)
            MongoCadmusRepository repository =
                new(_partTypeProvider, new StandardItemSortKeyBuilder());

            repository.Configure(new MongoCadmusRepositoryOptions
            {
                ConnectionString = string.Format(ConnectionString!, database)
            });

            return repository;
        }
    }
}
