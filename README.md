# Cadmus TGR Parts

Components for the _Tradizione Grammaticale Romana_ project (here TGR for short) in Cadmus.

## History

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

(3) `MsSignaturesPart` and `MsSignature` have been moved from Itinera to this project (Itinera is going to use a new generation model for codicology). Apart from their tag change, their code is unaffected.
