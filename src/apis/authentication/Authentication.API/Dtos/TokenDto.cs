﻿namespace Authentication.API.Dtos;

public record TokenDto(string Value, string RefreshToken, DateTime ExpiresAt);