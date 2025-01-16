CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;
CREATE TABLE "ExchangeTables" (
    "Id" TEXT NOT NULL,
    "Table" TEXT NOT NULL,
    "No" TEXT NOT NULL,
    "EffectiveDate" TEXT NOT NULL,
    CONSTRAINT "PK_ExchangeTables" PRIMARY KEY ("Id")
);

CREATE TABLE "Rates" (
    "Id" TEXT NOT NULL,
    "Currency" TEXT NOT NULL,
    "Code" TEXT NOT NULL,
    "Mid" TEXT NOT NULL,
    "ExchangeTableId" TEXT,
    CONSTRAINT "PK_Rates" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Rates_ExchangeTables_ExchangeTableId" FOREIGN KEY ("ExchangeTableId") REFERENCES "ExchangeTables" ("Id")
);

CREATE INDEX "IX_Rates_ExchangeTableId" ON "Rates" ("ExchangeTableId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250115111439_Init migration', '9.0.1');

ALTER TABLE "Rates" DROP CONSTRAINT "FK_Rates_ExchangeTables_ExchangeTableId";

UPDATE "Rates" SET "ExchangeTableId" = '00000000-0000-0000-0000-000000000000' WHERE "ExchangeTableId" IS NULL;
ALTER TABLE "Rates" ALTER COLUMN "ExchangeTableId" SET NOT NULL;
ALTER TABLE "Rates" ALTER COLUMN "ExchangeTableId" SET DEFAULT '00000000-0000-0000-0000-000000000000';

ALTER TABLE "Rates" ADD CONSTRAINT "FK_Rates_ExchangeTables_ExchangeTableId" FOREIGN KEY ("ExchangeTableId") REFERENCES "ExchangeTables" ("Id") ON DELETE CASCADE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250115121340_Change type of EffectiveDate prop in ExchangeTable', '9.0.1');

COMMIT;

