﻿namespace Shared.Common.Exceptions;

public class BadRequestException(string message) : Exception(message) { }
