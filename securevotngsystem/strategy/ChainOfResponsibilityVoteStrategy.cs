public class ChainOfResponsibilityVoteStrategy : IVoteStrategy
{
    public void Vote(int userId, int candidateId)
    {
        string electionType = "General"; // or "Local", decide based on your use case

        // Create the chain of handlers
        var checkHandler = new CheckAlreadyVotedHandler();
        var castHandler = new CastVoteHandler();
        var updateHandler = new UpdateUserVoteStatusHandler();

        // Link the handlers together in order
        checkHandler.SetNext(castHandler);
        castHandler.SetNext(updateHandler);

        // Start the chain from the first handler
        checkHandler.Handle(userId, candidateId, electionType);
    }
}
