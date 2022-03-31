using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z2DataHR.Application.ViewModels;

namespace Z2DataHR.Application.Services
{
    public interface ICandidate
    {
        List<Candidate> GetCandidates();                                  // Get all Candidates 

        Candidate GetCandidateById(int Id);                              // Get Candidate By ID

        public int CreateCandidates(Candidate candidate);               //  Create Candidates

        int UpdateCandidates(int Id, Candidate candidate);              // Update Candidates

        void DeleteCandidateById(int Id);                               // Delete Candidate By ID

        List<CandidateStatusCount> Get_CandidateWith_Status();           //Get Candidate With Status

    }
}
