// <copyright file="CoverageEventHandler.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>
#nullable enable

using System.Runtime.CompilerServices;
using System.Threading;

namespace Datadog.Trace.Ci.Coverage;

/// <summary>
/// Coverage event handler
/// </summary>
internal abstract class CoverageEventHandler
{
    private readonly AsyncLocal<CoverageContextContainer?> _asyncContext;
    private readonly CoverageContextContainer _globalContainer;

    protected CoverageEventHandler()
    {
        _asyncContext = new(obj => CoverageReporter.FireContextContainerChangeAction(obj.CurrentValue));
        _globalContainer = new CoverageContextContainer();
    }

    /// <summary>
    /// Gets the coverage local container
    /// </summary>
    internal CoverageContextContainer? Container => _asyncContext.Value;

    /// <summary>
    /// Gets the coverage global container
    /// </summary>
    internal CoverageContextContainer GlobalContainer => _globalContainer;

    /// <summary>
    /// Start session
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void StartSession()
    {
        var context = new CoverageContextContainer();
        OnSessionStart(context);
        _asyncContext.Value = context;
    }

    /// <summary>
    /// End async session
    /// </summary>
    /// <returns>Object instance with the final coverage report</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public object? EndSession()
    {
        if (_asyncContext.Value is { } context)
        {
            _asyncContext.Value = null;
            return OnSessionFinished(context);
        }

        return null;
    }

    /// <summary>
    /// Method called when a session is started
    /// </summary>
    /// <param name="context">Coverage context container</param>
    protected abstract void OnSessionStart(CoverageContextContainer context);

    /// <summary>
    /// Method called when a session is finished to process all coverage raw data.
    /// </summary>
    /// <param name="context">Coverage context container</param>
    /// <returns>Instance of the final coverage report</returns>
    protected abstract object? OnSessionFinished(CoverageContextContainer context);
}
