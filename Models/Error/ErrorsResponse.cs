using System.Collections.Generic;

namespace SurveySystem.Models.Error
{
    public class ErrorsResponse
    {
        public ErrorsResponse(IEnumerable<string> errors)
        {
            Errors = errors;
        }
        public IEnumerable<string> Errors { get; set; }
    }
}