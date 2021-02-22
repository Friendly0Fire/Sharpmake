﻿// Copyright (c) 2017, 2019 Ubisoft Entertainment
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sharpmake;

[module: Sharpmake.Include("projects.sharpmake.cs")]

namespace CLR_SharpmakeTest
{
    public static class Common
    {
        public static Target[] CommonTarget = {
            new Target(
                Platform.win32,
                DevEnv.vs2015 | DevEnv.vs2017,
                Optimization.Debug | Optimization.Release,
                OutputType.Dll,
                Blob.NoBlob,
                BuildSystem.MSBuild,
                DotNetFramework.v4_5 | DotNetFramework.v4_6_2)};
    }

    [Sharpmake.Generate]
    public class TheSolution : CSharpSolution
    {
        public TheSolution()
        {
            Name = "CPPCLI";
            AddTargets(Common.CommonTarget);
        }

        [Configure()]
        public void ConfigureAll(Configuration conf, Target target)
        {
            conf.SolutionFileName = "CPPCLI.[target.DevEnv].[target.Framework]";

            conf.SolutionPath = @"[solution.SharpmakeCsPath]\projects\";

            conf.AddProject<CLR_CPP_Proj>(target);
            conf.AddProject<OtherCSharpProj>(target);
            conf.AddProject<TestCSharpConsole>(target);
        }
    }

    public static class StartupClass
    {
        [Sharpmake.Main]
        public static void SharpmakeMain(Sharpmake.Arguments arguments)
        {
            arguments.Generate<TheSolution>();
        }
    }
}

