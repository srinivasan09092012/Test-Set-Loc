# Changes for AdventureWorks DB

Back to [Project](../../../../../README.md) | [Components](../../../../README.md) | [Databases](../../../README.md) | [MS SQL](../../README.md) | [AdventureWorks](../README.md)

---

## Intro

This directory should contain database change scripts to be associated with the AdventureWorks database.

## Rules

1. **ALL** database changes associated with feature development **MUST** be scripted.
2. Scripted database changes **MUST** be included in the feature branch merge into its **parent branch** (e.g. `main`, `r23`, `r24`, etc.) via **PULL REQUEST**.
3. **ALL** Database changes that cannot simply be modified in place (e.g. stored procedures, functions, views) should be made in the `_Changes` directory.
   1. Script filenames should follow naming conventions to facilitate keeping change scripts in **intended order** (e.g. `SP###_<seq##>_{<story####>_}<objectname>-{Table|Data}.sql`).
   2. Once the script has been MERGED into `main` thru PR approval, the script should **NOT** be changed any further.
      1. Once PR is approved, the script will be applied via automated CI process, and should NOT be applied again.
      2. Any additional changes to the underlying object(s) and/or data should be done via ADDITIONAL compensating action script(s) in the `_Changes` directory.
      3. TODO: Add a branch / directory rule to NOT allow changes to scripts in `_Changes` directory once merged into `main`.  **Is this possible?**
   3. Changes to indexes, constraints, and triggers should be considered **Table** changes.
   4. Scripts should be written to be resilient and survive **multiple executions** for the script.
      1. Add IF checks for existence.
      2. DROP and CREATE, if no impact on referential integrity
   5. Scripts should be written to avoid **unintended** data loss.
      1. Add IF checks for existence.
      2. Prefer to use **ALTER** statements.
      3. Do **NOT** DROP and CREATE, if impact on referential integrity.
      4. Do **NOT** DELETE (or TRUNCATE) and INSERT, if impact on referential integrity.
   6. Scripts can include DML (i.e. insert, update, delete) to safely implement the intended change (e.g. adding a NOT NULL column).
   7. Scripts can include multiple objects to safely implement the intended change (e.g. foreign key constraint).
   8. For **DATA** scripts, scripts should only address **CLIENT AGNOSTIC** data, defined and originating from the product **ENGINEERING** team.
      1. TODO:  What to do about **CLIENT SPECIFIC** data (e.g. rules, configuration, reference data, etc.)
   9. Scripts from `_Changes` directory should automatically generate corresponding changes to corresponding object directories as part of **Continuous Integration** automation.
