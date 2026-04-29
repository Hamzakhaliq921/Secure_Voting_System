using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace securevotngsystem
{
    public interface IVoteStrategy
    {
        void Vote(int userId, int candidateId);
    }
}
