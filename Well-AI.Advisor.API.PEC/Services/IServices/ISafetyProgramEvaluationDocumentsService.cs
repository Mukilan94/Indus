using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Well_AI.Advisor.API.PEC.Models.Dtos;

namespace Well_AI.Advisor.API.PEC.Services.IServices
{
    public interface  ISafetyProgramEvaluationDocumentsService
    {
        Task<List<SafetyProgramEvaluationDocumentsDto>> GetSafetyProgramEvaluationDocumentsAsync(string Id);
    }
}
