using Amazon.DynamoDBv2.DataModel;

namespace LambdaWebApi.Models
{
    [DynamoDBTable("sportTeams")]
    public class SportsTeam
    {
        [DynamoDBHashKey("sportType")]
        public string SportType { get; set; }

        [DynamoDBRangeKey("teamName")]
        public string TeamName { get; set; }
    }
}
