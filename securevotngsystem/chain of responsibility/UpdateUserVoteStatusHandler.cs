using System.Data.SqlClient;

namespace securevotngsystem
{
    public class UpdateUserVoteStatusHandler : VoteHandler
    {
        public override void Handle(int userId, int candidateId, string electionType)
        {
            var db = DbManager.Instance;

            string columnToUpdate = electionType == "Local" ? "HasVotedLocal" : "HasVotedGeneral";
            var cmd = new SqlCommand($"UPDATE Users SET {columnToUpdate} = 1 WHERE UserId = @userId", db.Connection);
            cmd.Parameters.AddWithValue("@userId", userId);

            db.Connection.Open();
            cmd.ExecuteNonQuery();
            db.Connection.Close();

            _next?.Handle(userId, candidateId, electionType);
        }
    }
}
