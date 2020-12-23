using LiteDB;
using Microsoft.Extensions.Options;
using NetworkScanner.Domain.Entities;

namespace NetworkScanner.Infrastructure.Data
{
    public class NetworkContext
    {
        protected string DbName { get; }
        protected string CollectionName { get; }
        public ILiteDatabase Database { get; set; }

        public NetworkContext(IOptions<LiteDbOptions> options)
        {
            var filePath = options.Value.DatabaseLocation.Replace(@"\\", @"\");
            var connection = options.Value.Connection;
            DbName = $"Filename={filePath};Connection={connection}";
            Database = new LiteDatabase(DbName);
            CollectionName = "Device";
        }
    }
}