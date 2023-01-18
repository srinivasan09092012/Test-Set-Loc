# User Security Roles

Back to [Project](../../README.md) | [Architecture](../README.md) | [ADRs](README.md)

## Context and Problem Statement

What user security roles will the project platform implement?

## Decision

Reference the [Identity & Access Management ADR](identity-and-access-mgmt.md) for a detailed description of the platform's security policies.

The project will implement [Role-based Access Controls (RBAC)](../dictionary.md#role-based-access-control---rbac) to ensure all users of the system are properly authorized to access and perform functions within the platform and must enforce the [Principle of Least Privilege (POLP)](../dictionary.md#principle-of-least-privilege---polp).

### End User Identity Roles

|Role|Abr|Type|Description|
|----|---|----|-----------|
|**Reference [ETS Architecture Roles](https://github.com/mygainwell/ets-architecture/tree/main/architecture-decision-records/user-security-roles.md#end-user-identity-roles)**|-|-|-|
|**TODO: Add project specific roles here**|-|-|-|-|-|-|-|-|-|-|-|-|-|

### System User Identity Roles

|Role|Abr|Type|Description|
|----|---|----|-----------|
|**Reference [ETS Architecture Roles](https://github.com/mygainwell/ets-architecture/tree/main/architecture-decision-records/user-security-roles.md#system-user-identity-roles)**|-|-|-|
|**TODO: Add project specific roles here**|-|-|-|-|-|-|-|-|-|-|-|-|-|

### User Role Privileges

#### Org-Level Privileges

|Privilege/Function|GUA|GSA|GSO|PUA|PSA|POM|PA|PD|TDE|TDS|TDA|PMA|PSS|
|--------|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|
|**Reference [ETS Architecture privileges](https://github.com/mygainwell/ets-architecture/tree/main/architecture-decision-records/user-security-roles.md#org-level-privileges)**|-|-|-|
|**TODO: Add project specific privileges here**|-|-|-|-|-|-|-|-|-|-|-|-|-|

#### Platform Privileges

|Privilege/Function|GUA|GSA|GSO|PUA|PSA|POM|PA|PD|TDE|TDS|TDA|PMA|PSS|
|--------|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|
|**Reference [ETS Architecture privileges](https://github.com/mygainwell/ets-architecture/tree/main/architecture-decision-records/user-security-roles.md#platform-privileges)**|-|-|-|
|**TODO: Add project specific privileges here**|-|-|-|-|-|-|-|-|-|-|-|-|-|

#### Tenant Privileges

|Privilege/Function|GUA|GSA|GSO|PUA|PSA|POM|PA|PD|TDE|TDS|TDA|PMA|PSS|
|--------|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|
|**Reference [ETS Architecture privileges](https://github.com/mygainwell/ets-architecture/tree/main/architecture-decision-records/user-security-roles.md#tenant-privileges)**|-|-|-|
|**TODO: Add project specific privileges here**|-|-|-|-|-|-|-|-|-|-|-|-|-|

## Consequences

- These limited but distinct roles will ensure proper separation of concerns for providing a functioning, safe, and secure platform for our tenants while aligning with platform security requirements.

## Relevant Sources

- NA
