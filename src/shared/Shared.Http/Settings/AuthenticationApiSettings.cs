﻿namespace Shared.Http.Settings;

public record AuthenticationApiSettings
{
    public string BaseUrl { get; init; } = default!;
    public string LoginRoute { get; init; } = default!;
    public string RegisterRoute { get; set; } = default!;
    public string ConfirmEmailRoute { get; set; } = default!;
}

