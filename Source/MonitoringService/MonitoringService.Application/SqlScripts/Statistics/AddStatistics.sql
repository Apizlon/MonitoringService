INSERT INTO public."Statistics" ("DeviceName","OperatingSystem","Version","LastUpdateDateTime")
VALUES (@DeviceName,@OperatingSystem,@Version,@LastUpdateDateTime)
RETURNING "Id";