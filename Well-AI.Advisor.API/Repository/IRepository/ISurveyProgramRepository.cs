using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Repository.IRepository
{
    public interface ISurveyProgramRepository
    {
        ICollection<SurveyProgram> GetSurveyProgramDetails();
        SurveyProgram GetSurveyProgramDetail(string uid);
        bool SurveyProgramExists(string Uid);
        bool CreateSurveyProgram(SurveyProgram surveyProgram);
        bool UpdateSurveyProgram(SurveyProgram surveyProgram);
        bool DeleteSurveyProgram(SurveyProgram surveyProgram);
        bool Save();
    }
}
