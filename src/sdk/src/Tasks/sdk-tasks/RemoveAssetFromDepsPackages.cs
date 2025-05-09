// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using Microsoft.Build.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.DotNet.Build.Tasks
{
    public class RemoveAssetFromDepsPackages : Task
    {
        [Required]
        public string DepsFile { get; set; }

        [Required]
        public string SectionName { get; set; }

        [Required]
        public string AssetPath { get; set; }

        public override bool Execute()
        {
            DoRemoveAssetFromDepsPackages(DepsFile, SectionName, AssetPath);

            return true;
        }

        public static void DoRemoveAssetFromDepsPackages(string depsFile, string sectionName, string assetPath)
        {
            JToken deps;
            using (var file = File.OpenText(depsFile))
            using (JsonTextReader reader = new(file))
            {
                deps = JToken.ReadFrom(reader);
            }

            bool found = false;
            foreach (JProperty target in deps["targets"])
            {
                foreach (JProperty pv in target.Value.Children<JProperty>())
                {
                    var section = pv.Value[sectionName];
                    if (section != null)
                    {
                        foreach (JProperty relPath in section)
                        {
                            if (assetPath.Equals(relPath.Name))
                            {
                                relPath.Remove();
                                found = true;
                                break;
                            }
                        }
                        if (assetPath.Equals("*"))
                        {
                            section.Parent.Remove();
                            found = true;
                        }
                    }
                }
            }

            if (found)
            {
                using (var file = File.CreateText(depsFile))
                using (var writer = new JsonTextWriter(file) { Formatting = Formatting.Indented })
                {
                    deps.WriteTo(writer);
                }
            }
        }
    }
}
