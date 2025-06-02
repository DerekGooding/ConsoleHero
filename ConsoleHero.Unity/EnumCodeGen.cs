using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
//using UnityEditor;
//using UnityEngine;

//[InitializeOnLoad]
public static class EnumCodeGen
{
    static EnumCodeGen()
    {
        //if (!SessionState.GetBool("EnumCodeGen_RanOnce", false))
        //{
        //    SessionState.SetBool("EnumCodeGen_RanOnce", true);
        //    Generate();
        //}
    }

    //[MenuItem("Tools/Regenerate Enums From IContent")]
    public static void Generate()
    {
        const string outputPath = "Assets/Generated";
        Directory.CreateDirectory(outputPath);

        var allTypes = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => !a.FullName.StartsWith("Unity")) // Filter Unity cruft
            .SelectMany(a =>
            {
                try { return a.GetTypes(); }
                catch { return Type.EmptyTypes; }
            });

        foreach (var type in allTypes)
        {
            if (!type.IsClass || type.IsAbstract) continue;

            var iContent = type.GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition().Name.StartsWith("IContent"));

            if (iContent == null) continue;

            var genericArg = iContent.GetGenericArguments().FirstOrDefault()?.Name ?? "object";
            var allProp = type.GetProperty("All", BindingFlags.Public | BindingFlags.Static);
            if (!(allProp?.GetValue(null) is IEnumerable allValues)) continue;

            var enumMembers = allValues.Cast<object>()
                .Select(o =>
                {
                    var str = o?.ToString() ?? "";
                    return SanitizeEnumName(str);
                })
                .ToList();

            if (enumMembers.Count == 0) continue;

            var enumCode = GenerateEnum(type.Name, enumMembers);
            var helperCode = GenerateHelper(type, enumMembers, genericArg);

            File.WriteAllText(Path.Combine(outputPath, $"{type.Name}TypeEnum.g.cs"), enumCode);
            File.WriteAllText(Path.Combine(outputPath, $"{type.Name}Helper.g.cs"), helperCode);
        }

        //AssetDatabase.Refresh();
        //Debug.Log("Enum generation complete.");
    }

    private static string GenerateEnum(string className, List<string> enumMembers)
    {
        var members = string.Join(",\n", enumMembers.Select(m => $"        {m}"));
        return $@"// Auto-generated
namespace ContentEnums
{{
    public enum {className}Type
    {{
{members}
    }}
}}";
    }

    private static string GenerateHelper(Type classType, List<string> enumMembers, string typeArg)
    {
        var className = classType.Name;
        var ns = classType.Namespace ?? "UnknownNamespace";
        var memberAccessors = string.Join(";\n",
            enumMembers.Select((m, i) => $"        public {typeArg} {m} => All[{i}]"));

        return $@"// Auto-generated
using ContentEnums;

namespace {ns}
{{
    public partial class {className}
    {{
        public {typeArg} Get({className}Type type) => All[(int)type];
        public {typeArg} this[{className}Type type] => All[(int)type];
        public {typeArg} GetById(int id) => All[id];
{memberAccessors};
    }}
}}";
    }

    private static string SanitizeEnumName(string name)
    {
        var builder = new StringBuilder();
        foreach (var ch in name)
        {
            if (char.IsLetterOrDigit(ch) || ch == '_') builder.Append(ch);
        }
        return builder.ToString();
    }
}
