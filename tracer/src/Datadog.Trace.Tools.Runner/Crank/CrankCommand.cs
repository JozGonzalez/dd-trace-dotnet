// <copyright file="CrankCommand.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using Spectre.Console.Cli;

namespace Datadog.Trace.Tools.Runner.Crank;

internal class CrankCommand : Command<CrankSettings>
{
    public override int Execute(CommandContext context, CrankSettings settings)
    {
        return Importer.Process(settings.InputFile);
    }
}
