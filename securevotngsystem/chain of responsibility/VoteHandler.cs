namespace securevotngsystem
{
    public abstract class VoteHandler
    {
        protected VoteHandler _next;

        public void SetNext(VoteHandler next)
        {
            _next = next;
        }

        public abstract void Handle(int userId, int candidateId, string electionType);
    }
}
