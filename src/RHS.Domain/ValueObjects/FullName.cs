﻿using EnsureThat;
using RHS.Domain.Common;

namespace RHS.Domain.ValueObjects;

public class FullName : ValueObjects
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    private FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public FullName() { } //for ORM

    public static Result<FullName> Create(string firstName, string lastName)
    {
        Ensure.That(firstName, nameof(firstName)).IsNotNullOrEmpty();
        Ensure.That(lastName, nameof(lastName)).IsNotNullOrEmpty();
        
        return Result.Ok<FullName>(new FullName(firstName, lastName));
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
    }
}