﻿global using Budget.API.Endpoints;
global using Budget.API.Middlewares;
global using Budget.API.Registrars;
global using Budget.Application.Abstractions;
global using Budget.Application.ExpenseCategories;
global using Budget.Application.ExpenseCategories.GetAll;
global using Budget.Application.Expenses;
global using Budget.Application.Expenses.Create;
global using Budget.Application.Expenses.Delete;
global using Budget.Application.Expenses.GetAll;
global using Budget.Application.Expenses.GetById;
global using Budget.Application.Expenses.Update;
global using Budget.Domain.Expenses;
global using Budget.Infrastructure.Authentication;
global using Budget.Infrastructure.Database;
global using Budget.Infrastructure.ExpenseCategories;
global using Budget.Infrastructure.Expenses;
global using FluentValidation;
global using MediatR;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.IdentityModel.Tokens;
global using Shared.Common.Behaviors;
global using Shared.Common.Exceptions;
global using Shared.Common.Interfaces;
global using Shared.Common.Settings;
global using Shared.Http.Responses;
global using System.Text;
global using System.Text.Json;
