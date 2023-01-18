# Example Tree

```
$ tree -a -C infrastructure-as-code/
infrastructure-as-code/
├── README.md
├── ansible
│   └── README.md
├── cloud-formation
│   └── README.md
└── terraform
    ├── README.md
    ├── component-1
    │   ├── dev
    │   │   ├── main.tf
    │   │   ├── outputs.tf
    │   │   └── variables.tf
    │   ├── prd
    │   └── stg
    ├── component-n
    ├── components
    │   ├── component-1
    │   │   ├── namespace-a.tf
    │   │   ├── namespace-b.tf
    │   │   ├── namespace-c.tf
    │   │   ├── outputs.tf
    │   │   └── variables.tf
    │   └── component-n
    └── global
        └── state
            ├── main.tf
            ├── outputs.tf
            └── variables.tf

13 directories, 15 files
```