UPDATE "Statistics"
SET
    "Id" = @Id,
    "DeviceName" = @DeviceName,
    "OperatingSystem" = @OperatingSystem,
    "Version" = @Version,
    "LastUpdateDateTime" = @LastUpdateDateTime
WHERE "Id" = @Id;