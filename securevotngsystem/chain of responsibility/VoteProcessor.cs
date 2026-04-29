namespace securevotngsystem
{
    public class VoteProcessor
    {
        private readonly VoteHandler _chain;

        public VoteProcessor()
        {
            // Setup the chain: Check -> Cast -> Update
            var checkHandler = new CheckAlreadyVotedHandler();
            var castHandler = new CastVoteHandler();
            var updateHandler = new UpdateUserVoteStatusHandler();

            checkHandler.SetNext(castHandler);
            castHandler.SetNext(updateHandler);

            _chain = checkHandler;
        }

        public void Process(int userId, int candidateId, string electionType)
        {
            _chain.Handle(userId, candidateId, electionType);
        }
    }
}
