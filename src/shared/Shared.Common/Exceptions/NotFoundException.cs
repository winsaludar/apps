﻿namespace Shared.Common.Exceptions;

public class NotFoundException(string message) : Exception(message) { }
