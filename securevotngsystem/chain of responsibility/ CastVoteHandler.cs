using System.Data.SqlClient;

namespace securevotngsystem
{
    public class CastVoteHandler : VoteHandler
    {
        public override void Handle(int userId, int candidateId, string electionType)
        {
            var db = DbManager.Instance;

            db.Connection.Open();
            var cmd = new SqlCommand(
                "INSERT INTO Votes (UserId, CandidateId, ElectionType) VALUES (@userId, @candidateId, @type)", db.Connection);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@candidateId", candidateId);
            cmd.Parameters.AddWithValue("@type", electionType);
            cmd.ExecuteNonQuery();
            db.Connection.Close();

            _next?.Handle(userId, candidateId, electionType);
        }
    }
}
