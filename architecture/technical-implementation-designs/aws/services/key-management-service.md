# Key Management Service

Back to [Project](../../../../README.md) | [Architecture](../../README.md) | [TID](../README.md) | [Cloud Platform](README.md)

|        |                          |
|-------:|:-------------------------|
|**Type**|Cloud-Native Service|
|**Platform**|AWS|
|**Model**|SaaS|
|**Multi-tenant Capable**|Yes|
|**Vendor**|Amazon|
|**Website**|[Amazon Key Management Service (KMS)](https://aws.amazon.com/kms/)|
|**Account Contact**|[NA](mailto:)|
|**Technical Contact**|[NA](mailto:)|

## Introduction

Amazon KMS is a fully managed service to create and control the keys used to encrypt or digitally sign data assets.

## Diagrams

### Pub/Sub

``` mermaid
graph LR
    subgraph AA["Account (MGMT, DEV, STAGE, PROD)"]
        S3[("S3 Data")]
        PR("Platform Process")
        subgraph KMS["Key Management Service"]
            SN("Key Service")
            ST[("Keystore")]
            SN --> |"reference"| ST
        end
    end
    S3 --> |"retrieve key(HTTPS:443)"| SN
    S3 --> |"encrypt/decrypt"| S3
    PR --> |"request data (HTTPS:443)"| S3
```

## Deploy/Integrate to Accounts

- [x] Development
- [x] Management
- [x] Staging
- [x] Production

## Deployment Method

- [ ] Manual
- [x] IaC Automation [iac_module]
- [ ] Vendor Managed (SaaS/PaaS)
- [ ] Other (Explain)

## Implementation Type

- [ ] Virtual Compute
- [ ] Docker Swarm
- [ ] Kubernetes
- [ ] Native Service
- [x] Vendor Managed (SaaS/PaaS)
- [ ] Other (Explain)

## Operating System

- [ ] RedHat
- [ ] Windows
- [x] N/A

## Keys

|Alias|Type|Use|Origin|Regionality|Tags|IAM Admin Roles|IAM User Roles|Description|
|-----------|----|----|------|-----------|----|---------------|--------------|-----------|
|gwt-product_name{account}-cmn-all-kms-sym|Symmetric|Encrypt/Decrypt|KMS|Multi-Region||GSO<br/>PSA|GSO<br/>PSA<br/>PMA|Common dev account symmetric key|

## Dependent Technology

- [Accounts](../accounts/README.md)

## Relevant Sources

- [AWS KMS Best Practices](https://docs.aws.amazon.com/kms/latest/developerguide/best-practices.html)
