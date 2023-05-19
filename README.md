# Cadmus TGR Parts

Components for the _Tradizione Grammaticale Romana_ project (here TGR for short) in Cadmus.

## History

### 4.0.6

- 2023-05-19: updated packages.

### 4.0.5

- 2023-05-12: updated packages.

### 4.0.4

- 2023-03-27: updated packages.

### 4.0.3

- 2023-03-07: updated packages.

### 4.0.2

- 2023-02-06: migrated to new components factory. This is a breaking change for backend components, please see [this page](https://myrmex.github.io/overview/cadmus/dev/history/#2023-02-01---backend-infrastructure-upgrade). Anyway, in the end you just have to update your libraries and a single namespace reference. Benefits include:
  - more streamlined component instantiation.
  - more functionality in components factory, including DI.
  - dropped third party dependencies.
  - adopted standard MS technologies for DI.

### 3.0.1

- 2022-11-27: updated packages.

### 3.0.0

- 2022-11-10: upgraded to NET 7.

### 2.3.0

- 2022-11-06: refactored projects for nullability check.

### 2.2.1

- 2022-10-22: updated packages and removed CLI project.

### 2.2.0

- 2022-10-10: refactored services to be used both by API and CLI. This makes the `Cadmus.Cli.Plugin.Tgr` project obsolete.

### 2.1.5

- 2022-10-10: updated packages.

### 2.1.4

- 2022-10-05: updated packages.

### 2.1.3

- 2022-08-18: updated packages.

### 2.1.2

- 2022-07-30: added `Note` to `QuotationParallel`.

### 2.1.1

- 2022-07-29: added `Note` to `VarQuotation`, both as its direct child and as child of `QuotationVariant`.
- 2022-07-24: made projects nullable.

### 2.1.0

- 2022-06-21: updated packages (new graph subsystem).

### 2.0.2

- 2022-05-29: updated packages.

### 2.0.1

- 2022-05-13: fixed dependencies to correctly point to Cadmus.General.Parts.

### 2.0.0

- 2022-04-24: added `AvailableWitnessesLayerFragment` and its related code.
- 2022-04-22: updated packages
- 2022-02-14: upgraded to new Cadmus part libraries.
- 2021-11-22: upgraded to refactored Cadmus components (API endpoints).
- 2021-11-11 (v 2.0.0): upgraded to NET 6.
- 2021-10-15 (v 1.1.0): breaking changes:

(1) for auth database by AspNetCore.Identity.Mongo 8.3.1 (used since Cadmus.Api.Controllers 1.3.0, Cadmus.Api.Services 1.2.0):

```js
/*
Removed fields:
AuthenticatorKey = null
RecoveryCodes = []
*/
db.Users.updateMany({}, { $unset: {"AuthenticatorKey": 1, "RecoveryCodes": 1} });
```

(2) `DocReference` uses the new model from bricks. This affects these parts:

- MsContent
- MsPlace

and their respective seeders.

We can provision an upgrade path which just concatenates author+work of the legacy references into a single citation, using some convention.

(3) `MsSignaturesPart` and `MsSignature` have been moved from Itinera to this project (Itinera is going to use a new generation model for codicology). Apart from their tag change, their code is unaffected. So, we need to update the profile by replacing 'itinera' with 'tgr':

(3.1.) in Mongo: update the parts:

```js
db.parts.updateMany({ "typeId" : "it.vedph.itinera.ms-signatures" }, { $set: { "typeId": "it.vedph.tgr.ms-signatures"}});
```

Update the facets: find the facet having the `itinera` part:

```js
db.getCollection("facets").find({ "partDefinitions.typeId" : "it.vedph.itinera.ms-signatures" });
```

It should have index=1 in the facets array. Now looking at its `partDefinitions` array, note its index; it should be 5. Then update:

```js
db.getCollection("facets").updateOne({ "partDefinitions.typeId" : "it.vedph.itinera.ms-signatures" }, { $set: { "partDefinitions.$.typeId": "it.vedph.tgr.ms-signatures"}});
```

If you now look at the collection, the above find should match nothing.

Finally, update the `model-types` thesaurus to add a new entry for `it.vedph.tgr.ms-signatures`:

```js
db.getCollection("thesauri").updateOne({ "_id": "model-types@en" }, { $push: { entries: { _id: "it.vedph.tgr.ms-signatures", value: "ms signatures" } } });
```

(3.2.) in index:

```sql
UPDATE `cadmus-tgr`.`pin` SET partTypeId='it.vedph.tgr.ms-signatures' WHERE partTypeId='it.vedph.itinera.ms-signatures';
```
