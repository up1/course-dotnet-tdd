# GitHub Copilot Instructions

**Project**: Database Migration/Redesign
**Last Updated**: 2025-12-17
---
## Project Overview
The goal is to migrate an existing PostgreSQL database schema to a MongoDB document-based database design. We want to leverage MongoDB's document model to improve performance by denormalizing data where appropriate.

## Technology Stack
*   **Source Database**: PostgreSQL 16 (Relational)
*   **Target Database**: MongoDB (NoSQL, document-oriented)
*   **Environment**: Visual Studio Code
---
## Coding Standards & Design Principles
*   Focus on data modeling that embeds related data to minimize joins, following MongoDB best practices.
*   Prioritize a schema design suitable for the application's most common access patterns.
*   Maintain data consistency as a key objective during denormalization.
*   Avoid using generic field names.
---
## Important Notes
Use the provided PostgreSQL schema definition as the source of truth for the data structure to be migrated.
