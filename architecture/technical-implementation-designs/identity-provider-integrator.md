# Identity Provider Integrator

```mermaid
graph LR
    
    subgraph Customers / Tenants
        CA["Customer A (Ohio - AAD)"]
        CB["Customer B (Vermont - Okta)"]
        CL["Customer C (California - No SSO)"] 
    end

    subgraph Gainwell
        GWT["gainwelltechnologies.com (Azure AD)"]
    end
    
    subgraph Okta / Azure
        IdP("IdP Integrator")        
    end

    subgraph Product Platform
        A("product.local")
        NSSO[["No SSO<br/>Static Keys (Unsupported)"]]
        DB[["Data"]]
        GW[["COTS UI"]]
        API[["COTS API"]]
        TAB[["COTS"]]        
        GOV("Identity Governance")
        %% GW --> |SAML|TAB
    end
    
    
    CA -->|Auth-N & Metadata| IdP
    CB -->|Auth-N & Metadata| IdP
    CL --> |Offer Service Add-On| IdP
    GWT  --> |Auth-N & Metadata| IdP
    GOV --> A    
    TAB --> |"Profile Configuration (App Dependant)"| A
    
    IdP -->|SAML Assertion| DB
    IdP --> |OIDC Token|GW
    IdP --> |OIDC Token|API
    IdP --> |SAML Assertion|TAB
    IdP --> |SCIM API Calls|GOV
    IdP --> |Denied|NSSO

```

---

## Questions / Considerations

Do we need a middleware service between IdP Integrator and the product?

  1. Perform Profile Creation - Based on the assertions attributes, `roles(s)` and `Id`, do we need to create local profiles / identities within the product or it's apps to support how that app functionality with SSO?
  1. Do we need to kick off any other job?
    - Emails to `Id`
    - Notifications to `Tenant`

Who maintains responsibility for the **IdP Integrator**?

  1. Gainwell Corporate or the product?
    - What does the model look like to onboard a new tenant?
    - Will there be a ticketing system to manage this?
