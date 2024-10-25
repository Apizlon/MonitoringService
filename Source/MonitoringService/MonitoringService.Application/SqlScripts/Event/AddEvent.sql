INSERT INTO "Events" ("Id","StatisticsId","EventDateTime","Name","Description")
VALUES (@Id,@StatisticsId,@EventDateTime,@Name,@Description)
RETURNING "Id";