namespace Microsoft.Azure.Commands.Sql.Database_Backup.Model
{
    public class AzureSqlDatabaseShortTermRetentionPolicyModel
    {
        /// <summary>
        /// Gets or sets the name of the resource group
        /// </summary>
        public string ResourceGroupName { get; set; }

        /// <summary>
        /// Gets or sets the name of the server
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the name of the database
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// Gets or sets the long term retention policy of the database
        /// </summary>
        public Management.Sql.Models.ShortTermRetentionPolicy Policy { get; set; }
    }
}