# AWS IAM Identities

Back to [Project](../../../../README.md) | [Architecture](../../../README.md) | [TID](../../README.md) | [AWS](../README.md) | [Network](README.md)


|        |                          |
|-------:|:-------------------------|
|**Type**|Cloud-Native Service|
|**Platform**|AWS|
|**Model**|IaaS|
|**Multi-tenant Capable**|Yes|
|**Vendor**|Amazon|
|**Website**|[Identities](https://docs.aws.amazon.com/IAM/latest/UserGuide/id.html)|
|**Account Contact**|[NA](mailto:)|
|**Technical Contact**|[NA](mailto:)|

## Diagrams

## Deploy/Integrate to Accounts

- [x] Development
- [x] Management
- [x] Staging
- [x] Production

## Deployment Method

- [ ] Manual
- [x] IaC Automation
- [ ] Vendor Managed (SaaS/PaaS)
- [ ] Other (Explain)

## Implementation Type

- [ ] Virtual Compute
- [ ] Docker Swarm
- [ ] Kubernetes
- [ ] Native Service
- [ ] Vendor Managed (SaaS/PaaS)
- [x] Other (AWS Directory Services Configuration)

## Operating System

- [ ] RedHat
- [ ] Windows
- [x] N/A

## IAM Policies

### Platform IAM Policies

|IAM Policy Name|Description|Accounts|
|---------------|-----------|--------|
|gwt-product_name`{account}`-iamp-platform-user-admin|Provisions and deprovisions platform user accounts and access rights|MGMT, DEV, STAGE, PROD|
|gwt-product_name`{account}`-iamp-platform-system-admin|Provisions and deprovisions platform cloud-based resources|MGMT, DEV, STAGE, PROD|
|gwt-product_name`{account}`-iamp-platform-ops-manager|Monitors platform stability and performance|MGMT, DEV, STAGE, PROD|
|gwt-product_name`{account}`-iamp-platform-architect|Designs and builds the platform components that make up the product offering|MGMT, DEV, STAGE, PROD|
|gwt-product_name`{account}`-iamp-platform-developer|Designs and builds platform software components that make up the product offering|MGMT, DEV, STAGE, PROD|
|gwt-product_name`{account}`-iamp-platform-mgmt-agent|Used to manage cloud-based platform resources|MGMT, DEV, STAGE, PROD|
|gwt-product_name`{account}`-iamp-platform-security-agent|Used to run and monitor system resources|MGMT, DEV, STAGE, PROD|

### Tenant IAM Policies

|IAM Policy Name|Description|Accounts|
|---------------|-----------|--------|
|gwt-product_name`{account}`-iamp-tenant-data-engineer|Uses the platform to build data science notebooks in order to transform raw data into basic business data aggregations|DEV, STAGE, PROD|
|gwt-product_name`{account}`-iamp-tenant-data-scientist|Uses the platform to build sophisticated machine learning and artificial intelligence algorithms to transform business data aggregations into advanced business data aggregates|DEV, STAGE, PROD|
|gwt-product_name`{account}`-iamp-tenant-data-analyst|Uses the platform to build, run, and analyze reports against the  basic and advanced business data aggregates|DEV, STAGE, PROD|

## IAM Roles

### Platform IAM Roles (System Agent Use)

|Business Role|IAM Role Name|Accounts|IAM Policy Name|
|-------------|---------------|--------|---------------|
|[Platform Management Agent](../../../adr/user-security-roles.md#end-user-identity-roles)|gwt-product_name`{account}`-iamr-platform-mgmt-agent|MGMT, DEV, STAGE, PROD|gwt-product_name`{account}`-iamp-platform-mgmt-agent|
|[Platform Security Agent](../../../adr/user-security-roles.md#end-user-identity-roles)|gwt-product_name`{account}`-iamr-platform-security-agent|MGMT, DEV, STAGE, PROD|gwt-product_name`{account}`-iamp-platform-security-agent|

## IAM User Groups

### Platform IAM User Groups

|Business Role|IAM User Group Name|Accounts|IAM Policy Name|
|-------------|---------------|--------|---------------|
|[Platform User Administrator](../../../adr/user-security-roles.md#end-user-identity-roles)|gwt-product_name`{account}`-iamg-platform-user-admin|MGMT, DEV, STAGE, PROD|gwt-product_name`{account}`-iamp-platform-user-admin|
|[Platform System Administrator](../../../adr/user-security-roles.md#end-user-identity-roles)|gwt-product_name`{account}`-iamg-platform-system-admin|MGMT, DEV, STAGE, PROD|gwt-product_name`{account}`-iamp-platform-system-admin|
|[Platform Operations Manager](../../../adr/user-security-roles.md#end-user-identity-roles)|gwt-product_name`{account}`-iamg-platform-ops-manager|MGMT, DEV, STAGE, PROD|gwt-product_name`{account}`-iamp-platform-ops-manager|
|[Platform Architect](../../../adr/user-security-roles.md#end-user-identity-roles)|gwt-product_name`{account}`-iamg-platform-architect|MGMT, DEV, STAGE, PROD|gwt-product_name`{account}`-iamp-platform-architect|
|[Platform Developer](../../../adr/user-security-roles.md#end-user-identity-roles)|gwt-product_name`{account}`-iamg-platform-developer|MGMT, DEV, STAGE, PROD|gwt-product_name`{account}`-iamp-platform-developer|

### Tenant IAM User Groups

|Business Role|IAM User Group Name|Accounts|IAM Policy Name|
|-------------|---------------|--------|---------------|
|[Tenant Data Engineer](../../../adr/user-security-roles.md#end-user-identity-roles)|gwt-product_name`{account}`-`{tenant}`-iamg-tenant-data-engineer|DEV, STAGE, PROD|gwt-product_name`{account}`-iamp-tenant-data-engineer|
|[Tenant Data Scientist](../../../adr/user-security-roles.md#end-user-identity-roles)|gwt-product_name`{account}`-`{tenant}`-iamg-tenant-data-scientist|DEV, STAGE, PROD|gwt-product_name`{account}`-iamp-tenant-data-scientist|
|[Tenant Data Analyst](../../../adr/user-security-roles.md#end-user-identity-roles)|gwt-product_name`{account}`-`{tenant}`-iamg-tenant-data-analyst|DEV, STAGE, PROD|gwt-product_name`{account}`-iamp-tenant-data-analyst|

### Dependent Technology

- [Accounts](../accounts/README.md)

## Relevant Sources

- NA
