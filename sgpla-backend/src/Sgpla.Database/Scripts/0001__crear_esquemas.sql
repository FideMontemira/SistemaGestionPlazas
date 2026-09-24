-- Esquemas de SGPLa (ver DATABASE.md, sección 1).
--   academico   : oferta académica
--   plazas      : ofertas vacantes, avisos y Consejo Técnico
--   usuarios    : identidad y acceso
--   integracion : servicios externos

IF SCHEMA_ID(N'academico') IS NULL EXEC (N'CREATE SCHEMA academico AUTHORIZATION dbo;');
IF SCHEMA_ID(N'plazas') IS NULL EXEC (N'CREATE SCHEMA plazas AUTHORIZATION dbo;');
IF SCHEMA_ID(N'usuarios') IS NULL EXEC (N'CREATE SCHEMA usuarios AUTHORIZATION dbo;');
IF SCHEMA_ID(N'integracion') IS NULL EXEC (N'CREATE SCHEMA integracion AUTHORIZATION dbo;');
