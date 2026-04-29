using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using securevotngsystem.Singleton;

namespace securevotngsystem.forms
{
    public partial class VoteHistoryForm : Form
    {
        private int _userId;

        public VoteHistoryForm(int userId)
        {
            InitializeComponent();
            _userId = userId;
            LoadVoteHistory();
        }

        private void LoadVoteHistory()
        {
            try
            {
                var db = DbManager.Instance;
                if (db.Connection.State != ConnectionState.Open)
                    db.Connection.Open();

                string query = @"
                    SELECT 
                        c.Name AS CandidateName, 
                        c.Position, 
                        c.Party, 
                        c.ElectionType,
                        v.VoteDate
                    FROM Votes v
                    INNER JOIN Candidates c ON v.CandidateId = c.CandidateId
                    WHERE v.UserId = @UserId
                    ORDER BY v.VoteDate DESC";

                using (SqlCommand cmd = new SqlCommand(query, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", _userId);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading vote history: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (DbManager.Instance.Connection.State == ConnectionState.Open)
                    DbManager.Instance.Connection.Close();
            }
        }
    }
}
