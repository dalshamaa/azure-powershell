using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Azure.Commands.Sql.Database_Backup.Model;

namespace Microsoft.Azure.Commands.Sql.Database_Backup.Cmdlet
{
    [Cmdlet(VerbsCommon.Set, "AzureRmSqlDatabaseShortTermRetentionPolicy",
    DefaultParameterSetName = PolicyByResourceServerDatabaseSet,
    SupportsShouldProcess = true,
    ConfirmImpact = ConfirmImpact.Low),
    OutputType(typeof(AzureSqlDatabaseShortTermRetentionPolicyModel))]
    public class SetAzureSqlDatabaseShortTermRetentionPolicy : AzureSqlDatabaseShortTermRetentionPolicyCmdletBase
    {
        /// <summary>
        /// Gets or sets the name of the database to use.
        /// </summary>
        [Parameter(ParameterSetName = PolicyByResourceServerDatabaseSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "The name of the Azure SQL Database to use.")]
        [ValidateNotNullOrEmpty]
        [ValidateRange(7,35)]
        public int? RetentionDays{ get; set; }
        /// <summary>
        /// Get the entities from the service
        /// </summary>
        /// <returns>The list of entities</returns>
        protected override IEnumerable<AzureSqlDatabaseShortTermRetentionPolicyModel> GetEntity()
        {
            ICollection<AzureSqlDatabaseShortTermRetentionPolicyModel> results = new List<AzureSqlDatabaseShortTermRetentionPolicyModel>()
            {
                ModelAdapter.GetDatabaseBackupShortTermRetentionPolicy(
                    this.ResourceGroupName,
                    this.ServerName,
                    this.DatabaseName)
            };

            return results;
        }

        /// <summary>
        /// Create the model from user input
        /// </summary>
        /// <param name="model">Model retrieved from service</param>
        /// <returns>The model that was passed in</returns>
        protected override IEnumerable<AzureSqlDatabaseShortTermRetentionPolicyModel> ApplyUserInputToModel(IEnumerable<AzureSqlDatabaseShortTermRetentionPolicyModel> model)
        {
            return new List<AzureSqlDatabaseShortTermRetentionPolicyModel>()
            {
                new AzureSqlDatabaseShortTermRetentionPolicyModel()
                {
                    ResourceGroupName = ResourceGroupName,
                    ServerName = ServerName,
                    DatabaseName = DatabaseName,
                    Policy = new Management.Sql.Models.ShortTermRetentionPolicy(retentionDays: RetentionDays)
                }
            };
        }

        /// <summary>
        /// Update the entity
        /// </summary>
        /// <param name="entity">The output of apply user input to model</param>
        /// <returns>The input entity</returns>
        protected override IEnumerable<AzureSqlDatabaseShortTermRetentionPolicyModel> PersistChanges(IEnumerable<AzureSqlDatabaseShortTermRetentionPolicyModel> entity)
        {
            if (ShouldProcess(DatabaseName))
            {
                return new List<AzureSqlDatabaseShortTermRetentionPolicyModel>() {
                    ModelAdapter.SetDatabaseShortTermRetentionPolicy(this.ResourceGroupName, this.ServerName, this.DatabaseName, entity.First())
                };
            }
            else
            {
                return null;
            }
        }
    }
}

