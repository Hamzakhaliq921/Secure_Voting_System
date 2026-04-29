using System;
using System.Data.SqlClient;

namespace securevotngsystem
{
    public class CheckAlreadyVotedHandler : VoteHandler
    {
        public override void Handle(int userId, int candidateId, string electionType)
        {
            var db = DbManager.Instance;
            var cmd = new SqlCommand("SELECT HasVotedLocal, HasVotedGeneral FROM Users WHERE UserId = @userId", db.Connection);
            cmd.Parameters.AddWithValue("@userId", userId);

            db.Connection.Open();
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    bool hasVotedLocal = reader.GetBoolean(0);
                    bool hasVotedGeneral = reader.GetBoolean(1);

                    if (hasVotedLocal || hasVotedGeneral)
                    {
                        db.Connection.Close();
                        throw new Exception("You have already voted in an election.");
                    }
                }
                else
                {
                    db.Connection.Close();
                    throw new Exception("User not found.");
                }
            }
            db.Connection.Close();

            _next?.Handle(userId, candidateId, electionType);
        }
    }
}
