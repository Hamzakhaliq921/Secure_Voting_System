using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace securevotngsystem
{
    public class GeneralVoteStrategy : IVoteStrategy
    {
        public void Vote(int userId, int candidateId)
        {
            var db = DbManager.Instance;

            try
            {
                db.Connection.Open();

                // Check if user has voted in any election (Local or General)
                var checkCmd = new SqlCommand("SELECT HasVotedLocal, HasVotedGeneral FROM Users WHERE UserId = @userId", db.Connection);
                checkCmd.Parameters.AddWithValue("@userId", userId);

                bool votedLocal = false;
                bool votedGeneral = false;

                using (var reader = checkCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        votedLocal = reader.GetBoolean(0);
                        votedGeneral = reader.GetBoolean(1);
                    }
                }

                if (votedLocal || votedGeneral)
                    throw new Exception("You have already voted in an election.");

                // Record the vote for General election
                var voteCmd = new SqlCommand(
                    "INSERT INTO Votes (UserId, CandidateId, ElectionType) VALUES (@userId, @candidateId, 'General')",
                    db.Connection);
                voteCmd.Parameters.AddWithValue("@userId", userId);
                voteCmd.Parameters.AddWithValue("@candidateId", candidateId);
                voteCmd.ExecuteNonQuery();

                // Update the user's General election vote status
                var updateCmd = new SqlCommand("UPDATE Users SET HasVotedGeneral = 1 WHERE UserId = @userId", db.Connection);
                updateCmd.Parameters.AddWithValue("@userId", userId);
                updateCmd.ExecuteNonQuery();

                MessageBox.Show("Vote cast successfully in General election.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                throw;
            }
            finally
            {
                db.Connection.Close();
            }
        }
    }
}
