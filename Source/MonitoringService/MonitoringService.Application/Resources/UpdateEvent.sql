UPDATE "Events"
SET
    "Id" = @Id,
    "StatisticsId" = @StatisticsId,
    "EventDateTime" = @EventDateTime,
    "Name" = @Name,
    "Description" = @Description
WHERE "Id" = @Id;