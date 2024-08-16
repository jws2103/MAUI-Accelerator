using System;
using System.Diagnostics.CodeAnalysis;
using MauiAccelerator.Core.Constants;

namespace MauiAccelerator.Core.Exceptions;

[ExcludeFromCodeCoverage]
public class NoConnectivityException() : Exception(AppConstants.Connectivity.ConnectionErrorMessage);