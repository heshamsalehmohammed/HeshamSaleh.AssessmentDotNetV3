using System.Collections.Generic;
using System.Reflection;

namespace HeshamSaleh.AssessmentDotNetV3.CrossCutting.Assemblies
{
    public static class AssemblyUtil
    {
        public static IEnumerable<Assembly> GetCurrentAssemblies()
        {
            return new Assembly[]
            {
                Assembly.Load("HeshamSaleh.AssessmentDotNetV3.Api"),
                Assembly.Load("HeshamSaleh.AssessmentDotNetV3.Application"),
                Assembly.Load("HeshamSaleh.AssessmentDotNetV3.Domain"),
                Assembly.Load("HeshamSaleh.AssessmentDotNetV3.Domain.Core"),
                Assembly.Load("HeshamSaleh.AssessmentDotNetV3.Infrastructure"),
                Assembly.Load("HeshamSaleh.AssessmentDotNetV3.CrossCutting")
            };
        }
    }
}
