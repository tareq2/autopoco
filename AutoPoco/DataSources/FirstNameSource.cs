﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;

namespace AutoPoco.DataSources
{
    public class FirstNameSource : DatasourceBase<String>
    {
        private Random mRandom = new Random();

        public override string Next(IGenerationSession session)
        {
            return FirstNames[mRandom.Next(0, FirstNames.Length - 1)];
        }

        private static string[] FirstNames = new String[]{
            "Jack",	
            "Thomas",	
            "Oliver",	
            "Joshua",
            "Harry",
            "Charlie",
            "Daniel",
            "William",
            "James",
            "Alfie",
            "Samuel",
            "George",
            "Joseph",
            "Benjamin",
            "Ethan",
            "Lewis",
            "Mohammed",
            "Jake",
            "Dylan",
            "Jacob",

            "Ruby",	
            "Olivia",	
            "Grace",	
            "Emily",
            "Jessica",
            "Chloe",
            "Lily",
            "Mia",
            "Lucy",
            "Amelia",
            "Evie",
            "Ella",
            "Katie",
            "Ellie",
            "Charlotte",
            "Summer",
            "Mohammed",
            "Megan",
            "Hannah",
            "Ava"
        };
    }
}
