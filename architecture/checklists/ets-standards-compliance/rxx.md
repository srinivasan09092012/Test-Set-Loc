# Checklist - ETS Standards Compliance

Back to [Project](../README.md) | [Architecture](README.md)

---

## Review Status

|Property|Value(s)|
|-------:|--------|
|**State**|`Proposed`, `Accepted`|
|**Product Name**||
|**Product Release Number**|XX|
|**Review Date**|mm/dd/yyyy|
|**Reviewer(s) - Product Architect**||
|**Reviewer(s) - DBAs**||
|**Reviewer(s) - DevSecOps**||
|**Reviewer(s) - Security**||

## Description

The purpose of this checklist is to ensure the product deliverables are following (or tracking towards) standard ETS guidelines for modern development. This will help ensure teams understand what deliverables are expected.

## Steps

In the last sprint of the release perform the following:

1. Create a copy (new markdown file) of this checklist for each major release and name it accordingly (i.e. `r25.md`). Ensure you use the [latest checklist](https://github.com/mygainwell/ets-bootstrap/tree/main/architecture/checklists/ets-standards-compliance/rxx.md) from the ETS bootstrap repository, it can change over time.
2. The module TO/TA fills out the information below. Review each requirement in the compliance checklist and place an [`x`] in the box where the project meets the requirement. If the project falls short in any area, add a backlog entry to ensure the project is tracking toward compliance.
3. The module TO/TA puts the document in `Proposed` status and creates a Pull Request for review.
4. The module TO/TA Schedules a meeting with the a minimum of 1 reviewer from the product architects, DBAs, DevSecOPs, and security teams.
5. Once reviewed and approved, change the status to `Accepted`, add the review date, and resource names present in the Review Status section above.
6. Commit the document to the main branch of the repo.

---

## Source Control, Technical Documentation, & Organization

- [ ] [Using GitHub for all Source Code](https://github.com/mygainwell/ets-architecture/tree/main/architecture-decision-records/scm)

- [ ] [Using GitHub for all Technical Documentation](https://github.com/mygainwell/ets-architecture/blob/main/architecture-decision-records/technical-docs.md)

- [ ] [Implementing Mono Repository](https://github.com/mygainwell/ets-architecture/blob/main/architecture-decision-records/scm/repository-mgmt.md)

- [ ] [Following Technical Documentation Standards](https://github.com/mygainwell/ets-architecture/blob/main/architecture-decision-records/technical-docs.md)

- [ ] [Following Repository Naming Standards](https://github.com/mygainwell/ets-architecture/blob/main/architecture-decision-records/scm/repository-mgmt.md#project-repositories) (Ensure your repo is listed)

- [ ] [Following Repository Workflow Policy](https://github.com/mygainwell/ets-architecture/blob/main/architecture-decision-records/scm/workflow-policy.md)

- [ ] [Using the Latest Bootstrap Repository Structure](https://github.com/mygainwell/ets-bootstrap)

### Technical Debt - SC

If the project does not conform, list any remaining technical debt & associated backlog user stories

|**#**|**Description**|**Backlog User Story**|
|-----|---------------|----------------------|
|1|TODO: Some description here...|[USXXXXXX](https://link-to-your-backlog-user-story.com/)|

### Comments / Notes  - SC

TODO: Add comments as needed

## Product Direction & Understanding

- [ ] [Defined Mission Statement](../../mission-statement.md)

- [ ] [Updated Adopted Software Technology](../../adopted-software-technologies.md)

- [ ] [Amended Guiding Principles](../../adopted-software-technologies.md) (if any required, do not repeat [ETS Guiding Principles](https://github.com/mygainwell/ets-architecture/blob/main/guiding-principles.md), only amend)

- [ ] [Updated Dictionary](../../dictionary.md)

- [ ] [Amended Definitions of Done](../../dictionary.md) (if any required, do not repeat [ETS DOD](https://github.com/mygainwell/ets-architecture/tree/main/definitions-of-done), only amend)

- [ ] [Created Project Onboarding](../../../onboarding/project-onboarding.md)

### Technical Debt - PD

If the project does not conform, list any remaining technical debt & associated backlog user stories

|**#**|**Description**|**Backlog User Story**|
|-----|---------------|----------------------|
|1|TODO: Some description here...|[USXXXXXX](https://link-to-your-backlog-user-story.com/)|

### Comments / Notes - PD

TODO: Add comments as needed

## Design & Development

- [ ] [Following Agile Practice Standards](https://github.com/mygainwell/ets-architecture/blob/main/architecture-decision-records/agile-team-development.md)

- [ ] [Following Guiding Principles](https://github.com/mygainwell/ets-architecture/blob/main/guiding-principles.md)

- [ ] [Using Approved & Adopted Software Technologies](https://github.com/mygainwell/ets-architecture/blob/main/adopted-software-technologies.md)

- [ ] [Implementing Trunk-based Development](https://github.com/mygainwell/ets-architecture/blob/main/architecture-decision-records/scm/branching-workflow.md)

- [ ] [Collaborating in GitHub Using Pull Requests](https://github.com/mygainwell/ets-architecture/blob/main/architecture-decision-records/scm/branching-workflow.md)

- [ ] [Code Conforms to Quality Standards](https://github.com/mygainwell/ets-architecture/blob/main/architecture-decision-records/measuring-quality.md)

  - [ ] Code Complexity Acceptable
  - [ ] Test Code Coverage Meets/Exceeds 90%

### Technical Debt - DD

If the project does not conform, list any remaining technical debt & associated backlog user stories

|**#**|**Description**|**Backlog User Story**|
|-----|---------------|----------------------|
|1|TODO: Some description here...|[USXXXXXX](https://link-to-your-backlog-user-story.com/)|

### Comments / Notes - DD

TODO: Add comments as needed

## CICD - Software & Infrastructure

### Software

- [ ] [Packages Stored in Package Registry](https://github.com/mygainwell/ets-architecture/blob/main/architecture-decision-records/scm/package-registry.md)

  - [ ] Azure Artifacts
  - [ ] JFrog Artifactory

- [ ] [Packages Following Organization and Naming Standards](https://github.com/mygainwell/ets-architecture/blob/main/architecture-decision-records/scm/package-registry.md)

### Infrastructure

- [ ] [Implementing Deployment Pipelines](https://github.com/mygainwell/ets-architecture/blob/main/architecture-decision-records/scm/release-mgmt.md)

  - [ ] Azure Pipelines (Pipeline as code)
  - [ ] GitHub Actions

- [ ] [Pipelines Using Security Scanning Tools](https://github.com/mygainwell/ets-architecture/blob/main/adopted-software-technologies.md#commercial-off-the-shelf-software---cots)

  - [ ] SonarQube
  - [ ] Veracode

- [ ] [Implementing Infrastructure-As-Code](https://github.com/mygainwell/ets-architecture/blob/main/dictionary.md#infrastructure-as-code---iac)

  - [ ] Terraform
    - [ ] Terraform state file is saved remotely
    - [ ] Using recommended organization hierarchy
    - [ ] Terratest written for any modules

  - [ ] Terragrunt

  - [ ] Ansible
    - [ ] Playbook variablized to scale for deployment to multiple environments
    - [ ] Tasks created as roles
    - [ ] Playbook dynamically querying EC2 tags to identify servers where install needs to occur
    - [ ] Automated linting and precommit check is enabled for quality

- [ ] [Following Resource Naming Standards](https://github.com/mygainwell/ets-architecture/blob/main/technical-implementation-designs/aws/resource-naming-standards.md)

- [ ] [Following Resource Tag Naming Standards](https://github.com/mygainwell/ets-architecture/blob/main/technical-implementation-designs/aws/tag-naming-standards.md)

### Commercial off the shelf (COTS) products

- [ ] Automation runbook checked in as part of documentation

- [ ] Automation demo complete
  - [ ] Install
  - [ ] Destroy infrastructure
  - [ ] Install again on new infrastructure

### Database

- [ ] [Implementing Database Deployment Automation](https://github.com/mygainwell/ets-architecture/blob/main/architecture-decision-records/scm/database-automation.md)

  - [ ] Database Seeded on Deployment (if applicable)

### Technical Debt - CICD

If the project does not conform, list any remaining technical debt & associated backlog user stories

|**#**|**Description**|**Backlog User Story**|
|-----|---------------|----------------------|
|1|TODO: Some description here...|[USXXXXXX](https://link-to-your-backlog-user-story.com/)|

### Comments / Notes - CICD

TODO: Add comments as needed
