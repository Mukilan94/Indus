using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Well_AI.Advisor.API.PEC.Models.Dtos;
using Well_AI.Advisor.API.PEC.Services.IServices;

namespace Well_AI.Advisor.API.PEC.Services
{
    public class SafetyProgramEvaluationDocumentsService : ISafetyProgramEvaluationDocumentsService
    {
        public Task<List<SafetyProgramEvaluationDocumentsDto>> GetSafetyProgramEvaluationDocumentsAsync(string Id)
        {
            throw new NotImplementedException();
        }
    }
}
