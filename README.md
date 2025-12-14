# MappingSystem

## Overview
This project implements a small, extensible mapping system in .NET.
It maps internal domain models to partner-specific models and back again
using a central MapHandler.

The system is designed with a strong focus on separation of concerns
and easy extensibility, allowing new partners or data formats to be added
without modifying existing core logic.

---

## Architecture

The solution is split into four projects, each with a clear responsibility.

### MappingSystem.Core
Contains the core mapping infrastructure:
- MapHandler – central entry point responsible for routing mapping requests
- IObjectMapper – contract implemented by all concrete mappers
- MappingNotFoundException – domain-specific exception for missing mappings

The core project does not depend on any concrete models or partner implementations.

---

### MappingSystem.Models
Contains data models used by the system:
- Model.Reservation – internal application model
- Google.Reservation – partner-specific model

Separate namespaces are used intentionally to avoid coupling between
internal and external (partner) representations.

---

### MappingSystem.Mappings
Contains concrete mapping implementations:
- ReservationToGoogleMapper
- GoogleToReservationMapper

Each mapper encapsulates partner-specific mapping and transformation rules.

---

### MappingSystem (Console Application)
Demonstrates how the mapping system is used:
- registering available mappings
- performing bidirectional mapping
- handling errors when no mapping is available

The console app contains no business logic and exists purely as a usage example.

---

## Usage

The system is used by creating a MapHandler with a set of registered mappers
and calling the Map method with a source object and source/target identifiers.

Example (simplified):

    var handler = new MapHandler(new IObjectMapper[]
    {
        new ReservationToGoogleMapper(),
        new GoogleToReservationMapper()
    });

    var reservation = new Model.Reservation
    {
        Id = "RES-2025-001",
        GuestName = "Angela Merkel",
        From = new DateTime(2025, 3, 15),
        To = new DateTime(2025, 3, 18)
    };

    var googleReservation =
        (Google.Reservation)handler.Map(
            reservation,
            "Model.Reservation",
            "Google.Reservation"
        );

    var backToModel =
        (Model.Reservation)handler.Map(
            googleReservation,
            "Google.Reservation",
            "Model.Reservation"
        );

---

## Extending the System

To add support for a new partner or data format:

1. Add new model classes (if required) in MappingSystem.Models
2. Create one or more mapper classes in MappingSystem.Mappings
   implementing IObjectMapper
3. Define appropriate source and target type identifiers
4. Register the new mapper(s) when creating the MapHandler

No changes to the core mapping logic are required.

---

## Error Handling

- An exception is thrown when no mapping is registered for a given
  source/target type combination.
- Each mapper validates the runtime type of the input object and fails fast
  if an unexpected type is supplied.

---

## Assumptions

- Source and target type identifiers are provided consistently by callers.
- All mappings are registered at application startup.
- The demo focuses on reservation mapping; additional domain models would
  follow the same pattern.

---

## Limitations

- The dynamic mapping API relies on runtime type checks rather than
  compile-time type safety.
- Mapper registration is manual and explicit.
- The project demonstrates in-memory object mapping only; no persistence,
  serialization, or transport layers are included.
